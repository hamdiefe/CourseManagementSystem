(function ($) {
    var _$teacherTabContent = $("#CreateOrEditTeacherModalTabContent");
    var _$teacherForm = _$teacherTabContent.find('form');

    //Validations
    _$teacherForm.find("select[name^='addressTown']").on('change', function () {
        _$teacherForm.find(this).closest(".col-sm-9").find(".input-group").removeClass("is-invalid");
        _$teacherForm.find(this).closest(".col-sm-9").find(".text-danger").remove();
    });

    //Select2
    _$teacherForm.find('.js-select2-custom').each(function () {
        var select2 = $.HSCore.components.HSSelect2.init(_$teacherForm.find(this));
    });

})(jQuery);

