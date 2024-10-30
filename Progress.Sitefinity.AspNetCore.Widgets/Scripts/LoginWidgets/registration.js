(function () {
    document.addEventListener('DOMContentLoaded', function () {
        var widgetContainer = document.querySelector('[data-sf-role="sf-registration-container"]');
        var formContainer = widgetContainer.querySelector('[data-sf-role="form-container"]');
        var form = formContainer.querySelector("form");
        var errorMessageContainer = widgetContainer.querySelector('[data-sf-role="error-message-container"]');
        var successRegistrationMessageContainer = widgetContainer.querySelector('[data-sf-role="success-registration-message-container"]');
        var confirmRegistrationMessageContainer = widgetContainer.querySelector('[data-sf-role="confirm-registration-message-container"]');
        var visibilityClassElement = document.querySelector('[data-sf-visibility-hidden]');
        var visibilityClassHidden = visibilityClassElement.dataset ? visibilityClassElement.dataset.sfVisibilityHidden : null;
        var invalidClassElement = document.querySelector('[data-sf-invalid]');
        var classInvalidValue = invalidClassElement.dataset && isNotEmpty(invalidClassElement.dataset.sfInvalid) ? invalidClassElement.dataset.sfInvalid : null;
        var classInvalid = classInvalidValue ? processCssClass(classInvalidValue) : null;

        var invalidDataAttr = "data-sf-invalid";

        function isNotEmpty(attr) {
            return (attr && attr !== "");
        }

        function processCssClass(str) {
            var classList = str.split(" ");
            return classList;
        }

        form.addEventListener('submit', function (event) {
            event.preventDefault();

            if (!validateForm(form)) {
                return;
            }

            setAntiforgeryTokens().then(res => {
                if (validateForm(form)) {
                    submitFormHandler(form, null, postRegistrationAction, onRegistrationError);
                }
            }, err => {
                showError("Antiforgery token retrieval failed");
            })
        });

        var postRegistrationAction = function () {
            var action = formContainer.querySelector("input[name='PostRegistrationAction']").value;
            var activationMethod = formContainer.querySelector("input[name='ActivationMethod']").value;

            if (action === 'ViewMessage') {
                if (activationMethod == "AfterConfirmation") {
                    showSuccessAndConfirmationSentMessage();
                } else {
                    showSuccessMessage();
                }
            } else if (action === 'RedirectToPage') {
                var redirectUrl = formContainer.querySelector("input[name='RedirectUrl']").value;

                redirect(redirectUrl);
            }
        };

        var onRegistrationError = function (errorMessage, status) {
            errorMessageContainer.innerText = errorMessage;
            showElement(errorMessageContainer);
        };

        var showSuccessMessage = function () {
            hideElement(errorMessageContainer);
            hideElement(formContainer);
            showElement(successRegistrationMessageContainer);
        };

        var showSuccessAndConfirmationSentMessage = function () {
            hideElement(formContainer);
            showElement(confirmRegistrationMessageContainer);

            var header = confirmRegistrationMessageContainer.querySelector('[data-sf-role="activation-link-header');
            header.innerText = confirmRegistrationMessageContainer.querySelector('[name="PleaseCheckYourEmailHeader"]').value;

            var activationLinkLabel = confirmRegistrationMessageContainer.querySelector("input[name='PleaseCheckYourEmailMessage']").value;

            var activationLinkMessageContainer = confirmRegistrationMessageContainer.querySelector('[data-sf-role="activation-link-message-container"]');

            var formData = new FormData(form);
            var email = formData.get("Email");

            activationLinkMessageContainer.innerText = activationLinkLabel + " " + email;

            var sendAgainBtn = confirmRegistrationMessageContainer.querySelector('[data-sf-role="sendAgainLink"]');
            sendAgainBtn.replaceWith(sendAgainBtn.cloneNode(true));
            sendAgainBtn = confirmRegistrationMessageContainer.querySelector('[data-sf-role="sendAgainLink"]');

            sendAgainBtn.innerText = confirmRegistrationMessageContainer.querySelector('input[name="SendAgainLink"]').value;

            var resendUrl = confirmRegistrationMessageContainer.querySelector("input[name='ResendConfirmationEmailUrl']").value;

            sendAgainBtn.addEventListener('click', function (event) {
                event.preventDefault();
                submitFormHandler(form, resendUrl, postResendAction);
            });
        };

        var showErrorMessage = function () {
            hideElement(formContainer);
            showElement(confirmRegistrationMessageContainer);

            var header = confirmRegistrationMessageContainer.querySelector('[data-sf-role="activation-link-header');
            header.innerText = confirmRegistrationMessageContainer.querySelector('[name="ActivateAccountTitle"]').value;

            var activationLinkMessageContainer = confirmRegistrationMessageContainer.querySelector('[data-sf-role="activation-link-message-container"]');
            var email = confirmRegistrationMessageContainer.querySelector('input[name="ExistingEmail"]').value;
            var activationLabel = confirmRegistrationMessageContainer.querySelector('input[name="ActivateAccountMessage"]').value.replace("{0}", email);
            activationLinkMessageContainer.innerText = activationLabel;
            form.querySelector("input[name='Email']").value = email;
            var userExists = Boolean(widgetContainer.querySelector("input[name='ExistingEmail']").value);

            var sendAgainBtn = confirmRegistrationMessageContainer.querySelector('[data-sf-role="sendAgainLink"]');

            if (userExists) {
                sendAgainBtn.innerText = confirmRegistrationMessageContainer.querySelector('input[name="ActivateAccountLink"]').value;

                var resendUrl = confirmRegistrationMessageContainer.querySelector("input[name='ResendConfirmationEmailUrl']").value;
                sendAgainBtn.addEventListener('click', function (event) {
                    event.preventDefault();
                    submitFormHandler(form, resendUrl, showSuccessAndConfirmationSentMessage);
                });
            } else {
                hideElement(sendAgainBtn);
            }
        };

        var submitFormHandler = function (form, url, onSuccess, onError) {

            url = url || form.attributes['action'].value;

            var model = { model: serializeForm(form) };

            window.fetch(url, { method: 'POST', body: JSON.stringify(model), headers: { 'Content-Type': 'application/json' } })
                .then((response) => {
                    var status = response.status;
                    if (status === 0 || (status >= 200 && status < 400)) {
                        if (onSuccess) {
                            onSuccess();
                        }
                    } else {
                        response.json().then((res) => {
                            var message = res.error.message;

                            if (onError) {
                                onError(message, status);
                            }
                        });
                    }
                });
        };

        var postResendAction = function () {
            var header = confirmRegistrationMessageContainer.querySelector('[data-sf-role="activation-link-header');
            header.innerText = confirmRegistrationMessageContainer.querySelector('[name="PleaseCheckYourEmailHeader"]').value;

            var activationLinkMessageContainer = confirmRegistrationMessageContainer.querySelector('[data-sf-role="activation-link-message-container"]');
            var sendAgainLabel = confirmRegistrationMessageContainer.querySelector("input[name='PleaseCheckYourEmailAnotherMessage']").value;
            var formData = new FormData(form);
            var email = formData.get("Email");
            activationLinkMessageContainer.innerText = sendAgainLabel.replace("{0}", email);
        };

        var validateForm = function (form) {
            var isValid = true;
            resetValidationErrors(form);
            hideElement(errorMessageContainer);

            var requiredInputs = form.querySelectorAll("input[data-sf-role='required']");

            requiredInputs.forEach(function (input) {
                if (!input.value) {
                    invalidateElement(input);
                    isValid = false;
                }
            });

            if (!isValid) {
                errorMessageContainer.innerText = formContainer.querySelector("input[name='ValidationRequiredMessage']").value;
                showElement(errorMessageContainer);

                return isValid;
            }

            var emailInput = form.querySelector("input[name='Email']");
            if (!isValidEmail(emailInput.value)) {
                errorMessageContainer.innerText = formContainer.querySelector("input[name='ValidationInvalidEmailMessage']").value;
                invalidateElement(emailInput);
                showElement(errorMessageContainer);
                return false;
            }

            var passwordFields = form.querySelectorAll("[type='password']");

            if (passwordFields[0].value !== passwordFields[1].value) {
                errorMessageContainer.innerText = formContainer.querySelector("input[name='ValidationMismatchMessage']").value;
                invalidateElement(passwordFields[1]);
                showElement(errorMessageContainer);

                return false;
            }

            return isValid;
        };

        var serializeForm = function (form) {
            var obj = {};
            var formData = new FormData(form);
            for (var key of formData.keys()) {
                obj[key] = formData.get(key);
            }
            return obj;
        };

        var invalidateElement = function (element) {
            if (element) {

                if (classInvalid) {
                    element.classList.add(...classInvalid);
                }

                //adding data attribute for queries, to be used instead of a class
                element.setAttribute(invalidDataAttr, "");
            }
        };

        var resetValidationErrors = function (parentElement) {
            var invalidElements = parentElement.querySelectorAll(`[${invalidDataAttr}]`);
            invalidElements.forEach(function (element) {
                if (classInvalid) {
                    element.classList.remove(...classInvalid);
                }

                element.removeAttribute(invalidDataAttr);
            });
        };

        var showElement = function (element) {
            if (element) {

                if (visibilityClassHidden) {
                    element.classList.remove(visibilityClassHidden);
                } else {
                    element.style.display = "";
                }
            }
        };

        var hideElement = function (element) {
            if (element) {

                if (visibilityClassHidden) {
                    element.classList.add(visibilityClassHidden);
                } else {
                    element.style.display = "none";
                }
            }
        };

        var isValidEmail = function (email) {
            return /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w+)+$/.test(email);
        };

        var redirect = function (redirectUrl) {
            window.location = redirectUrl;
        };

        function setAntiforgeryTokens() {
            return new Promise((resolve, reject) => {
                let xhr = new XMLHttpRequest();
                xhr.open('GET', '/sitefinity/anticsrf');
                xhr.setRequestHeader('X-SF-ANTIFORGERY-REQUEST', 'true')
                xhr.responseType = 'json';
                xhr.onload = function () {
                    const response = xhr.response;
                    if (response != null) {
                        const token = response.Value;
                        document.querySelectorAll("input[name = 'sf_antiforgery']").forEach(i => i.value = token);
                        resolve();
                    }
                    else {
                        resolve();
                    }
                };
                xhr.onerror = function () { reject(); };
                xhr.send();
            });
        }

        function showError(err) {
            var errorMessageContainer = document.querySelector('[data-sf-role="error-message-container"]');
            errorMessageContainer.innerText = err;
        }

        var userExists = Boolean(widgetContainer.querySelector("input[name='ExistingEmail']").value);
        var activationFailed = Boolean(widgetContainer.querySelector("input[name='ActivationFailed']").value);

        if (activationFailed || userExists) {
            showErrorMessage();
        }
    });
})();
