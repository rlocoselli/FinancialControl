/*
 * Localized default methods for the jQuery validation plugin.
 * Locale: PT_BR
 */
jQuery.extend(jQuery.validator.methods, {
	date: function (value, element) {
		return this.optional(element) || /^\d\d?\/\d\d?\/\d\d\d?\d?$/.test(value);
	},
	number: function (value, element) {
		return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:\.\d{3})+)(?:,\d+)?$/.test(value);
	}
});

$(document).ready(function(){
    try {
        $(".numeric").inputmask({ 'mask': "9{0,5},9{0,2}", greedy: false, rightAlign:true });
    }
    catch(e)
    {
        alert(e);
    }
});