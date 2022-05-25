$(document).ready(function () {
    try{
        $.validator.methods.date = function (value, element) {
            return this.optional(element) || parseDate(value, "dd/MM/yyyy") !== null;
        };
    }
    catch(e)
    {

    }
});