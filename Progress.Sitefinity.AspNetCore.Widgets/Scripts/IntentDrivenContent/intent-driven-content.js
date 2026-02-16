(function () {
    const ERROR_MESSAGES = {
        GENERIC_ERROR: 'Error',
        SERVICE_UNAVAILABLE: 'Service unavailable',
        TOO_MANY_REQUESTS: 'Too many requests',
        PAYMENT_REQUIRED: 'Payment required',
        NO_DATA_FOUND: 'No data for this query could be found. Try another one.'
    };

    const DEFAULT_ERROR_CONFIG = {
        message: ERROR_MESSAGES.SERVICE_UNAVAILABLE,
        errorType: 'ServiceError',
    };

    const ERROR_STATUS_MAP = {
        429: {
            message: ERROR_MESSAGES.TOO_MANY_REQUESTS,
            errorType: 'QuotaExceeded',
            allowErrorSurfacing: false // explicitly set, but false by default
        },
        402: {
            message: ERROR_MESSAGES.PAYMENT_REQUIRED,
            errorType: 'QuotaExceeded',
            allowErrorSurfacing: false
        }
    };

    function getUserJourney() {
        const userJourneyData = {
            currentUserJourney: [],
            subjectKey: "",
            source: ""
        };

        if (window.DataIntelligenceSubmitScript) {
            userJourneyData.currentUserJourney = window.DataIntelligenceSubmitScript?._client?.recommenderClient?.getClientJourney() || [];
            userJourneyData.subjectKey = window.DataIntelligenceSubmitScript?._client?.subjectKey || "";
            userJourneyData.source = window.DataIntelligenceSubmitScript?._client?.source || "";
        }
        
        return userJourneyData;
    }

    function normalizeErrorResponse(status, responseText) {
        let responseData = null;
        if (responseText) {
            try {
                responseData = JSON.parse(responseText);
            } catch (e) {
                responseData = null;
            }
        }

        const statusConfig = ERROR_STATUS_MAP[status] || DEFAULT_ERROR_CONFIG;
        const errorData = {
            errorMessage: statusConfig.message,
            errorType: statusConfig.errorType
        };

        // Surface error message from the response if allowed for this status code
        // Also handle a couple of different error response types
        if (statusConfig.allowErrorSurfacing) {
            if (responseData?.errorMessage) {
                errorData.errorMessage = responseData.errorMessage;
            } else if (responseData?.message) {
                errorData.errorMessage = responseData.message;
            } else if (responseText) {
                errorData.errorMessage = responseText;
            }
        }

        return errorData;
    }

    function reportDataToInsights(intentBoxQuery) {
        function reportData() {
            DataIntelligenceSubmitScript?._client?.sentenceClient?.writeSentence({
                predicate: 'Intent Box Prompt',
                object: intentBoxQuery
            });
        }

        if (window.DataIntelligenceSubmitScript) {
            reportData();
        } else if (window.addEventListener) {
            window.addEventListener('decclientready', reportData, true);
        }
    }

    function changeLoadersVisibility(show) {
        document.querySelectorAll('[data-dg-skeleton]').forEach(function (loaderElement) {
            if (show) {
                loaderElement.style.removeProperty('display');
            } else {
                loaderElement.style.setProperty('display', 'none', 'important');
            }
        });
    }

    document.addEventListener('IntentDrivenContentRequestInProgress', function () {
        changeLoadersVisibility(true);
    });

    document.addEventListener('DOMContentLoaded', function () {
        const isInsightsEnabled = !!document.querySelector('script#sf-insight-settings');
        const containers = document.querySelectorAll('[data-sf-intent-driven-content]');
        for (var i = 0; i < containers.length; i++) {
            var container = containers[i];

            if (!container) {
                return;
            }

            const defaultQuery = container.getAttribute('data-sf-default-query');
            const serviceUrl = container.getAttribute('data-sf-service-url');
            const siteId = container.getAttribute('data-sf-site-id');
            const pageId = container.getAttribute('data-sf-page-id');
            const language = container.getAttribute('data-sf-language');
            const sections = container.getAttribute('data-sf-section-names')?.split(',').filter(x => x);

            const urlParams = new URLSearchParams(window.location.search);
            const isUserQuery = new URLSearchParams(window.location.search).has('dg-query');

            const dgQuery = urlParams.get('dg-query');
            const query = dgQuery || defaultQuery;
            const variationId = new URLSearchParams(window.location.search).get('variationId');

            if (!query && !variationId) {
                return;
            }

            var onData = function (data, innerContainer) {
                const sections = innerContainer.querySelectorAll('[data-dg-section="' + data.sectionName + '"]');
                 if (sections?.length) {
                        sections.forEach(section => {
                            renderSection(section, data.sectionData, onError);
                        });
                }

                // Dispatch event for each section loaded
                const eventDetail = { sectionName: data.sectionName, sectionData: data.sectionData };
                document.dispatchEvent(new Event('IntentDrivenContentSectionLoaded', { detail: eventDetail }));
            };

            var onError = function (error, innerContainer, renderErrorSection) {
                if (renderErrorSection) {
                        const cnt = innerContainer || container;
                        const errorSection = (cnt && cnt.querySelector('[data-dg-section="error"]')) || document.querySelector('[data-dg-section="error"]');
                        if (errorSection) {
                            errorSection.classList.remove('d-none');
                        let sectionData = error;
                        // If simple string, wrap it
                        if (typeof error === 'string') {
                            sectionData = {
                                errorMessage: error,
                                errorType: 'ServiceError'
                            };
                        }
                        // If object but using 'message' instead of 'errorMessage', fix it (although fetchDataStream handles this too now)
                        else if (sectionData && sectionData.message && !sectionData.errorMessage) {
                            sectionData.errorMessage = sectionData.message;
                        }
                        
                        renderSection(errorSection, sectionData);
                    }
                }

                // Remove loaders
                changeLoadersVisibility(false);

                // Dispatch error event for whatever error we receive
                document.dispatchEvent(new Event('IntentDrivenContentSectionError', { detail: error }));
            };

            function responseContainsNoData(value) {
                if (value == null) {
                    return true;
                }

                if (Array.isArray(value)) {
                    return value.length === 0;
                }

                if (typeof value === 'object' && Object.keys(value).length === 0) {
                    return true;
                }
                
                if (typeof value === 'string') {
                    return value.includes('Not enough data');
                }

                return false;
            }

            var onDone = function (innerContainer, collectedData) {
                const dataArray = Array.isArray(collectedData) ? collectedData : [];
                const hasNoData = dataArray.length === 0 || dataArray.every(item => responseContainsNoData(item?.sectionData));
                if (hasNoData) {
                    onError?.({
                        errorMessage: ERROR_MESSAGES.NO_DATA_FOUND,
                        errorType: 'NoContent'
                    }, innerContainer, true);
                    return;
                }

                // Replace all content on completion with collected data
                dataArray.forEach(function(collectedSection) {
                    const sectionElements = innerContainer.querySelectorAll('[data-dg-section="' + collectedSection.sectionName + '"]');
                    if (sectionElements?.length) {
                        sectionElements.forEach(section => {
                            renderSection(section, collectedSection.sectionData, onError);
                        });
                    }
                });

                // Remove loaders
                changeLoadersVisibility(false);

                // Dispatch event for all content loaded
                document.dispatchEvent(new Event('IntentDrivenContentContentLoaded', { detail: collectedData }));
            };

            function executeRequest() {
                fetchDataStream(
                    serviceUrl,
                    { sections: sections, query: query, siteId: siteId, pageId: pageId, language: language, variationId: variationId, isUserQuery: isUserQuery },
                    container,
                    onData,
                    onError,
                    onDone
                );

                dispatchLoadingStartedEvent(container);
            }

            function initiateRequest() {
                // do not send requests if no sections are configured
                if (!sections?.length) {
                    return;
                }
                if (!isInsightsEnabled) {
                    executeRequest();
                    return;
                }

                if (window.DataIntelligenceSubmitScript) {
                    executeRequest();
                    return;
                }

                if (window.addEventListener) {
                    const handleDecClientReady = function () {
                        window.removeEventListener('decclientready', handleDecClientReady, true);
                        executeRequest();
                    };

                    window.addEventListener('decclientready', handleDecClientReady, true);
                    return;
                }

                executeRequest();
            }

            initiateRequest();

            if (isUserQuery && isInsightsEnabled) {
                reportDataToInsights(query);
            }

            document.dispatchEvent(new Event('IntentDrivenContentRequestInProgress'));

            // No scroll to the first dynamically generated container on default query - only if the user entered a query
            const firstContainer = document.querySelector('[data-sf-intent-driven-content]');
            const inputElement = document.querySelector('.sf-intent-box input[type="text"]');

            if (firstContainer && inputElement && inputElement.value && inputElement.value.trim()) {
                firstContainer.scrollIntoView({ behavior: 'smooth', block: 'start' });
            }
        }
    });

    function dispatchLoadingStartedEvent(container) {
        container.querySelectorAll('[data-dg-section] intent-section').forEach((genericComponent) => {
            customElements.whenDefined('intent-section').then(() => {
                genericComponent.dispatchEvent(new CustomEvent('data-loading', {
                    bubbles: false
                }));
            }).catch((error) => {
                console.error('Error waiting for component definition: intent-section', error);
            });
        });
    }

    function renderSection(sectionElement, sectionData, onError) {
        if (!sectionElement || !sectionData) {
            return;
        }

        try {
            // Get section template name
            const templateName = sectionElement.dataset.dgSection?.toLowerCase();
            if (!templateName) {
                console.warn('Section element missing data-dg-section attribute');
                return;
            }

            // Prefer the generic component.
            const genericComponent = sectionElement.querySelector('intent-section');
            if (genericComponent) {
                customElements.whenDefined('intent-section').then(() => {
                    genericComponent.dispatchEvent(new CustomEvent('data-loaded', {
                        detail: sectionData,
                        bubbles: false
                    }));
                }).catch((error) => {
                    console.error('Error waiting for component definition: intent-section', error);
                });
                return;
            }

            const webComponent = sectionElement.querySelector(`[data-dg-section="${templateName}"]`);
            if (!webComponent) {
                console.warn('Web component not found:', componentTag, 'in section:', templateName);
                return;
            }

            customElements.whenDefined(componentTag).then(() => {
                webComponent.dispatchEvent(new CustomEvent('data-loaded', {
                    detail: sectionData,
                    bubbles: false
                }));
            }).catch((error) => {
                console.error('Error waiting for component definition:', componentTag, error);
            });

        } catch (error) {
            console.error('Failed to render section:', error);
            onError?.('Failed to render section: ' + error.message);
        }
    }

    /**
     * @param {string} apiUrl
     * @param {RequestParams} params
     * @param {HTMLElement} container
     * @param {(data: StreamChunk, container: HTMLElement) => void} [onData]
     * @param {(error: string, container: HTMLElement) => void} [onError]
     * @param {(container: HTMLElement) => void} [onComplete]
     * @returns {Promise<void>}
     */
    async function fetchDataStream(apiUrl, params, container, onData, onError, onComplete) {
        // Store all section data for later replacement of the page content
        let collectedData = '';

        const userJourney = getUserJourney();
        params.userJourneyData = userJourney;
            
        const response = await fetch(apiUrl, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(params)
        });
        if (!response.ok) {
            let responseText = '';
            try {
                responseText = await response.text();
            } catch (e) {
                responseText = '';
            }

            const errorData = normalizeErrorResponse(response.status, responseText);

            onError?.(errorData, container, true);
            return;
        }

        if (!response.body) {
            onError?.(normalizeErrorResponse(response.status, ERROR_MESSAGES.NO_DATA_FOUND), container, true);
            return;
        }

        // Get a reader from the stream
        const reader = response.body.getReader();
        const decoder = new TextDecoder();

        let parsingStreamingCollection = false;
        // Read the stream
        while (true) {
            const { done, value } = await reader.read();
            if (done) {
                break;
            }

            // Convert the Uint8Array to a string
            const chunk = decoder.decode(value, { stream: true });

            collectedData += chunk;

            try {
                // For streaming enumerable collection

                let trimmedChunk = chunk.trim();
                if (parsingStreamingCollection || trimmedChunk.startsWith('[')) {
                    if (trimmedChunk.startsWith('[') && trimmedChunk.endsWith(']')) {
                        // Single complete array in one chunk
                        const parsedData = JSON.parse(trimmedChunk);
                        parsedData.forEach((item) => onData?.(item, container));
                        continue;
                    }
                    parsingStreamingCollection = true;

                    if (trimmedChunk.startsWith('[')) {
                        trimmedChunk = trimmedChunk.substring(1).trim(); // Remove starting '['
                    }

                    if (trimmedChunk.startsWith(',')) {
                        trimmedChunk = trimmedChunk.substring(1).trim(); // Remove starting ','
                    }

                    if (trimmedChunk.endsWith(']')) {
                        trimmedChunk = trimmedChunk.substring(0, trimmedChunk.length - 1).trim(); // Remove ending ']'
                        parsingStreamingCollection = false;
                    }

                    if (trimmedChunk) {
                        const parsedData = JSON.parse(trimmedChunk);
                        onData?.(parsedData, container);
                        continue;
                    }
                }

                // Check if the chunk contains multiple JSON objects
                // (For APIs that send multiple JSON objects separated by newlines)
                if (chunk.includes('\n')) {
                    const jsonObjects = chunk.trim().split('\n');
                    for (const jsonStr of jsonObjects) {
                        if (jsonStr.trim()) {
                            const parsedData = JSON.parse(jsonStr);
                            onData?.(parsedData, container);
                            continue;
                        }
                    }
                } else {
                    // For a single JSON object in the chunk
                    const parsedData = JSON.parse(chunk);
                    onData?.(parsedData, container);
                    continue;
                }
            } catch (error) {
                console.warn('Error parsing JSON chunk:', error);
                // If it's not valid JSON, just send the raw chunk
                onError?.(chunk, container, false);
            }
        }
        try {
            collectedData = JSON.parse(collectedData);
        } catch (error) {
            console.error('Error parsing collected data:', error);
            collectedData = [];
        }

        onComplete?.(container, collectedData);

        return;
    }

})();
