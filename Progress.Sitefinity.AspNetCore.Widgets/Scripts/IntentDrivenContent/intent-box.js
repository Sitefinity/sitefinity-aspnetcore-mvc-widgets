(function () {
    document.addEventListener('DOMContentLoaded', function () {
        var inputBoxes = document.querySelectorAll('[data-sf-intent-box]');

        inputBoxes.forEach(function (inputBox) {
            var inputField = inputBox.querySelector('input[type="text"]');
            if (!inputField) {
                return;
            }

            attachSuggestionClickHandlers(inputBox);

            // Pre-fill the input field with the current query if available
            var currentQuery = getCurrentQuery();
            if (currentQuery) {
                inputField.value = currentQuery;
            }

            var submitButton = inputBox.querySelector('.submit-button');

            inputField.addEventListener('keydown', function (event) {
                if (event.key === 'Enter') {
                    event.preventDefault();
                    handleSubmit(inputBox, inputField);
                }
            });

            if (submitButton) {
                var clearIcon = submitButton.querySelector('.clear');
                var submitIcon = submitButton.querySelector('.submit');

                document.addEventListener('IntentDrivenContentContentLoaded', function () {
                    if (inputField.value && inputField.value.trim()) {
                        clearIcon?.classList.remove('d-none');
                        submitIcon?.classList.add('d-none');
                        submitButton.setAttribute('data-sf-clear', true);
                    }
                });

                inputField.addEventListener('input', function () {
                    clearIcon?.classList.add('d-none');
                    submitIcon?.classList.remove('d-none');
                    submitButton.hasAttribute('data-sf-clear') && submitButton.removeAttribute('data-sf-clear');
                });

                submitButton.addEventListener('click', function () {
                    if (submitButton.hasAttribute('data-sf-clear')) {
                        clearIcon?.classList.add('d-none');
                        submitIcon?.classList.remove('d-none');
                        inputField.value = '';
                        submitButton.removeAttribute('data-sf-clear');
                        return;
                    } else {
                        handleSubmit(inputBox, inputField);
                    }
                });
            }
        });
    });

    function handleSubmit(inputBox, inputField) {
        var query = inputField.value.trim();
        if (!query) {
            return;
        }

        var targetPage = inputBox.getAttribute('data-sf-target-page') || window.location.pathname;
        var searchParam = new URLSearchParams(window.location.search);
        searchParam.set('dg-query', query);
        var url = new URL(targetPage, window.location.origin);
        url.search = searchParam;
        window.location.href = url.toString();
    }

    function getCurrentQuery() {
        var searchParam = new URLSearchParams(window.location.search);
        return searchParam.get('dg-query') || '';
    }

    function attachSuggestionClickHandlers(inputContainer) {
        var suggestionsContainer = inputContainer.querySelector('.suggestions-container');
        if (!suggestionsContainer) {
            return;
        }

        var suggestions = suggestionsContainer.querySelectorAll('.suggestion-item');

        suggestions.forEach(function (suggestion) {
            suggestion.addEventListener('click', function () {
                var inputField = inputContainer.querySelector('input[type="text"]');
                if (inputField) {
                    var submitButton = inputContainer.querySelector('.submit-button');
                    if (submitButton && submitButton.hasAttribute('data-sf-clear')) {
                        submitButton.removeAttribute('data-sf-clear');
                    }

                    inputField.value = suggestion.textContent;

                    if (submitButton) {
                        submitButton.click();
                    }
                }
            });
        });
    }
})();
