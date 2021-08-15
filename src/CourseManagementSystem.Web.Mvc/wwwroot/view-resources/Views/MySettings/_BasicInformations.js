(function ($) {
    var _mySettingsService = abp.services.app.mySettings,
        l = abp.localization.getSource('CourseManagementSystem'),
        _$form = $('#BasicInformationsForm');

    function save() {
        if (!_$form.valid()) {
            return;
        }

        var basicInformationsDto = _$form.serializeFormToObject();

        abp.ui.setBusy(_$form);
        var skipClearBusy = false;
        _mySettingsService.updateBasicInformations(basicInformationsDto).done(success => {
            if (success) {
                skipClearBusy = true;
                abp.notify.info(l('SavedSuccessfully'));
                abp.event.trigger('mySettings.basicInformationsSaved');
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