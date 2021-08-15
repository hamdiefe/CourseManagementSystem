(function ($) {
    var l = abp.localization.getSource('CourseManagementSystem');
    var _documentsService = abp.services.app.documents;
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

        var document = _$form.serializeFormToObject();

        abp.ui.setBusy(_$form);
        _documentsService.createOrEdit(
            document
        ).done(function () {
            _$modal.modal('hide');
            abp.notify.info(l('SavedSuccessfully'));
            abp.event.trigger('document.createOrEditGradeModalSaved');
        }).always(function () {
            abp.ui.clearBusy(_$form);
        });
    }

    function mask() {
        _$form.find('.js-masked-input').each(function () {
            var mask = $.HSCore.components.HSMask.init(_$studentForm.find(this));
        });
        $("#Document_Debt").maskMoney({ thousands: '.', decimal: ',' });
        $("#Document_Credit").maskMoney({ thousands: '.', decimal: ',' });
        $("#Document_Remaining").maskMoney({ thousands: '.', decimal: ',' });
    }

    //Flatpckr
    _$form.find('.js-flatpickr').each(function () {
        $.HSCore.components.HSFlatpickr.init(_$form.find(this));
    });

    //Select2
    _$form.find('.js-select2-custom').each(function () {
        var select2 = $.HSCore.components.HSSelect2.init(_$form.find(this));
    });

    //Mask inputs 
    mask();
})(jQuery);

