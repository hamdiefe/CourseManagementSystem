(function ($) {
    var _$studentTabContent = $("#CreateOrEditStudentModalTabContent");
    var _$studentForm = _$studentTabContent.find('form');

    //Validations
    _$studentForm.find("select[name^='addressTown']").on('change', function () {
        _$studentForm.find(this).closest(".col-sm-9").find(".input-group").removeClass("is-invalid");
        _$studentForm.find(this).closest(".col-sm-9").find(".text-danger").remove();
    });

    //Select2
    _$studentForm.find('.js-select2-custom').each(function () {
        var select2 = $.HSCore.components.HSSelect2.init(_$studentForm.find(this));
    });

})(jQuery);

