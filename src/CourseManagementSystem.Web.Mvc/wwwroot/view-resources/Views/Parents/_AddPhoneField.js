(function ($) {  
    var _$parentTabContent = $("#CreateOrEditParentModalTabContent");
    var _$parentForm = _$parentTabContent.find('form');

    //DeletePhone
    _$parentForm.find(".delete-phone").click(function () {
        var element = _$parentForm.find(this);
        element.closest(".phone-container").remove();
    });

    //Validations
    _$parentForm.find("input[name^='phoneNumber']").on('change', function () {
        if (_$parentForm.find(this).val() != "" && _$parentForm.find(this).val().length == 15) {
            _$parentForm.find(this).removeClass("is-invalid");
            _$parentForm.find(this).closest(".phone-container").find(".input-group-prepend").removeClass("is-invalid");
            _$parentForm.find(this).closest(".phone-container").find(".input-group-append").removeClass("is-invalid");
            _$parentForm.find(this).closest(".phone-container").find(".text-danger").remove();
        }
    });

    //Select2
    _$parentForm.find('.js-select2-custom').each(function () {
        var select2 = $.HSCore.components.HSSelect2.init(_$parentForm.find(this));
    });
})(jQuery);

