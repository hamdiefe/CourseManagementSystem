(function ($) {
    var _mySettingsService = abp.services.app.mySettings,
        l = abp.localization.getSource('CourseManagementSystem'),
        _$form = $('#ProfileImageForm');

    function save() {
        if (!_$form.valid()) {
            return;
        }

        var profileImageDto = _$form.serializeFormToObject();

        var fileUpload = $('#avatarUploader').get(0);
        var files = fileUpload.files;
        var data = new FormData();
        data.append("dosya", files[0]);
        $.ajax({
            type: "POST",
            url: "/MySettings/UploadImage",
            contentType: false,
            processData: false,
            data: data,
            async: false,
            beforeSend: function () {
            },
            success: function (message) {
                console.log(message);
                if (message.result != "False") {
                    profileImageDto.profileImage = message.result;
                }
            },
            error: function () {
                alert("Resim Yüklenemedi.");
            },
            complete: function () {
            }
        });


        abp.ui.setBusy(_$form);
        var skipClearBusy = false;
        _mySettingsService.updateProfileImage(profileImageDto).done(success => {
            if (success) {
                skipClearBusy = true;
                abp.notify.info(l('SavedSuccessfully'));
                abp.event.trigger('mySettings.profileImageSaved');
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