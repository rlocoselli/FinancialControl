$("#quantity_installment").prop("disabled", !$('#flg_installment').is(':checked'));

if ($('#flg_installment').is(':checked')) {
    $("#quantity_installment").val(1);
}
else {
    $("#quantity_installment").val("");
}


$('#flg_installment').change(function () {
    $("#quantity_installment").prop("disabled", !$(this).is(':checked'));

    if ($(this).is(':checked')) {
        $("#quantity_installment").val(1);
    }
    else
    {
        $("#quantity_installment").val("");
    }

});
