(function ($) {
    var l = abp.localization.getSource('CourseManagementSystem');
    var _gradesService = abp.services.app.grades;
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

        var grade = _$form.serializeFormToObject();

        abp.ui.setBusy(_$form);
        _gradesService.createOrEdit(
            grade
        ).done(function () {
            _$modal.modal('hide');
            abp.notify.info(l('SavedSuccessfully'));
            abp.event.trigger('grade.createOrEditGradeModalSaved');
        }).always(function () {
            abp.ui.clearBusy(_$form);
        });
    }
})(jQuery);

