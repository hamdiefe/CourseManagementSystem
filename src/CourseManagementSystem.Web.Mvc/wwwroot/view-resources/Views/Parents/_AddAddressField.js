(function ($) {
    var _$parentTabContent = $("#CreateOrEditParentModalTabContent");
    var _$parentForm = _$parentTabContent.find('form');

    //DeleteAddress
    _$parentForm.find(".delete-address").click(function () {
        var element = _$parentForm.find(this);
        element.closest(".address-container").remove();
    });

    //LocationDropdowns
    _$parentForm.find("select[name^='addressCountry']").on('change', function () {
        var element = _$parentForm.find(this);
        var url = 'Parents/AddressCityDropdown?countryId=' + this.value;

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
    _$parentForm.find("textArea[name^='addressDetail']").on('change', function () {
        if (_$parentForm.find(this).val() != "") {
            _$parentForm.find(this).removeClass("is-invalid");
            _$parentForm.find(this).closest(".row").find(".text-danger").remove();
        }
    });

    _$parentForm.find("select[name^='addressCountry']").on('change', function () {
        _$parentForm.find(this).closest(".col-sm-9").find(".input-group").removeClass("is-invalid");
        _$parentForm.find(this).closest(".col-sm-9").find(".text-danger").remove();
    });

    //Select2
    _$parentForm.find('.js-select2-custom').each(function () {
        var select2 = $.HSCore.components.HSSelect2.init(_$parentForm.find(this));
    });
})(jQuery);

