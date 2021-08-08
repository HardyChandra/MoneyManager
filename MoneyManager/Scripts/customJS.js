function confirmDelete(isDeleteClicked) {
    var deleteSpan = 'deleteSpan';
    var confirmDeleteSpan = 'confirmDeleteSpan';

    if (isDeleteClicked) {
        $('#' + deleteSpan).hide();
        $('#' + confirmDeleteSpan).show();
    }
    else {
        $('#' + deleteSpan).show();
        $('#' + confirmDeleteSpan).hide();
    }
}

function confirmUpdate(isUpdateClicked) {
    var updateSpan = 'updateSpan';
    var confirmUpdateSpan = 'confirmUpdateSpan';

    if (isUpdateClicked) {
        $('#' + updateSpan).hide();
        $('#' + confirmUpdateSpan).show();
    }
    else {
        $('#' + updateSpan).show();
        $('#' + confirmUpdateSpan).hide();
    }
}