(function ($) {
    var l = abp.localization.getSource('CourseManagementSystem');
    var _schoolsService = abp.services.app.schools;
    var _$modal = $('#CreateOrEditModal');
    var _$form = _$modal.find('form');

    _$form.closest('div.modal-content').find(".save-button").click(function (e) {
        e.preventDefault();
        save();
    });

    function save() {
        if (!_$form.valid()) {
            return;
        }

        var school = _$form.serializeFormToObject();

        abp.ui.setBusy(_$form);
        _schoolsService.createOrEdit(
            school
        ).done(function () {
            _$modal.modal('hide');
            abp.notify.info(l('SavedSuccessfully'));
            abp.event.trigger('school.createOrEditSchoolModalSaved');
        }).always(function () {
            abp.ui.clearBusy(_$form);
        });
    }
})(jQuery);

