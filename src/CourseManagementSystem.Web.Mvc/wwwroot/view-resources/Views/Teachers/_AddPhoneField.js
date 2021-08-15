(function ($) {  
    var _$teacherTabContent = $("#CreateOrEditTeacherModalTabContent");
    var _$teacherForm = _$teacherTabContent.find('form');

    //DeletePhone
    _$teacherForm.find(".delete-phone").click(function () {
        var element = _$teacherForm.find(this);
        element.closest(".phone-container").remove();
    });

    //Validations
    _$teacherForm.find("input[name^='phoneNumber']").on('change', function () {
        if (_$teacherForm.find(this).val() != "" && _$teacherForm.find(this).val().length == 15) {
            _$teacherForm.find(this).removeClass("is-invalid");
            _$teacherForm.find(this).closest(".phone-container").find(".input-group-prepend").removeClass("is-invalid");
            _$teacherForm.find(this).closest(".phone-container").find(".input-group-append").removeClass("is-invalid");
            _$teacherForm.find(this).closest(".phone-container").find(".text-danger").remove();
        }
    });

    //Select2
    _$teacherForm.find('.js-select2-custom').each(function () {
        var select2 = $.HSCore.components.HSSelect2.init(_$teacherForm.find(this));
    });
})(jQuery);

