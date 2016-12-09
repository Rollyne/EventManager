$(function () {
    jQuery.validator.setDefaults({
        debug: true,
        success: "valid"
    });
    //ADDING METHODS TO THE VALIDATE
    $.validator.addMethod('minValue', function (value, el, param) {
        return value >= param;
    }, 'The number should be at least {0}');
    $.validator.addMethod('maxValue', function (value, el, param) {
        return value <= param;
    }, "The number shouldn't be more than {0}");
    $.validator.addMethod("regex", function (value, element, regexpr) {
        return regexpr.test(value);
    }, "The number shouldn't be more than {0}");
    $.validator.addMethod("test", function (value, element, param) {
        return true;
    });

    //VALIDATIONS
    $("#createEvent").validate({
        rules: {
            Destination: {
                required: true,
                minlength: 3,
                maxlength: 30
            },
            Content: {
                required: false,
                maxlength: 1000
            },
            EventLength: {
                required: true,
                minlength: 1,
                number: true,
                minValue: 1
            },
            hour: {
                required: false,
                number: true,
                maxValue: 24,
                minValue: 0
            },
            minutes: {
                required: false,
                number: true,
                maxValue: 60,
                minValue: 0
            },
            fromDate: {
                required: true
            },
            toDate: {
                required: true
            }

        },
        submitHandler: function (form) {
            form.submit();
        }
    });
    $('#regForm').validate({
        rules: {
            Email: {
                required: true,
                regex: /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/

            },
            Password: {
                required: true,
                minlength: 6

            },
            
            "ConfirmPassword": {
                equalTo: "#regPassword"
            }
        },
        submitHandler: function (form) {
            form.submit();
        }
    });
    $('#formLogin').validate({
        rules: {
            Email: {
                required: true,
                regex: /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/

            },
            Password: {
                required: true,
                minlength: 6

            }
        },
        submitHandler: function (form) {
            form.submit();
        }
    });
    jQuery.extend(jQuery.validator.messages, {
        required: function (param, input) {
            switch (input.name) {
                case "Destination": return 'The title field is required';
                case "EventLength": return 'The duration field is required'
                default: return 'The ' + input.name + ' field is required';
            }
            
        },
        minValue: function (param, input) {
            if (input.name == 'EventLength') {
                return 'The ' + input.name + ' should be at least ' + param + ' day long';
            }
            if (input.name == 'hour') {
                return 'The ' + input.name + 's should be at least ' + param;
            }
            if (input.name == 'minutes') {
                return 'The ' + input.name + ' should be at least ' + param;
            }
        },
        maxValue: function (param, input) {
            if (input.name == 'EventLength') {
                return 'The ' + input.name + " shouldn't be more than " + param + ' days long';
            }
            if (input.name == 'hour') {
                return 'The ' + input.name + "s shouldn't be more than " + param;
            }
            if (input.name == 'minutes') {
                return 'The ' + input.name + " shouldn't be more than " + param;
            }
        },
        equalTo: function (param, input) {
            if (input.name == 'ConfirmPassword') {
                return 'The passwords do not match';
            } else {
                return 'The values don not match';
            }
        },
        regex: function (param, input) {
            return 'Please enter a valid ' + input.name;
        },
        number: function (param, input) {
            return 'The ' + input.name + ' should be a valid number'
        }
    });
});