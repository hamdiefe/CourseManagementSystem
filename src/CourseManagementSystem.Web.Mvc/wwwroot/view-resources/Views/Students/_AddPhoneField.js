(function ($) {  
    var _$studentTabContent = $("#CreateOrEditStudentModalTabContent");
    var _$studentForm = _$studentTabContent.find('form');

    //DeletePhone
    _$studentForm.find(".delete-phone").click(function () {
        var element = _$studentForm.find(this);
        element.closest(".phone-container").remove();
    });

    //Validations
    _$studentForm.find("input[name^='phoneNumber']").on('change', function () {
        if (_$studentForm.find(this).val() != "" && _$studentForm.find(this).val().length == 15) {
            _$studentForm.find(this).removeClass("is-invalid");
            _$studentForm.find(this).closest(".phone-container").find(".input-group-prepend").removeClass("is-invalid");
            _$studentForm.find(this).closest(".phone-container").find(".input-group-append").removeClass("is-invalid");
            _$studentForm.find(this).closest(".phone-container").find(".text-danger").remove();
        }
    });

    //Select2
    _$studentForm.find('.js-select2-custom').each(function () {
        var select2 = $.HSCore.components.HSSelect2.init(_$studentForm.find(this));
    });
})(jQuery);

