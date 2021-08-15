(function ($) {
    var _$parentTabContent = $("#CreateOrEditParentModalTabContent");
    var _$parentForm = _$parentTabContent.find('form');

    //LocationDropdowns
    _$parentForm.find("select[name^='addressCity']").on('change', function () {
        var element = _$parentForm.find(this);
        var url = 'Parents/AddressTownDropdown?cityId=' + this.value;

        abp.ajax({
            url: url,
            type: 'POST',
            dataType: 'html',
            success: function (content) {

                element.closest(".locationDiv").find(".townDiv").html(content);
            },
            error: function (e) { }
        });
    });

    if (_$parentForm.find("input[name='addressTownId']").val() != "" && _$parentForm.find("input[name='addressTownId']").val() != null) {
        _$parentForm.find("select[name^='addressCity']").each(function (i, v) {
            var element = _$parentForm.find(this);
            var selectedValue = _$parentForm.find(this).closest(".locationDiv").find("input[name^='addressTownId'").val();
            var url = 'Parents/AddressTownDropdown?cityId=' + this.value + '&selectedValue=' + selectedValue;

            abp.ajax({
                url: url,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    element.closest(".locationDiv").find(".townDiv").html(content);
                },
                error: function (e) { }
            });
        });
    }

    //Validations
    _$parentForm.find("select[name^='addressCity']").on('change', function () {
        _$parentForm.find(this).closest(".col-sm-9").find(".input-group").removeClass("is-invalid");
        _$parentForm.find(this).closest(".col-sm-9").find(".text-danger").remove();
    });

    //Select2
    _$parentForm.find('.js-select2-custom').each(function () {
        var select2 = $.HSCore.components.HSSelect2.init(_$parentForm.find(this));
    });

})(jQuery);

