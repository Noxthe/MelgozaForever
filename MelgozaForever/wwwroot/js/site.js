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
