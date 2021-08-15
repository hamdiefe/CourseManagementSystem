(function ($) {
    var _$studentTabContent = $("#CreateOrEditStudentModalTabContent");
    var _$studentForm = _$studentTabContent.find('form');

    //DeleteAddress
    _$studentForm.find(".delete-address").click(function () {
        var element = _$studentForm.find(this);
        element.closest(".address-container").remove();
    });

    //LocationDropdowns
    _$studentForm.find("select[name^='addressCountry']").on('change', function () {
        var element = _$studentForm.find(this);
        var url = 'Students/AddressCityDropdown?countryId=' + this.value;

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
    _$studentForm.find("textArea[name^='addressDetail']").on('change', function () {
        if (_$studentForm.find(this).val() != "") {
            _$studentForm.find(this).removeClass("is-invalid");
            _$studentForm.find(this).closest(".row").find(".text-danger").remove();
        }
    });

    _$studentForm.find("select[name^='addressCountry']").on('change', function () {
        _$studentForm.find(this).closest(".col-sm-9").find(".input-group").removeClass("is-invalid");
        _$studentForm.find(this).closest(".col-sm-9").find(".text-danger").remove();
    });

    //Select2
    _$studentForm.find('.js-select2-custom').each(function () {
        var select2 = $.HSCore.components.HSSelect2.init(_$studentForm.find(this));
    });
})(jQuery);

