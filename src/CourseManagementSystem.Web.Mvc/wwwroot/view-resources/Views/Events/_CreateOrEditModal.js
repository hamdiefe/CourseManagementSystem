(function ($) {
    var l = abp.localization.getSource('CourseManagementSystem');
    var _eventsService = abp.services.app.events;
    var _$eventTabContent = $("#CreateOrEditEventModalTabContent");
    var _$eventModal = _$eventTabContent.closest("#CreateOrEditModal");
    var _$eventForm = _$eventTabContent.find('form');

    //AddStartDate
    _$eventForm.find("#AddStartDate").click(function () {
        abp.ajax({
            url: 'Events/AddStartDateField/',
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                _$eventForm.find("#addDateFieldContainer").append(content);
            },
            error: function (e) { }
        });
    });

    //DeleteDate
    _$eventForm.find(".delete-date").click(function () {
        var element = _$eventForm.find(this);
        element.closest(".date-container").remove();
    });

    //Save
    _$eventForm.closest('div.modal-content').find(".save-button").click(function (e) {
        e.preventDefault();
        save();
    });

    if (_$eventForm.find("input[name='startDate']").val() != "" && _$eventForm.find("input[name='startDate']").val() != null) {
        _$eventForm.find("input[name^='startDate']").each(function (i, v) {
            var element = _$eventForm.find(this);
            var selectedEndDate = _$eventForm.find(this).closest(".date-container").find(".endDateDiv").find("input[name^='selectedEndDate'").val();

            var url = 'Events/AddEndDateField?startDate=' + this.value + '&selectedEndDate=' + selectedEndDate;

            abp.ajax({
                url: url,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    element.closest(".date-container").find(".endDateDiv").html(content);
                },
                error: function (e) { }
            });
        });
    }

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

    //function save() {
    //    var isValid = true;

    //    if (!_$eventForm.valid()) {
    //        isValid = false;
    //        return;
    //    }

    //    if (_$eventForm.find("input[name^='title']").val() == "") {

    //        if (_$eventForm.find("input[name^='title']").val() == "") {
    //            _$eventForm.find("input[name^='title']").addClass("is-invalid");
    //            _$eventForm.find("input[name^='title']").closest(".js-form-message").after('<p class="text-danger">' + l('ThisFieldRequired') + '</p>');
    //        }

    //        isValid = false;
    //        abp.message.error(l('CheckThe{0}', l('GeneralInformations')));
    //        return;
    //    }

    //    if (_$eventForm.find("div[class^='startDateDiv']").length == 0) {
    //        isValid = false;
    //        abp.message.error(l('AtLeastOneDateMustBeEntered'));
    //    }

    //    var event = _$eventForm.serializeFormToObject();

    //    event.eventStudents = [];
    //    var eventStudentData = _$eventForm.find("select[name^='student']").val();
    //    for (var i = 0; i < eventStudentData.length; i++) {
    //        event.eventStudents.push({
    //            studentId: eventStudentData[i]
    //        });
    //    }

    //    event.eventTeachers = [];
    //    var eventTeacherData = _$eventForm.find("select[name^='teacher']").val();
    //    for (var i = 0; i < eventTeacherData.length; i++) {
    //        event.eventTeachers.push({
    //            teacherId: eventTeacherData[i]
    //        });
    //    }

    //    event.eventDates = []

    //    _$eventForm.find("input[name^='startDate']").each(function (i, v) {
    //        if (_$eventForm.find(this).val() == "") {
    //            _$eventForm.find(this).addClass("is-invalid");
    //            _$eventForm.find(this).closest(".js-form-message").nextAll().remove();
    //            _$eventForm.find(this).closest(".js-form-message").after('<p class="text-danger">' + l('ThisFieldRequired') + '</p>');

    //            isValid = false;
    //            abp.message.error(l('CheckThe{0}', l('StartDate')));
    //            return;
    //        }

    //        event.eventDates.push({
    //            id: _$eventForm.find(this).attr('data-eventDate-id'),
    //            startDate: _$eventForm.find(this).val()
    //        });

    //    });

    //    _$eventForm.find("input[name^='endDate']").each(function (i, v) {
    //        if (_$eventForm.find(this).val() == "") {
    //            _$eventForm.find(this).addClass("is-invalid");
    //            _$eventForm.find(this).closest(".js-form-message").nextAll().remove();
    //            _$eventForm.find(this).closest(".js-form-message").after('<p class="text-danger">' + l('ThisFieldRequired') + '</p>');

    //            isValid = false;
    //            abp.message.error(l('CheckThe{0}', l('EndDate')));
    //            return;
    //        }
    //        event.eventDates[i].endDate = _$eventForm.find(this).val();
    //    });
    //    if (isValid == true) {
    //        abp.ui.setBusy(_$eventForm);
    //        _eventsService.createOrEdit(
    //            event
    //        ).done(function () {
    //            _$eventModal.modal('hide');
    //            abp.notify.info(l('SavedSuccessfully'));
    //            abp.event.trigger('event.createOrEditEventModalSaved');
    //        }).always(function () {
    //            abp.ui.clearBusy(_$eventForm);
    //        });
    //    }
    //}


    function save() {
        var isValid = true;

        if (!_$eventForm.valid()) {
            isValid = false;
            return;
        }

        if (_$eventForm.find("input[name^='title']").val() == "") {

            if (_$eventForm.find("input[name^='title']").val() == "") {
                _$eventForm.find("input[name^='title']").addClass("is-invalid");
                _$eventForm.find("input[name^='title']").closest(".js-form-message").after('<p class="text-danger">' + l('ThisFieldRequired') + '</p>');
            }

            isValid = false;
            abp.message.error(l('CheckThe{0}', l('GeneralInformations')));
            return;
        }

        if (_$eventForm.find("div[class^='startDateDiv']").length == 0) {
            isValid = false;
            abp.message.error(l('AtLeastOneDateMustBeEntered'));
        }

        var event = _$eventForm.serializeFormToObject();

        event.eventStudents = [];
        var eventStudentIdListForPayment = [];
        var eventStudentData = _$eventForm.find("select[name^='student']").val();
        for (var i = 0; i < eventStudentData.length; i++) {
            event.eventStudents.push({
                studentId: eventStudentData[i]
            });
            eventStudentIdListForPayment.push(eventStudentData[i]);
        }

        event.eventTeachers = [];
        var eventTeacherData = _$eventForm.find("select[name^='teacher']").val();
        for (var i = 0; i < eventTeacherData.length; i++) {
            event.eventTeachers.push({
                teacherId: eventTeacherData[i]
            });
        }

        event.eventDates = []

        _$eventForm.find("input[name^='startDate']").each(function (i, v) {
            if (_$eventForm.find(this).val() == "") {
                _$eventForm.find(this).addClass("is-invalid");
                _$eventForm.find(this).closest(".js-form-message").nextAll().remove();
                _$eventForm.find(this).closest(".js-form-message").after('<p class="text-danger">' + l('ThisFieldRequired') + '</p>');

                isValid = false;
                abp.message.error(l('CheckThe{0}', l('StartDate')));
                return;
            }

            event.eventDates.push({
                id: _$eventForm.find(this).attr('data-eventDate-id'),
                startDate: _$eventForm.find(this).val()
            });

        });

        _$eventForm.find("input[name^='endDate']").each(function (i, v) {
            if (_$eventForm.find(this).val() == "") {
                _$eventForm.find(this).addClass("is-invalid");
                _$eventForm.find(this).closest(".js-form-message").nextAll().remove();
                _$eventForm.find(this).closest(".js-form-message").after('<p class="text-danger">' + l('ThisFieldRequired') + '</p>');

                isValid = false;
                abp.message.error(l('CheckThe{0}', l('EndDate')));
                return;
            }
            event.eventDates[i].endDate = _$eventForm.find(this).val();
        });

        if (isValid == true) {
            abp.ui.setBusy(_$eventForm);
            _eventsService.createOrEdit(
                event
            ).done(function () {
                console.log(eventStudentIdListForPayment)

                _eventsService.refreshPayments(
                    $("#Event_StudentId").val()
                ).done(function () {
                    _$eventModal.modal('hide');
                    abp.notify.info(l('SavedSuccessfully'));
                    abp.event.trigger('event.createOrEditEventModalSaved');
                }).always(function () {

                });
            }).always(function () {
                abp.ui.clearBusy(_$eventForm);
            });
        }
    }

    //Validations
    _$eventForm.find("input[name='title']").on('change', function () {
        if (_$eventForm.find("input[name='title']") != "") {
            _$eventForm.find("input[name='title']").removeClass("is-invalid");
            _$eventForm.find("input[name='title']").closest(".col-sm-9").find(".text-danger").remove();
        }
    });

    _$eventForm.find("input[name^='startDate']").on('change', function () {
        if (_$eventForm.find(this) != "") {
            _$eventForm.find(this).removeClass("is-invalid");
            _$eventForm.find(this).closest(".col-sm-9").find(".text-danger").remove();
        }
    });

    _$eventForm.find("input[name^='endDate']").on('change', function () {
        if (_$eventForm.find(this) != "") {
            _$eventForm.find(this).removeClass("is-invalid");
            _$eventForm.find(this).closest(".col-sm-9").find(".text-danger").remove();
        }
    });

    //Select2
    _$eventForm.find('.js-select2-custom').each(function () {
        var select2 = $.HSCore.components.HSSelect2.init(_$eventForm.find(this));
    });

    //Tabs 
    _$eventForm.find('.js-tabs-to-dropdown').each(function () {
        var transformTabsToBtn = new HSTransformTabsToBtn(_$eventForm.find(this)).init();
    });

    //Flatpckr
    _$eventForm.find('.js-flatpickr').each(function () {
        $.HSCore.components.HSFlatpickr.init(_$eventForm.find(this));
    });
})(jQuery);

