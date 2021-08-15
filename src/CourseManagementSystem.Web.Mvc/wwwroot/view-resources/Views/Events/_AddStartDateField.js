(function ($) {
    var l = abp.localization.getSource('CourseManagementSystem');
    var _$eventTabContent = $("#CreateOrEditEventModalTabContent");
    var _$eventForm = _$eventTabContent.find('form');

    //GetEndDate
    _$eventForm.find("input[name^='startDate']").on('change', function () {
        var element = _$eventForm.find(this);
        var url = 'Events/AddEndDateField?startDate=' + this.value;

        abp.ajax({
            url: url,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                element.closest(".date-container").find('.endDateDiv').html(content);
                element.closest(".startDateDiv").find(".form-text").show();
            },
            error: function (e) { }
        });
    });

    //DeleteDate
    _$eventForm.find(".delete-date").click(function () {
        var element = _$eventForm.find(this);
        element.closest(".date-container").remove();
    });    

    //Validations
    _$eventForm.find("input[name^='startDate']").on('change', function () {
        if (_$eventForm.find(this) != "") {
            _$eventForm.find(this).removeClass("is-invalid");
            _$eventForm.find(this).closest(".col-sm-9").find(".text-danger").remove();
        }
    });   
})(jQuery);

