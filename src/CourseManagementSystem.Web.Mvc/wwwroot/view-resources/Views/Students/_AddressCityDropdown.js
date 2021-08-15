(function ($) {
    var _$studentTabContent = $("#CreateOrEditStudentModalTabContent");
    var _$studentForm = _$studentTabContent.find('form');

    //LocationDropdowns
    _$studentForm.find("select[name^='addressCity']").on('change', function () {
        var element = _$studentForm.find(this);
        var url = 'Students/AddressTownDropdown?cityId=' + this.value;

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

    if (_$studentForm.find("input[name='addressTownId']").val() != "" && _$studentForm.find("input[name='addressTownId']").val() != null) {
        _$studentForm.find("select[name^='addressCity']").each(function (i, v) {
            var element = _$studentForm.find(this);
            var selectedValue = _$studentForm.find(this).closest(".locationDiv").find("input[name^='addressTownId'").val();
            var url = 'Students/AddressTownDropdown?cityId=' + this.value + '&selectedValue=' + selectedValue;

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
    _$studentForm.find("select[name^='addressCity']").on('change', function () {
        _$studentForm.find(this).closest(".col-sm-9").find(".input-group").removeClass("is-invalid");
        _$studentForm.find(this).closest(".col-sm-9").find(".text-danger").remove();
    });

    //Select2
    _$studentForm.find('.js-select2-custom').each(function () {
        var select2 = $.HSCore.components.HSSelect2.init(_$studentForm.find(this));
    });

})(jQuery);

