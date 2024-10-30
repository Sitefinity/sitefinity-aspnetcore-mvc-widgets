(function () {
    document.addEventListener('DOMContentLoaded', function () {
        var viewMode = document.querySelector("input[name='ViewMode']").value;
        var visibilityClassElement = document.querySelector('[data-sf-visibility-hidden]');
        var visibilityClassHidden = visibilityClassElement.dataset ? visibilityClassElement.dataset.sfVisibilityHidden : null;
        var readContainer = document.querySelector('[data-sf-role="read-container"]');
        var formContainer = document.querySelector('[data-sf-role="form-container"]');
        var errorMessageContainer = document.querySelector('[data-sf-role="error-message-container"]');
        var successMessageContainer = document.querySelector('[data-sf-role="success-message-container"]');
        var invalidPhotoErrorMessage = formContainer.querySelector("input[name='InvalidPhotoErrorMessage']").value;
        var invalidPasswordErrorMessage = formContainer.querySelector("input[name='InvalidPasswordErrorMessage']").value;
        var confirmEmailChangeRequest = formContainer.querySelector("input[name='ConfirmEmailChangeRequest']").value.toLowerCase() === 'true';
        var confirmEmailChangeError = formContainer.querySelector("input[name='ConfirmEmailChangeError']").value.toLowerCase() === 'true';

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

        var redirect = function (redirectUrl) {
            window.location = redirectUrl;
        };

        var showSuccessMessage = function () {
            hideElement(errorMessageContainer);
            showElement(successMessageContainer);
        };

        var hideMessages = function () {
            hideElement(errorMessageContainer);
            hideElement(successMessageContainer);
        }

        var showErrorMessage = function (message, isHtml) {
            if (message) {
                if (isHtml) {
                    errorMessageContainer.innerHTML = message;
                } else {
                    errorMessageContainer.innerText = message;
                }
            }

            if (errorMessageContainer.innerText) {
                hideElement(successMessageContainer);
                showElement(errorMessageContainer);
            }
        };

        switch (viewMode) {
            case "Edit":
                initEdit(viewMode);
                showElement(formContainer);
                hideElement(readContainer);
                break;
            case "Read":
                initRead(viewMode);
                showElement(readContainer);
                hideElement(formContainer);
                break;
            case "ReadEdit":
                initRead(viewMode);
                initEdit(viewMode);
                showElement(readContainer);
                hideElement(formContainer);
                break;
            default:
                break;
        }

        function initEdit(viewMode) {
            var formContainer = document.querySelector('[data-sf-role="form-container"]');
            var form = formContainer.querySelector("form");
            var fileUploadInput = document.querySelector('[data-sf-role="edit-profile-upload-picture-input"]');
            var editProfileUserImage = document.querySelector('[data-sf-role="sf-user-profile-avatar"]');
            var invalidClassElement = document.querySelector('[data-sf-invalid]');
            var classInvalidValue = invalidClassElement.dataset && isNotEmpty(invalidClassElement.dataset.sfInvalid) ? invalidClassElement.dataset.sfInvalid : null;
            var classInvalid = classInvalidValue ? processCssClass(classInvalidValue) : null;
            var invalidDataAttr = "data-sf-invalid";
            var showPasswordPrompt = false;
            var showConfirmEmailChanges = false;
            var invalidEmailErrorMessage = formContainer.querySelector("input[name='InvalidEmailErrorMessage']").value;
            var validationRequiredErrorMessage = formContainer.querySelector("input[name='ValidationRequiredMessage']").value;
            var activationMethod = formContainer.querySelector("input[name='ActivationMethod']").value;
            var confirmChangeContainer = document.querySelector('[data-sf-role="confirm-email-change-container"]');
            var showSendAgainActivationLink = confirmChangeContainer.querySelector('[data-sf-role="show-send-again-activation-link"]').value.toLowerCase() === 'true';
            var confirmationForm = confirmChangeContainer.querySelector('form');

            form.addEventListener('submit', function (event) {
                event.preventDefault();
                hideMessages();
                if (!validateForm(form)) {
                    return;
                }

                var initialMail = formContainer.querySelector("input[name='InitialEmail']").value;
                var currentEmail = form.querySelector("input[name='Email']").value;

                if (!showPasswordPrompt) {
                    form.querySelector("input[name='Password']").value = "";
                }

                if (initialMail != currentEmail && !showPasswordPrompt) {
                    hideMessages();
                    hideElement(document.querySelector('[data-sf-role="edit-profile-container"]'));
                    showElement(document.querySelector('[data-sf-role="password-container"]'));
                    showPasswordPrompt = true;
                    showConfirmEmailChanges = true;
                    return;
                }

                setAntiforgeryTokens().then(res => {
                    if (validateForm(form)) {
                        submitFormHandler(form, null, postSubmitAction, onSubmitError);
                    }
                }, err => {
                    showErrorMessage("Antiforgery token retrieval failed");
                });
            });

            fileUploadInput.addEventListener('change', function (event) {
                if (event.target.files && event.target.files[0]) {
                    var reader = new FileReader();
                    reader.onload = function (readerLoadedEvent) {
                        editProfileUserImage.src = readerLoadedEvent.target.result;
                    };
                    reader.readAsDataURL(event.target.files[0]);
                }
            });

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
                    showErrorMessage(validationRequiredErrorMessage);

                    return isValid;
                }

                var emailInput = form.querySelector("input[name='Email']");
                if (!isValidEmail(emailInput.value)) {
                    invalidateElement(emailInput);
                    showErrorMessage(invalidEmailErrorMessage);
                    return false;
                }

                if (fileUploadInput.files.length > 0) {
                    if (!validateFile(fileUploadInput.files[0])) {
                        return false;
                    }
                }

                return isValid;
            };

            var isValidEmail = function (email) {
                return /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w+)+$/.test(email);
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

            var submitFormHandler = function (form, url, onSuccess, onError) {
                url = url || form.attributes['action'].value;

                var formData = new FormData();
                for (var i = 0; i < form.elements.length; i++) {
                    var field = form.elements[i];

                    if (field.type === 'submit') {
                        continue;
                    }

                    if (field.type === 'file') {
                        formData.append('image', field.files[0])
                        continue;
                    }

                    formData.append(field.name, field.value);
                }

                window.fetch(url, { method: 'POST', body: formData })
                    .then((response) => {
                        var status = response.status;
                        if (status === 0 || (status >= 200 && status < 400)) {
                            if (onSuccess) {
                                onSuccess(response);
                            }
                        } else {
                            response.json().then((res) => {
                                var message = res.error.message;
                                var fieldsErrors = res.error.fieldsErrors;

                                if (onError) {
                                    onError(message, fieldsErrors, status);
                                }
                            });
                        }
                    })
                    .finally(() => {
                        if (showPasswordPrompt) {
                            formContainer.querySelector('input[name=Password]').value = '';
                            showElement(document.querySelector('[data-sf-role="edit-profile-container"]'));
                            hideElement(document.querySelector('[data-sf-role="password-container"]'));
                            showPasswordPrompt = false;
                        }
                    });
            };

            var bind = function (response) {
                response.json().then(res => {
                    var model = res.value;

                    formContainer.querySelector("input[name='FirstName']").value = model.FirstName;
                    formContainer.querySelector("input[name='LastName']").value = model.LastName;
                    formContainer.querySelector("input[name='Email']").value = model.Email;
                    formContainer.querySelector("input[name='Nickname']").value = model.Nickname;
                    formContainer.querySelector("textarea[name='About']").value = model.About;
                    var avatar = formContainer.querySelector("img[data-sf-role='sf-user-profile-avatar']");
                    avatar.src = model.AvatarUrl;
                    avatar.alt = model.Email;
                    formContainer.querySelector("input[name='InitialEmail']").value = model.Email;
                });
            }

            var postSubmitAction = function (response) {
                if (showConfirmEmailChanges && activationMethod == "AfterConfirmation") {
                    hideElement(document.querySelector('[data-sf-role="profile-container"]'));
                    showElement(document.querySelector('[data-sf-role="confirm-email-change-container"]'));
                } else {
                    var action = formContainer.querySelector("input[name='PostUpdateAction']").value;

                    switch (action) {
                        case "ViewMessage":
                            bind(response);
                            hideElement(errorMessageContainer);
                            showSuccessMessage();
                            break;
                        case "RedirectToPage":
                            var redirectUrl = formContainer.querySelector("input[name='RedirectUrl']").value;
                            redirect(redirectUrl);
                            break;
                        case "SwitchToReadMode":
                            hideElement(formContainer);
                            showElement(readContainer);
                            redirect(window.location);
                            break;
                        default:
                            break;
                    }
                }
            };

            var onSubmitError = function (errorMessage, responseFieldsErrors, status) {
                switch (status) {
                    case 403: errorMessage = invalidPasswordErrorMessage;
                }

                var fieldErrors = [];

                if (errorMessage) {
                    fieldErrors.push(errorMessage);
                }

                // Profile fields invalidation
                if (responseFieldsErrors) {
                    Object.keys(responseFieldsErrors).forEach(key => {
                        var inputElement = formContainer.querySelector("input[name='" + key + "']") ?? formContainer.querySelector("textarea[name='" + key + "']") ?? formContainer.querySelector("img[name='" + key + "']");
                        if (inputElement) {
                            invalidateElement(inputElement);

                            if (inputElement.id) {
                                var fieldName = formContainer.querySelector("label[for='" + inputElement.id + "']").innerText;
                                fieldErrors.push(responseFieldsErrors[key].replace("{0}", fieldName));
                            } else {
                                fieldErrors.push(responseFieldsErrors[key]);
                            }
                        }
                    });
                }

                showErrorMessage(fieldErrors.join('<br />'), true);
            };

            var confirmationSuccess = function () {
                confirmChangeContainer.querySelector('[data-sf-role="confirm-email-change-title"] h2').innerText = confirmChangeContainer.querySelector('[data-sf-role="confirm-email-change-success-title"]').value;
                confirmChangeContainer.querySelector('[data-sf-role="confirm-email-change-message"]').innerText = confirmChangeContainer.querySelector('[data-sf-role="confirm-email-change-success-message"]').value;
                confirmationForm.querySelector('[type="submit"]').value = confirmChangeContainer.querySelector('[data-sf-role="send-again-label"]').value;
            }

            if (confirmEmailChangeRequest && activationMethod == "AfterConfirmation") {
                if (confirmEmailChangeError) {
                    hideElement(document.querySelector('[data-sf-role="profile-container"]'));
                    showElement(confirmChangeContainer);

                    if (showSendAgainActivationLink) {
                        confirmationForm.addEventListener('submit', function (event) {
                            event.preventDefault();

                            setAntiforgeryTokens().then(res => {
                                submitFormHandler(confirmationForm, null, confirmationSuccess);
                            }, err => {
                                showErrorMessage("Antiforgery token retrieval failed");
                            });
                        });
                        showElement(confirmationForm.querySelector('[type="submit"]'));
                    }
                    return;
                } else {
                    showSuccessMessage();
                }
            }
        }

        function initRead(viewMode) {
            if (viewMode === "ReadEdit") {
                var editProfileLink = document.querySelector('[data-sf-role="editProfileLink"]');

                editProfileLink.addEventListener('click', function (event) {
                    event.preventDefault();
                    hideElement(readContainer);
                    showElement(formContainer);
                });
            }
        }

        function isNotEmpty(attr) {
            return (attr && attr !== "");
        }

        function processCssClass(str) {
            var classList = str.split(" ");
            return classList;
        }

        function validateFile(file) {
            var fileSize = file.size;
            var maxSize = 25 * 1024 * 1024;
            var allowedExtensions = formContainer.querySelector('input[data-sf-role="sf-allowed-avatar-formats"]').value;
            var fileName = file.name;
            var fileExtension = fileName.substr(fileName.lastIndexOf('.')).toLowerCase();

            if (fileSize > maxSize || !allowedExtensions.split(",").map(x => x.trim()).includes(fileExtension)) {
                showErrorMessage(invalidPhotoErrorMessage.replace('{0}', maxSize).replace('{1}', allowedExtensions));
                return false;
            }

            return true;
        }

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
    });
})();
