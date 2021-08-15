(function ($) {
    var _mySettingsService = abp.services.app.mySettings,
        l = abp.localization.getSource('CourseManagementSystem'),
        _$form = $('#ChangePasswordForm');

    $.validator.addMethod("regex", function (value, element, regexpr) {
        return regexpr.test(value);
    }, l("PasswordsMustBeAtLeast8CharactersContainLowercaseUppercaseNumber"));

    _$form.validate({
        rules: {
            NewPassword: {
                regex: /(?=^.{8,}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s)[0-9a-zA-Z!@#$%^&*()]*$/
            },
            ConfirmNewPassword: {
                equalTo: "#NewPassword"
            }
        },
        messages: {
            ConfirmNewPassword: {
                equalTo: l("PasswordsDoNotMatch")
            }
        }
    });

    function save() {
        if (!_$form.valid()) {
            return;
        }

        var changePasswordDto = _$form.serializeFormToObject();

        abp.ui.setBusy(_$form);
        var skipClearBusy = false;
        _mySettingsService.changePassword(changePasswordDto).done(success => {
            if (success) {
                skipClearBusy = true;
                abp.notify.info(l('SavedSuccessfully'));
                abp.event.trigger('mySettings.passwordSaved');
                abp.ui.clearBusy(_$form);
            }
        }).always(function () {
            if (!skipClearBusy) {
                abp.ui.clearBusy(_$form);
            }
        });
    }

    _$form.submit(function (e) {
        e.preventDefault();
        save();
    });

})(jQuery);