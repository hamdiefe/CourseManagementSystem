(function ($) {
    var _$teacherTabContent = $("#CreateOrEditTeacherModalTabContent");
    var _$teacherForm = _$teacherTabContent.find('form');

    //LocationDropdowns
    _$teacherForm.find("select[name^='addressCity']").on('change', function () {
        var element = _$teacherForm.find(this);
        var url = 'Teachers/AddressTownDropdown?cityId=' + this.value;

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

    if (_$teacherForm.find("input[name='addressTownId']").val() != "" && _$teacherForm.find("input[name='addressTownId']").val() != null) {
        _$teacherForm.find("select[name^='addressCity']").each(function (i, v) {
            var element = _$teacherForm.find(this);
            var selectedValue = _$teacherForm.find(this).closest(".locationDiv").find("input[name^='addressTownId'").val();
            var url = 'Teachers/AddressTownDropdown?cityId=' + this.value + '&selectedValue=' + selectedValue;

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
    _$teacherForm.find("select[name^='addressCity']").on('change', function () {
        _$teacherForm.find(this).closest(".col-sm-9").find(".input-group").removeClass("is-invalid");
        _$teacherForm.find(this).closest(".col-sm-9").find(".text-danger").remove();
    });

    //Select2
    _$teacherForm.find('.js-select2-custom').each(function () {
        var select2 = $.HSCore.components.HSSelect2.init(_$teacherForm.find(this));
    });

})(jQuery);

