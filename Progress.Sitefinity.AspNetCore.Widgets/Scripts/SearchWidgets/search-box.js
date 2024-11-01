﻿(function () {
    var activeAttribute = "data-sf-active";
    var dataSfItemAttribute = "data-sfitem";
    var activeClassElement = document.querySelector('[data-sf-active-class]');
    var activeClass = activeClassElement.dataset && isNotEmpty(activeClassElement.dataset.sfActiveClass) ? activeClassElement.dataset.sfActiveClass : null;
    var activeClassProcessed = activeClass ? processCssClass(activeClass) : null;
    var visibilityClassElement = document.querySelector('[data-sf-visibility-hidden]');
    var visibilityClassHidden = visibilityClassElement.dataset ? visibilityClassElement.dataset.sfVisibilityHidden : null;
    var searchAutocompleteItemElement = document.querySelector('[data-sf-search-autocomplete-item-class]');
    var autocompleteItemClass = searchAutocompleteItemElement.dataset ? searchAutocompleteItemElement.dataset.sfSearchAutocompleteItemClass : null;
    var autocompleteItemClassProcessed = autocompleteItemClass ? processCssClass(autocompleteItemClass) : null;

    function processCssClass(str) {
        var classList = str.split(" ");
        return classList;
    }

    function isNotEmpty(attr) {
        return (attr && attr !== "");
    }

    function init(event) {
        if (event && event.type === "widgetLoaded") {
            var inputField = event.detail.element.querySelector("[data-sf-role='search-box']");
            var searchButton = event.detail.element.querySelector("[data-sf-role='search-button'");
            if (inputField) {
                searchBox(inputField, searchButton);
            }
        } else {
            document.querySelectorAll("[data-sf-role='search-box']").forEach(function (inputField) {
                if (inputField) {
                    var searchButton = inputField.parentNode.querySelector("[data-sf-role='search-button'");
                    searchBox(inputField, searchButton);
                }
            });
        }
    }

    function searchBox(inputField, searchButton) {
        initSearchBox(inputField, searchButton);

        var autocompleteDropdown = autocomplete(inputField);

        function initSearchBox(input, button) {
            if (input) {
                input.addEventListener("keypress", function (ev) {
                    keypressHandler(ev);
                });

                input.addEventListener("keyup", function (ev) {
                    keyupHandler(ev);
                });
            }

            if (button) {
                button.addEventListener("click", function (ev) {
                    navigateToResults(input);
                });
            }
        }


        function getSearchBoxParams(input) {
            return {
                resultsUrl: input.getAttribute("data-sf-results-url"),
                catalogue: input.getAttribute("data-sf-search-catalogue"),
                scoringSetting: input.getAttribute("data-sf-scoring-setting"),
                minSuggestionLength: parseInt(input.getAttribute("data-sf-suggestions-length")),
                siteId: input.getAttribute("data-sf-site-id"),
                culture: input.getAttribute("data-sf-culture"),
                suggestionFields: input.getAttribute("data-sf-suggestions-fields"),
                servicePath: input.getAttribute("data-sf-service-path"),
                orderBy: input.getAttribute("data-sf-sort"),
                resultsForAllSites: input.getAttribute("data-sf-results-all")
            };
        }

        function keypressHandler(e) {
            if (!e) {
                e = window.event;
            }

            var keyCode = e.keyCode || e.charCode;

            if (keyCode == 13) {
                navigateToResults(e.target);
            }
        }

        function keyupHandler(e) {
            if (e.keyCode != 38 &&  // up arrow
                e.keyCode != 40 && // down arrow
                e.keyCode != 27) { // esc

                var searchText = e.target.value.trim();
                var config = getSearchBoxParams(e.target);

                if (config.minSuggestionLength && searchText.length >= config.minSuggestionLength) {
                    getSuggestions(e.target);
                } else {
                    if(autocompleteDropdown) {
                        autocompleteDropdown.hide();
                    }
                }
            }

            if (e.keyCode == 40) {
                autocompleteDropdown.focus();
            } else if (e.keyCode == 27) {
                autocompleteDropdown.hide();
            }
        }

        function getSuggestions(input) {
            var data = getSearchBoxParams(input);
            var requestUrl = data.servicePath +
                "/Default.GetSuggestions()" +
                "?indexName=" + data.catalogue +
                "&sf_culture=" + data.culture +
                "&siteId=" + data.siteId +
                "&scoringInfo=" + data.scoringSetting +
                "&suggestionFields=" + data.suggestionFields +
                "&searchQuery=" + input.value;
            if (data.resultsForAllSites == 1) {
                requestUrl += "&resultsForAllSites=True";
            }
            else if (data.resultsForAllSites == 2) {
                requestUrl += "&resultsForAllSites=False";
            }

            fetch(requestUrl).then(function (res) {
                res.json().then(function (suggestions) {
                    autocompleteDropdown.source(suggestions.value);
                });
            }).catch(function () {
                autocompleteDropdown.hide();
            });
        }

        function navigateToResults(input) {
            if (window.DataIntelligenceSubmitScript) {
                DataIntelligenceSubmitScript._client.fetchClient.sendInteraction({
                    P: "Search for",
                    O: input.value.trim(),
                    OM: {
                        PageUrl: location.href
                    }
                });
            }

            var url = getSearchUrl(input);
            window.location = url;
        }

        function getSearchUrl(input) {
            var query = input.value.trim();
            var resultsUrl = input.getAttribute("data-sf-results-url");
            var separator = resultsUrl.indexOf("?") === -1 ? "?" : "&";
            var catalogue = separator + "indexCatalogue=" + input.getAttribute("data-sf-search-catalogue");
            var searchQuery = "&searchQuery=" + encodeURIComponent(query);
            var wordsMode = "&wordsMode=" + "AllWords";
            var culture = "&sf_culture=" + input.getAttribute("data-sf-culture");

            var url = resultsUrl + catalogue + searchQuery + wordsMode + culture;

            var scoringSetting = input.getAttribute("data-sf-scoring-setting");
            if (scoringSetting) {
                url = url + "&scoringInfo=" + scoringSetting;
            }

            var sorting = input.getAttribute("data-sf-sort");
            if (sorting) {
                url = url + "&orderBy=" + sorting;
            }

            var resultsForAllSites = input.getAttribute("data-sf-results-all");
            if (resultsForAllSites == 1) {
                url += "&resultsForAllSites=True";
            }
            else if (resultsForAllSites == 2) {
                url += "&resultsForAllSites=False";
            }

            return url;
        }

        /**
         * Autocomplete dropdown functionality
         */
        function autocomplete(inputField) {
            var suggestionsDropdown = inputField.parentNode.parentNode.querySelector("[data-sf-role='search-box-autocomplete']");
            var suggestions = [];

            function setSource(items) {
                items = Array.isArray(items) ? items : [];
                suggestions = generateDropdownItems(items);

                clearDropdown();

                for (var i = 0; i < suggestions.length; i++) {
                    if (suggestionsDropdown) {
                        suggestionsDropdown.appendChild(suggestions[i]);
                    }
                }

                if (suggestions.length) {
                    show();
                }
            }

            function clearDropdown() {
                if (suggestionsDropdown) {
                    var child = suggestionsDropdown.lastElementChild;
                    while (child) {
                        suggestionsDropdown.removeChild(child);
                        child = suggestionsDropdown.lastElementChild;
                    }
                }
            }

            function generateDropdownItems(suggestions) {
                var dropDownItems = [];

                if (Array.isArray(suggestions) && suggestions.length > 0) {
                    for (var i = 0; i < suggestions.length; i++) {
                        var dropdownItem = document.createElement("LI");
                        dropdownItem.setAttribute("role", "option");
                        var item = document.createElement("BUTTON");
                        item.setAttribute("role", "presentation");

                        if (autocompleteItemClassProcessed) {
                            item.classList.add(...autocompleteItemClassProcessed);
                        }

                        item.setAttribute(dataSfItemAttribute, "");
                        item.innerText = suggestions[i];
                        item.title = suggestions[i];
                        item.tabIndex = -1;
                        dropdownItem.appendChild(item);
                        dropDownItems.push(dropdownItem);
                    }
                }

                return dropDownItems;
            }

            function suggestionsKeyupHandler(e) {
                var key = e.keyCode;
                var activeLinkSelector = `[${dataSfItemAttribute}][${activeAttribute}]`;
                var activeLink = suggestionsDropdown.querySelector(activeLinkSelector);
                if (!activeLink) {
                    return;
                }

                var previousParent = activeLink.parentElement.previousElementSibling;
                var nextParent = activeLink.parentElement.nextElementSibling;
                if (key == 38 && previousParent) {
                    focusItem(previousParent);
                } else if (key == 40 && nextParent) {
                    focusItem(nextParent);
                } else if (key == 13) {
                    inputField.value = currentNode.innerText;
                    navigateToResults(inputField);
                    hide();
                    inputField.focus();
                } else if (key == 27) {
                    resetActiveClass();
                    hide(false);
                    inputField.focus();
                }
            }

            function resetActiveClass() {
                var activeLink = suggestionsDropdown.querySelector(`[${activeAttribute}]`);

                if (activeLink && activeClassProcessed) {
                    activeLink.classList.remove(...activeClassProcessed);
                    activeLink.removeAttribute(activeAttribute);
                }
            }

            function suggestionsClickHandler(e) {
                var target = e.target;
                var content = target.innerText;
                inputField.value = content;
                navigateToResults(inputField);
                hide();
            }

            function inputKeyupHandler(e) {
                if (e.keyCode == 40 && suggestions.length) {
                    show();
                    focusItem(suggestions[0]);
                }
            }

            function show() {
                setAutocompleteDropdownWidth();

                if (suggestionsDropdown) {
                    if (visibilityClassHidden) {
                        suggestionsDropdown.classList.remove(visibilityClassHidden);
                    } else {
                        suggestionsDropdown.style.display = "";
                    }
                }
            }

            function hide(clear) {
                if (clear === undefined) {
                    clear = true;
                }

                if (clear) {
                    clearDropdown();
                }
                clearAutocompleteDropdownWidth();

                if (suggestionsDropdown) {

                    if (visibilityClassHidden) {
                        suggestionsDropdown?.classList.add(visibilityClassHidden);
                    } else {
                        if (suggestionsDropdown != null)
                            suggestionsDropdown.style.display = "none";
                    }
                }
            }

            function dropdownFocusout(e) {
                if (suggestionsDropdown != null && !suggestionsDropdown.contains(e.relatedTarget)) {
                    hide(false);
                }
            }

            function focus() {
                if (suggestionsDropdown && suggestionsDropdown.children.length) {
                    suggestionsDropdown.children[0].querySelector(`[${dataSfItemAttribute}]`).focus();
                }
            }

            function focusItem(item) {
                resetActiveClass();

                var link = item.querySelector(`[${dataSfItemAttribute}]`);

                if (link && activeClassProcessed) {
                    link.classList.add(...activeClassProcessed);
                }

                //set data attribute, to be used in queries instead of class
                link.setAttribute(activeAttribute, "");

                link.focus();
            }

            function setAutocompleteDropdownWidth() {
                const inputWidth = inputField.clientWidth;

                if (suggestionsDropdown) {
                    suggestionsDropdown.style.width = inputWidth + "px";
                }
            }

            function clearAutocompleteDropdownWidth() {
                if (suggestionsDropdown != null)
                    suggestionsDropdown.style.width = "";
            }

            inputField.addEventListener("keyup", inputKeyupHandler);
            inputField.addEventListener("focusout", dropdownFocusout);
            suggestionsDropdown?.addEventListener("focusout", dropdownFocusout);
            suggestionsDropdown?.addEventListener("keyup", suggestionsKeyupHandler);
            suggestionsDropdown?.addEventListener("click", suggestionsClickHandler);

            return {
                source: setSource,
                show: show,
                hide: hide,
                clear: clearDropdown,
                focus: focus
            };
        }
    }

    document.addEventListener('widgetLoaded', init);
    document.addEventListener("DOMContentLoaded", init)
})();
