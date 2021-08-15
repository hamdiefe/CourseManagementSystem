(function ($) {
    var l = abp.localization.getSource('CourseManagementSystem');
    var _parentTypesService = abp.services.app.parentTypes;
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

        var parentType = _$form.serializeFormToObject();

        abp.ui.setBusy(_$form);
        _parentTypesService.createOrEdit(
            parentType
        ).done(function () {
            _$modal.modal('hide');
            abp.notify.info(l('SavedSuccessfully'));
            abp.event.trigger('parentType.createOrEditParentTypeModalSaved');
        }).always(function () {
            abp.ui.clearBusy(_$form);
        });
    }

    //Select2
    $('.js-select2-custom').each(function () {
        var select2 = $.HSCore.components.HSSelect2.init($(this));
    });
})(jQuery);

