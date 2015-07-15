jQuery.validator.addMethod("dropdownrule",
   function (value, element, params) {

       if (value == "-1") {
           return false;
       }
       return true;
   });

jQuery.validator.unobtrusive.adapters.add("dropdownrule", function (options) {
    options.rules["dropdownrule"] = {
    };
    options.messages["dropdownrule"] = options.message;
});