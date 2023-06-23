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
    let regex = /^[a-zA-Z0-9À-ÿ\u00f1\u00d1\u0020\u0021\u0022\u0023\u0024\u0025\u0026\u0027\u0028\u0029\u002a\u002b\u002c\u002d\u002e\u002f\u003a\u003b\u003c\u003d\u003e\u003f\u0040\u005b\u005c\u005d\u005e\u005f\u0060\u007b\u007c\u007d\u007e ]+$/;
    if (!text.match(regex)) {
        return false;
    }
    return true;
};

function validateTextInput(input, minLen, maxLen, message, messageSpan) {
    let category = $(input).val();
    let valid = validateTextAlphaNumeric(category, maxLen, minLen, true);
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
    if (isNaN(content) || content < min || content > max) {
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

function validateSelectInput(input, message, messageSpan) {
    let content = $(input).val();
    if (content == 'Elige...') {
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
    validateTextInput(this, 1, 50, 'Nombre inválido: debe ser de entre 1 y 50 caracteres.', '#modify-form-message');
});

$('#register-input-name').keyup(function () {
    validateTextInput(this, 1, 50, 'Nombre inválido: debe ser de entre 1 y 50 caracteres.', '#register-form-message');
});

$('#modify-input-brand').change(function () {
    validateSelectInput(this, 'Por favor seleccione una marca de la lista.', '#modify-form-message');
});

$('#register-input-brand').change(function () {
    validateSelectInput(this, 'Por favor seleccione una marca de la lista.', '#register-form-message');
});

$('#modify-input-category').keyup(function () {
    validateTextInput(this, 1, 50, 'Categoría inválida: debe ser de entre 1 y 50 caracteres.', '#modify-form-message');
});

$('#register-input-category').keyup(function () {
    validateTextInput(this, 1, 50, 'Categoría inválida: debe ser de entre 1 y 50 caracteres.', '#register-form-message');
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

$('#modify-input-target').change(function () {
    validateSelectInput(this, 'Por favor seleccione el publico objetivo.', '#modify-form-message');
});

$('#register-input-target').change(function () {
    validateSelectInput(this, 'Por favor seleccione el publico objetivo.', '#register-form-message');
});

$('#modify-form-submit').click(function (e) {
    let valid = true;
    valid = validateTextInput('#modify-input-name', 1, 50) && valid;
    valid = validateSelectInput('#modify-input-brand') && valid;
    valid = validateTextInput('#modify-input-category', 1, 50) && valid;
    valid = validateTextInput('#modify-texarea-description', 1, 150) && valid;
    valid = validateNumberInput('#modify-input-price', 1, 999999) && valid;
    valid = validateNumberInput('#modify-input-stock', 1, 999999) && valid;
    valid = validateNumberInput('#modify-input-size', 1, 999999) && valid;
    valid = validateSelectInput('#modify-input-target') && valid;

    if (!valid) {
        e.preventDefault();
        $('#modify-form-message').text('Campos inválidos');
    }
});

$('#register-form-submit').click(function (e) {
    let valid = true;
    valid = validateTextInput('#register-input-name', 1, 50) && valid;
    valid = validateSelectInput('#register-input-brand') && valid;
    valid = validateTextInput('#register-input-category', 1, 50) && valid;
    valid = validateTextInput('#register-texarea-description', 1, 150) && valid;
    valid = validateNumberInput('#register-input-price', 1, 999999) && valid;
    valid = validateNumberInput('#register-input-stock', 1, 999999) && valid;
    valid = validateNumberInput('#register-input-size', 1, 999999) && valid;
    valid = validateSelectInput('#register-input-target') && valid;

    if (!valid) {
        e.preventDefault();
        $('#register-form-message').text('Campos inválidos');
    }
});

$('#delete-form-submit').click(function (e) {
    if (selectedProductId != null) {
        $('#delete-form-product-id').val(selectedProductId);
        $('#delete-form').submit();
    }
});

$(document).ready(function () {
    if ($('#messageModalTrigger').length) {
        $('#messageModalTrigger').click();
    }
});
