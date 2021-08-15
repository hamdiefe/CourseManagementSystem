(function ($) {
    var _$teacherTabContent = $("#CreateOrEditTeacherModalTabContent");
    var _$teacherForm = _$teacherTabContent.find('form');

    //DeleteAddress
    _$teacherForm.find(".delete-address").click(function () {
        var element = _$teacherForm.find(this);
        element.closest(".address-container").remove();
    });

    //LocationDropdowns
    _$teacherForm.find("select[name^='addressCountry']").on('change', function () {
        var element = _$teacherForm.find(this);
        var url = 'Teachers/AddressCityDropdown?countryId=' + this.value;

        abp.ajax({
            url: url,
            type: 'POST',
            dataType: 'html',
            success: function (content) {

                element.closest(".locationDiv").find(".cityDiv").html(content);
            },
            error: function (e) { }
        });
    });
  
    //Validations
    _$teacherForm.find("textArea[name^='addressDetail']").on('change', function () {
        if (_$teacherForm.find(this).val() != "") {
            _$teacherForm.find(this).removeClass("is-invalid");
            _$teacherForm.find(this).closest(".row").find(".text-danger").remove();
        }
    });

    _$teacherForm.find("select[name^='addressCountry']").on('change', function () {
        _$teacherForm.find(this).closest(".col-sm-9").find(".input-group").removeClass("is-invalid");
        _$teacherForm.find(this).closest(".col-sm-9").find(".text-danger").remove();
    });

    //Select2
    _$teacherForm.find('.js-select2-custom').each(function () {
        var select2 = $.HSCore.components.HSSelect2.init(_$teacherForm.find(this));
    });
})(jQuery);

