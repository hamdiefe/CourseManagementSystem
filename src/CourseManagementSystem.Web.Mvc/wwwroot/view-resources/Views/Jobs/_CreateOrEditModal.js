(function ($) {
    var l = abp.localization.getSource('CourseManagementSystem');
    var _jobsService = abp.services.app.jobs;
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

        var job = _$form.serializeFormToObject();

        abp.ui.setBusy(_$form);
        _jobsService.createOrEdit(
            job
        ).done(function () {
            _$modal.modal('hide');
            abp.notify.info(l('SavedSuccessfully'));
            abp.event.trigger('job.createOrEditJobModalSaved');
        }).always(function () {
            abp.ui.clearBusy(_$form);
        });
    }
})(jQuery);

