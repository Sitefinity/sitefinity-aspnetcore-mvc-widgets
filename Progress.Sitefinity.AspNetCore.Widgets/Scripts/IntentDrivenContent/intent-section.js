/**
 * Generic intent-driven section web component.
 *
 * Contract:
 * - The host page provides a <template id="..."> element with markup for the section.
 * - The component receives API data via a `data-loaded` CustomEvent (detail = object | string | array).
 * - Skeleton loaders: Provide skeleton markup inside the component via data-dg-skeleton attribute. 
 *   It shows until data arrives, then gets replaced by the rendered template.
 * - Nodes inside the template opt-in to binding via:
 *   - data-dg-text="path.to.value"         => sets textContent (XSS-safe)
 *   - data-dg-html="path.to.value"         => sets sanitized innerHTML (for rich text/markdown)
 *   - data-dg-attr-<name>="path"           => sets attribute <name> (e.g., data-dg-attr-href, data-dg-attr-src)
 *   - data-dg-if="path"                    => hides node when falsy (adds hidden attribute)
 *   - data-dg-each="path" data-dg-item="x" => repeats children for arrays (creates scoped item variable)
 *
 * Data normalization:
 * - Strings are wrapped as { content: "..." }
 * - Arrays are wrapped as { root: [...], items: [...] }
 * - Objects pass through unchanged
 *
 * Notes:
 * - We intentionally keep styling out and focus on rendering/binding.
 * - Rich text: if marked.parse exists we treat source as markdown; otherwise we treat as HTML.
 * - All text bindings use textContent (XSS-safe). HTML bindings use DOMParser.
 */

(function () {
    const DEFAULT_SKELETON_ATTR = 'data-dg-skeleton';

    // URL-bearing attributes that should be sanitized to avoid script URL injection.
    const URL_ATTRS = new Set(['href', 'src']);

    /**
     * Sanitize a URL-like attribute value.
     *
     * Allows:
     * - relative URLs
     * - fragment-only (#...)
     * - http(s)
     * - mailto/tel
     *
     * Rejects:
     * - javascript:, vbscript:, data: (conservative default)
     * - malformed URLs
     *
     * @param {string} raw
     * @returns {string|null} a safe value to set, or null to remove the attribute.
     */
    function sanitizeUrl(raw) {
        const value = String(raw ?? '').trim();
        if (!value) return null;

        // Allow in-page anchors.
        if (value.startsWith('#')) return value;

        // Resolve relative URLs against the current document.
        let parsed;
        try {
            parsed = new URL(value, document.baseURI);
        } catch {
            return null;
        }

        const protocol = String(parsed.protocol || '').toLowerCase();
        switch (protocol) {
            case 'http:':
            case 'https:':
            case 'mailto:':
            case 'tel:':
                // Keep the original text so we don’t rewrite relative URLs.
                return value;
            default:
                return null;
        }
    }

    /**
     * Safely get nested values from an object by a dotted path.
     * @param {any} obj
     * @param {string} path
     */
    function getPath(obj, path) {
        if (!path) return undefined;
        const parts = path.split('.').map(p => p.trim()).filter(Boolean);
        let current = obj;
        for (const key of parts) {
            if (current == null) return undefined;
            current = current[key];
        }
        return current;
    }

    /**
     * Convert markdown/HTML string into sanitized HTML.
     * @param {string} raw
     */
    function toSafeHtml(raw) {
        const source = raw ?? '';

        // Prefer markdown -> HTML if marked is present
        let html = source;
        try {
            if (typeof marked !== 'undefined' && typeof marked.parse === 'function'
                && typeof DOMPurify !== 'undefined' && typeof DOMPurify.sanitize === 'function') {
                html = DOMPurify.sanitize(marked.parse(source));
            }
        } catch {
            // fall back to original string
            html = source;
        }

        // Make a best-effort parse. This does NOT sanitize, but it avoids breaking DOM.
        const parser = new DOMParser();
        const doc = parser.parseFromString(String(html), 'text/html');
        return doc.body?.innerHTML ?? '';
    }

    class IntentSection extends HTMLElement {
        constructor() {
            super();
            this._loaded = false;
            this._data = undefined;
            this._onDataLoaded = this._onDataLoaded.bind(this);
            this._onDataLoading = this._onDataLoading.bind(this);
        }

        //  Declares which HTML attributes trigger attributeChangedCallback when they change.
        static get observedAttributes() {
            return ['template-id', 'data-template-id'];
        }

        // Runs when the element is inserted into the DOM.
        connectedCallback() {
            this.classList.add('intent-section');

            this.addEventListener('data-loading', this._onDataLoading);

            // Listen for data updates from the renderer.
            this.addEventListener('data-loaded', this._onDataLoaded);

            // If server rendered data attributes exist, allow immediate render.
            const initialJson = this.getAttribute('data-initial');
            if (initialJson) {
                try {
                    this.setData(JSON.parse(initialJson));
                } catch {
                    // ignore
                }
            }
        }

        // Removes the event listener when the element is removed from DOM.
        disconnectedCallback() {
            this.removeEventListener('data-loaded', this._onDataLoaded);
            this.removeEventListener('data-loading', this._onDataLoading);
        }

        // Re-renders when observed attributes change 
        attributeChangedCallback() {
            // If template changes after load, re-render.
            if (this._loaded) {
                this._render();
            }
        }

        /**
         * Public hook used by renderer code.
         * @param {any} data
         */
        setData(data) {
            this._data = data;
            this._loaded = true;
            this._render();
        }

        _onDataLoaded(e) {
            this.setData(e?.detail);
        }

        _onDataLoading(e) {
            this._renderSkeleton();
        }

        _getTemplate() {
            const id = this.getAttribute('template-id') || this.getAttribute('data-template-id');
            if (!id) return null;
            const doc = this.ownerDocument || document;
            const template = doc.getElementById(id);
            if (!template || template.tagName.toLowerCase() !== 'template') return null;
            return template;
        }

        // Shows a loading placeholder while waiting for API data.
        // Uses data-dg-skeleton attribute for flexible, declarative skeleton markup.
        _renderSkeleton() {
            // Check if skeleton content exists
            const skeletonEl = this.querySelector(`[${DEFAULT_SKELETON_ATTR}]`);
            
            if (skeletonEl) {
                skeletonEl.style.removeProperty('display');
                return;
            }
        }

        _render() {
            const template = this._getTemplate();
            if (!template) {
                console.warn('IntentSection: missing template. Provide template-id/data-template-id.');
                return;
            }

            const data = this._normalizeData(this._data);
            const fragment = template.content.cloneNode(true);

            // Apply bindings with data directly in scope (so hasRoot, root, items are accessible)
            this._applyBindings(fragment, data, data);

            // Remove skeleton blocks entirely (don't just hide them)
            fragment.querySelectorAll(`[${DEFAULT_SKELETON_ATTR}]`).forEach(el => {
                el.remove();
            });

            this.replaceChildren(fragment);
        }

        _normalizeData(raw) {
            // Some endpoints might give string directly (e.g. rich text).
            if (typeof raw === 'string') {
                return { content: raw };
            }
            // Some endpoints return arrays directly (e.g. content list, FAQ).
            // Expose as { root: [...], items: [...] } for convenient bindings in templates.
            if (Array.isArray(raw)) {
                return { root: raw, items: raw };
            }
            return raw ?? {};
        }

        /**
         *  The core binding logic. Finds all data-dg-* attributes and:
         *      data-dg-if → Hides node if value is falsy
         *      data-dg-each → Loops over arrays, creates scoped variables
         *      data-dg-text → Sets textContent (XSS-safe)
         *      data-dg-html → Sets sanitized innerHTML (for rich text)
         *      data-dg-attr-* → Sets attributes (href, src, etc.)
         * @param {DocumentFragment|HTMLElement} root
         * @param {any} data
         * @param {{root:any, [k:string]:any}} scope
         */
        _applyBindings(root, data, scope) {
            const doc = root.ownerDocument || this.ownerDocument || document;
            const xpath = ".//*[@data-dg-text or @data-dg-html or @data-dg-if or @data-dg-each or @*[starts-with(name(), 'data-dg-attr-')]]";
            const nodes = [];

            // go over direct children only because we cannot pass document fragment as contextNode
            for(const child of root.children) {
                const result = doc.evaluate(xpath, child, null, XPathResult.ORDERED_NODE_SNAPSHOT_TYPE, null);
                for (let i = 0; i < result.snapshotLength; i++) {
                    nodes.push(result.snapshotItem(i));
                }
            }

            for (const node of nodes) {
                // Conditional
                const ifPath = node.getAttribute('data-dg-if');
                if (ifPath) {
                    const val = this._resolve(ifPath, data, scope);
                    if (!val) {
                        node.setAttribute('hidden', 'hidden');
                        continue;
                    }
                    node.removeAttribute('hidden');
                }

                // Repeat
                const eachPath = node.getAttribute('data-dg-each');
                if (eachPath) {
                    const itemName = node.getAttribute('data-dg-item') || 'item';
                    const items = this._resolve(eachPath, data, scope);

                    // Expect node's direct children as "item template"
                    const itemTemplate = Array.from(node.childNodes).map(n => n.cloneNode(true));
                    node.replaceChildren();

                    if (Array.isArray(items)) {
                        for (const it of items) {
                            const localScope = { ...scope, [itemName]: it };
                            const container = doc.createDocumentFragment();
                            for (const t of itemTemplate) container.appendChild(t.cloneNode(true));
                            // Recurse bindings within repeated content
                            this._applyBindings(container, data, localScope);
                            node.appendChild(container);
                        }
                    }

                    // Continue: repeated nodes shouldn't also do text/html.
                    continue;
                }

                // Text
                const textPath = node.getAttribute('data-dg-text');
                if (textPath) {
                    const value = this._resolve(textPath, data, scope);
                    node.textContent = value == null ? '' : String(value);
                }

                // HTML / rich text
                const htmlPath = node.getAttribute('data-dg-html');
                if (htmlPath) {
                    const value = this._resolve(htmlPath, data, scope);
                    node.innerHTML = toSafeHtml(value == null ? '' : String(value));
                }

                // Attributes
                for (const attr of Array.from(node.attributes)) {
                    if (!attr.name.startsWith('data-dg-attr-')) continue;
                    const realName = attr.name.replace('data-dg-attr-', '');
                    const realNameLower = realName.toLowerCase();
                    const value = this._resolve(attr.value, data, scope);
                    if (value == null || value === false) {
                        node.removeAttribute(realName);
                    } else {
                        if (URL_ATTRS.has(realNameLower)) {
                            const safe = sanitizeUrl(String(value));
                            if (safe == null) {
                                node.removeAttribute(realName);
                            } else {
                                node.setAttribute(realName, safe);
                            }
                            continue;
                        }

                        node.setAttribute(realName, String(value));
                    }
                }
            }
        }

        _resolve(expr, data, scope) {
            // Support scope variables, e.g. "item.title" or "root.title"
            const trimmed = (expr || '').trim();
            if (!trimmed) return undefined;

            const firstDot = trimmed.indexOf('.');
            const head = firstDot === -1 ? trimmed : trimmed.slice(0, firstDot);

            if (Object.prototype.hasOwnProperty.call(scope, head)) {
                const rest = firstDot === -1 ? '' : trimmed.slice(firstDot + 1);
                return rest ? getPath(scope[head], rest) : scope[head];
            }

            return getPath(data, trimmed);
        }
    }

    if (!customElements.get('intent-section')) {
        customElements.define('intent-section', IntentSection);
    }
})();
