// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var selectedProductId = null;

function validateText(text, maxLength, minLength = 1, spacesAllowed = true) {
    if (!text || text.length < minLength || text.length > maxLength) {
        return false;
    }
    if (!spacesAllowed) {
        if (text.includes(' ')) {
            return false;
        }
    }
    return true;
};

function validateTextAlphaNumeric(text, maxLength, minLength = 1, spacesAllowed = true) {
    if (!validateText(text, maxLength, minLength, spacesAllowed)) {
        return false;
    }
    if (!text.match(/^[a-zA-Z0-9 ]+$/)) {
        return false;
    }
    return true;
};

function validateTextAlpha(text, maxLength, minLength = 1, spacesAllowed = true) {
    if (!validateText(text, maxLength, minLength, spacesAllowed)) {
        return false;
    }
    if (!text.match(/^[a-zA-Z ]+$/)) {
        return false;
    }
    return true;
};

function validateTextNumeric(text, maxLength, minLength = 1, spacesAllowed = true) {
    if (!validateText(text, maxLength, minLength, spacesAllowed)) {
        return false;
    }
    if (!text.match(/^[0-9]+$/)) {
        return false;
    }
    return true;
}

function validateEmail(email) {
    if (!email.match(/^[a-zA-Z0-9]+@[a-zA-Z0-9]+\.[a-zA-Z0-9]+$/)) {
        return false;
    }
    return true;
}

function checkPasswordStrength(password) {
    if (!password.match(/^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*()_+.])/)) {
        return false;
    }
    return true;
}

function validateTextInput(input, minLen, maxLen, message, messageSpan) {
    let category = $(input).val();
    let valid = validateTextAlpha(category, maxLen, minLen, true);
    if (valid) {
        $(input).removeClass('is-invalid');
        if (message && messageSpan) {
            $(messageSpan).text('');
        }
        return true;
    }
    else {
        $(input).addClass('is-invalid');
        if (message && messageSpan) {
            $(messageSpan).text(message);
        }
        return false;
    }
}

function validateNumberInput(input, min, max, message, messageSpan) {
    let content = $(input).val();
    console.log(typeof content === "undefined");
    if (typeof content === "undefined" || isNaN(content) || content < min || content > max) {
        $(input).addClass('is-invalid');
        if (message && messageSpan) {
            $(messageSpan).text(message);
        }
        return false;
    }
    else {
        $(input).removeClass('is-invalid');
        if (message && messageSpan) {
            $(messageSpan).text('');
        }
        return true;
    }
}

$('.products-table').on('click', 'tbody tr', function (event) {
    $(this).addClass('table-primary').siblings().removeClass('table-primary');
    $('#modify-button').removeClass('disabled');
    $('#delete-button').removeClass('disabled');

    selectedProductId = $(this).attr('id');
    let name = $(this).find('td[name="name"]').text();
    let brandId = $(this).find('td[name="brand"]').attr('brandId');
    let category = $(this).find('td[name="category"]').text();
    let price = $(this).find('td[name="price"]').text();
    let stock = $(this).find('td[name="stock"]').text();
    let description = $(this).find('td[name="description"]').text();
    let size = $(this).find('td[name="size"]').text();
    let targetId = $(this).find('td[name="target"]').attr('targetId');

    $('#modify-input-id').val(selectedProductId);
    $('#modify-input-name').val(name);
    $('#modify-input-brand').val(brandId).change();
    $('#modify-input-category').val(category);
    $('#modify-input-price').val(price);
    $('#modify-input-stock').val(stock);
    $('#modify-texarea-description').val(description);
    $('#modify-input-size').val(size);
    $('#modify-input-target').val(targetId).change();
});

$('.pagination-page').click(function (e) {
    let number = $(this).text();
    if (!isNaN(number)) {
        $('#pageInput').val(number);
        $('#paramsForm').submit();
    }
    else {
        alert("What are you doing??");
    }
});

$('.pagination-previous').click(function (e) {
    let number = $('#pageInput').val();
    if (!isNaN(number)) {
        $('#pageInput').val(--number);
        $('#paramsForm').submit();
    }
    else {
        alert("What are you doing??");
    }
});

$('.pagination-next').click(function (e) {
    let number = $('#pageInput').val();
    if (!isNaN(number)) {
        $('#pageInput').val(++number);
        $('#paramsForm').submit();
    }
    else {
        alert("What are you doing??");
    }
});

$('#search-button').click(function (e) {
    $('#searchInput').val($('#searchTextInput').val());
    $('#paramsForm').submit();
});

$('#modify-input-name').keyup(function () {
    validateTextInput(this, 1, 20, 'Nombre inválido: debe ser de entre 1 y 20 caracteres.', '#modify-form-message');
});

$('#register-input-name').keyup(function () {
    validateTextInput(this, 1, 20, 'Nombre inválido: debe ser de entre 1 y 20 caracteres.', '#register-form-message');
});

$('#modify-input-category').keyup(function () {
    validateTextInput(this, 1, 25, 'Categoría inválida: debe ser de entre 1 y 25 caracteres.', '#modify-form-message');
});

$('#register-input-category').keyup(function () {
    validateTextInput(this, 1, 25, 'Categoría inválida: debe ser de entre 1 y 25 caracteres.', '#register-form-message');
});

$('#modify-texarea-description').keyup(function () {
    validateTextInput(this, 1, 150, 'Descripción inválida: debe ser de entre 1 y 150 caracteres.', '#modify-form-message');
});

$('#register-texarea-description').keyup(function () {
    validateTextInput(this, 1, 150, 'Descripción inválida: debe ser de entre 1 y 150 caracteres.', '#register-form-message');
});

$('#modify-input-price').keyup(function () {
    validateNumberInput(this, 1, 999999, 'Precio inválido: debe ser un número entre 0 y 999999.', '#modify-form-message');
});

$('#register-input-price').keyup(function () {
    validateNumberInput(this, 1, 999999, 'Precio inválido: debe ser un número entre 0 y 999999.', '#register-form-message');
});

$('#modify-input-stock').keyup(function () {
    validateNumberInput(this, 1, 999999, 'Stock inválido: debe ser un número entre 0 y 999999.', '#modify-form-message');
});

$('#register-input-stock').keyup(function () {
    validateNumberInput(this, 1, 999999, 'Stock inválido: debe ser un número entre 0 y 999999.', '#register-form-message');
});

$('#modify-input-size').keyup(function () {
    validateNumberInput(this, 1, 999999, 'Talle inválido: debe ser un número entre 0 y 999999.', '#modify-form-message');
});

$('#register-input-size').keyup(function () {
    validateNumberInput(this, 1, 999999, 'Talle inválido: debe ser un número entre 0 y 999999.', '#register-form-message');
});

$('#modify-form-submit').click(function (e) {
    e.preventDefault();

    let valid = true;
    valid = validateTextInput('#modify-input-name', 1, 20) && valid;
    valid = validateTextInput('#modify-input-category', 1, 25) && valid;
    valid = validateTextInput('#modify-texarea-description', 1, 150) && valid;
    valid = validateNumberInput('modify-input-price', 1, 999999) && valid;
    valid = validateNumberInput('modify-input-stock', 1, 999999) && valid;
    valid = validateNumberInput('modify-input-size', 1, 999999) && valid;

    if (valid) {
        $(this).click();
    }
    else {
        $('#modify-form-message').text('Campos inválidos');
    }
});

$('#register-form-submit').click(function (e) {
    e.preventDefault();

    let valid = true;
    valid = validateTextInput('#register-input-name', 1, 20) && valid;
    valid = validateTextInput('#register-input-category', 1, 25) && valid;
    valid = validateTextInput('#register-texarea-description', 1, 150) && valid;
    valid = validateNumberInput('register-input-price', 1, 999999) && valid;
    valid = validateNumberInput('register-input-stock', 1, 999999) && valid;
    valid = validateNumberInput('register-input-size', 1, 999999) && valid;

    if (valid) {
        $(this).click();
    }
    else {
        $('#register-form-message').text('Campos inválidos');
    }
});
