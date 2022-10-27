(function () {
    function init(parent) {
        var searchResults = parent.querySelectorAll("[data-sf-role='search-results']");

        searchResults.forEach(function (sr) {
            var searchResultsSortingDropdown = sr.querySelector(".userSortDropdown");
            if (searchResultsSortingDropdown) {
                searchResultsSortingDropdown.querySelectorAll("option").forEach(function (option) {
                    option.selected = option.value == sr.getAttribute("data-sf-sorting");
                });

                searchResultsSortingDropdown.addEventListener("change", function (e) {
                    var value = e.target.value;
                    handleSelectionChange(sr, value);
                });
            }

            sr.addEventListener("click", function (e) {
                if (e.target.getAttribute("data-sf-role") === "search-results-language") {
                    var language = e.target.getAttribute("data-sf-language");
                    if (language) {
                        handleSelectionChange(sr, undefined, language);
                    }
                }
            });
        });

        function handleSelectionChange(parentElement, orderValue, languageValue) {
            var query = parentElement.getAttribute("data-sf-search-query") || "";
            var index = parentElement.getAttribute("data-sf-search-catalogue") || "";
            var wordsMode = parentElement.getAttribute("data-sf-words-mode") || "";
            var language = languageValue || parentElement.getAttribute("data-sf-language") || "";
            var orderBy = orderValue || parentElement.getAttribute("data-sf-sorting") || "";
            var scroingInfo = parentElement.getAttribute("data-sf-scoring-info");
            var resultsForAllSites = parentElement.getAttribute("data-sf-results-all");

            var query = "?searchQuery=" + query +
                "&indexCatalogue=" + index +
                "&wordsMode=" + wordsMode +
                "&sf_culture=" + language +
                "&orderBy=" + orderBy;

            if (scroingInfo) {
                query = query + "&scoringInfo=" + scroingInfo;
            }

            if (resultsForAllSites == "True") {
                query += "&resultsForAllSites=True";
            }
            else if (resultsForAllSites == "False") {
                query += "&resultsForAllSites=False";
            }

            window.location.search = query;
        }
    }

    document.addEventListener('widgetLoaded', function (args) {
        if (args.detail.model.Name === "SitefinitySearchResults") {
            init(args.detail.element);
        }
    });

    document.addEventListener("DOMContentLoaded", function () { init(document); });
})();
