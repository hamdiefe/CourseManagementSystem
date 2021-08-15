(function ($) {
    var _$eventTabContent = $("#CreateOrEditEventModalTabContent");
    var _$eventForm = _$eventTabContent.find('form');

    //ShowFormText
    _$eventForm.find("input[name^='endDate']").on('change', function () {
        var element = _$eventForm.find(this);

        element.closest(".endDateDiv").find(".form-text").show();

        //WeekCountInputShow
        $(".weekCountHr").show();
        $(".weekCountDiv").show();
    });

    //Validations
    _$eventForm.find("input[name^='endDate']").on('change', function () {
        if (_$eventForm.find(this) != "") {
            _$eventForm.find(this).removeClass("is-invalid");
            _$eventForm.find(this).closest(".col-sm-9").find(".text-danger").remove();
        }
    });
})(jQuery);

