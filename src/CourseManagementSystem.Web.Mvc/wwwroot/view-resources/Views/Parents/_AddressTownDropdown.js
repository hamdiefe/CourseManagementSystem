(function ($) {
    var _$parentTabContent = $("#CreateOrEditParentModalTabContent");
    var _$parentForm = _$parentTabContent.find('form');

    //Validations
    _$parentForm.find("select[name^='addressTown']").on('change', function () {
        _$parentForm.find(this).closest(".col-sm-9").find(".input-group").removeClass("is-invalid");
        _$parentForm.find(this).closest(".col-sm-9").find(".text-danger").remove();
    });

    //Select2
    _$parentForm.find('.js-select2-custom').each(function () {
        var select2 = $.HSCore.components.HSSelect2.init(_$parentForm.find(this));
    });

})(jQuery);

