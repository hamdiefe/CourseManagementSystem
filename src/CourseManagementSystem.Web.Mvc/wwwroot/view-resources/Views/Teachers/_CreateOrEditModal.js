(function ($) {
    var l = abp.localization.getSource('CourseManagementSystem');
    var _teachersService = abp.services.app.teachers;
    var _$teacherTabContent = $("#CreateOrEditTeacherModalTabContent");
    var _$teacherModal = _$teacherTabContent.closest("#CreateOrEditModal");
    var _$teacherForm = _$teacherTabContent.find('form');

    //Save
    _$teacherForm.closest('div.modal-content').find(".save-button").click(function (e) {
        e.preventDefault();
        save();
    });

    //AddAddress
    _$teacherForm.find("#AddAddress").click(function () {
        abp.ajax({
            url: 'Teachers/AddAddressField/',
            type: 'POST',
            dataType: 'html',
            success: function (content) {

                _$teacherForm.find("#addAddressFieldContainer").append(content);

            },
            error: function (e) { }
        });
    });

    //AddPhone
    _$teacherForm.find("#AddPhone").click(function () {
        abp.ajax({
            url: 'Teachers/AddPhoneField/',
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                _$teacherForm.find("#addPhoneFieldContainer").append(content);
                mask();
            },
            error: function (e) { }
        });
    });

    //DeleteAddress
    _$teacherForm.find(".delete-address").click(function () {
        var element = _$teacherForm.find(this);
        element.closest(".address-container").remove();
    });

    //DeletePhone
    _$teacherForm.find(".delete-phone").click(function () {
        var element = _$teacherForm.find(this);
        element.closest(".phone-container").remove();
    });

    //Validations
    _$teacherForm.find("input[name^='phoneNumber']").on('change', function () {
        if (_$teacherForm.find(this).val() != "" && _$teacherForm.find(this).val().length == 15) {
            _$teacherForm.find(this).removeClass("is-invalid");
            _$teacherForm.find(this).closest(".phone-container").find(".input-group-prepend").removeClass("is-invalid");
            _$teacherForm.find(this).closest(".phone-container").find(".input-group-append").removeClass("is-invalid");
            _$teacherForm.find(this).closest(".phone-container").find(".text-danger").remove();
        }
    });

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

    _$teacherForm.find("select[name^='addressCity']").on('change', function () {
        _$teacherForm.find(this).closest(".col-sm-9").find(".input-group").removeClass("is-invalid");
        _$teacherForm.find(this).closest(".col-sm-9").find(".text-danger").remove();
    });

    _$teacherForm.find("select[name^='addressTown']").on('change', function () {
        _$teacherForm.find(this).closest(".col-sm-9").find(".input-group").removeClass("is-invalid");
        _$teacherForm.find(this).closest(".col-sm-9").find(".text-danger").remove();
    });

    _$teacherForm.find("input[name='name']").on('change', function () {
        if (_$teacherForm.find("input[name='name']") != "") {
            _$teacherForm.find("input[name='name']").removeClass("is-invalid");
            _$teacherForm.find("input[name='name']").closest(".col-sm-9").find(".text-danger").remove();
        }
    });

    _$teacherForm.find("input[name='surname']").on('change', function () {
        if (_$teacherForm.find("input[name='surname']").val() != "") {
            _$teacherForm.find("input[name='surname']").removeClass("is-invalid");
            _$teacherForm.find("input[name='surname']").closest(".col-sm-9").find(".text-danger").remove();
        }
    });

    _$teacherForm.find("input[name='identityNumber']").on('change', function () {
        if (_$teacherForm.find("input[name='identityNumber']").val() != "") {
            _$teacherForm.find("input[name='identityNumber']").removeClass("is-invalid");
            _$teacherForm.find("input[name='identityNumber']").closest(".col-sm-9").find(".text-danger").remove();
        }
    });

    //LocationDropdowns
    _$teacherForm.find("select[name^='addressCountry']").on('change', function () {
        var element = _$teacherForm.find(this);

        abp.ajax({
            url: 'Teachers/AddressCityDropdown?countryId=' + this.value,
            type: 'POST',
            dataType: 'html',
            success: function (content) {

                element.closest(".locationDiv").find(".cityDiv").html(content);
            },
            error: function (e) { }
        });

        var url = 'Teachers/AddressTownDropdown?cityId=';

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

    if (_$teacherForm.find("input[name='addressCityId']").val() != "" && _$teacherForm.find("input[name='addressCityId']").val() != null) {
        _$teacherForm.find("select[name^='addressCountry']").each(function (i, v) {
            var element = _$teacherForm.find(this);
            var selectedValue = _$teacherForm.find(this).closest(".locationDiv").find("input[name^='addressCityId'").val();
            var url = 'Teachers/AddressCityDropdown?countryId=' + this.value + '&selectedValue=' + selectedValue;

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
    }

    function save() {
        var isValid = true;

        if (!_$teacherForm.valid()) {
            isValid = false;
            return;
        }

        if (_$teacherForm.find("input[name^='name']").val() == "" || _$teacherForm.find("input[name^='surname']").val() == "") {

            if (_$teacherForm.find("input[name^='name']").val() == "") {
                _$teacherForm.find("input[name^='name']").addClass("is-invalid");
                _$teacherForm.find("input[name^='name']").closest(".js-form-message").after('<p class="text-danger">' + l('ThisFieldRequired') + '</p>');
            }

            if (_$teacherForm.find("input[name^='surname']").val() == "") {
                _$teacherForm.find("input[name^='surname']").addClass("is-invalid");
                _$teacherForm.find("input[name^='surname']").closest(".js-form-message").after('<p class="text-danger">' + l('ThisFieldRequired') + '</p>');
            }

            isValid = false;
            abp.message.error(l('CheckThe{0}', l('GeneralInformations')));
            return;
        }

        if (_$teacherForm.find("input[name^='identityNumber']").val().length != 11) {
            _$teacherForm.find("input[name^='identityNumber']").addClass("is-invalid");
            _$teacherForm.find("input[name^='identityNumber']").closest(".js-form-message").after('<p class="text-danger">' + l('IdentityNumberCouldNotVerify') + '</p>');

            abp.message.error(l('IdentityNumberCouldNotVerify'));
            isValid = false;
            return;
        }

        if (_$teacherForm.find('#Teacher_IdentityNumber').val() != "") {
            var identityNumberResult = verifyIdentityNumber(_$teacherForm.find('#Teacher_IdentityNumber').val());

            if (identityNumberResult == false) {
                _$teacherForm.find("input[name^='identityNumber']").addClass("is-invalid");
                _$teacherForm.find("input[name^='identityNumber']").closest(".js-form-message").after('<p class="text-danger">' + l('IdentityNumberCouldNotVerify') + '</p>');
                abp.message.error(l('IdentityNumberCouldNotVerify'));
                isValid = false;
                return;
            }
        }

        var teacher = _$teacherForm.serializeFormToObject();

        var phones = [];
        var phoneCount = 0;

        _$teacherForm.find("input[name^='phoneNumber']").each(function (i, v) {

            //Validations
            if (_$teacherForm.find(this).val() == "") {
                _$teacherForm.find(this).removeClass("is-invalid");
                _$teacherForm.find(this).closest(".phone-container").find(".input-group-prepend").removeClass("is-invalid");
                _$teacherForm.find(this).closest(".phone-container").find(".input-group-append").removeClass("is-invalid");
                _$teacherForm.find(this).closest(".phone-container").find(".text-danger").remove();

                _$teacherForm.find(this).addClass("is-invalid");
                _$teacherForm.find(this).closest(".phone-container").find(".input-group-prepend").addClass("is-invalid");
                _$teacherForm.find(this).closest(".phone-container").find(".input-group-append").addClass("is-invalid");
                _$teacherForm.find(this).closest(".phone-container").find(".js-form-message").after('<p class="text-danger">' + l('ThisFieldRequired') + '</p>');
                abp.message.error(l('CheckThe{0}', l('Phone')));
                isValid = false;
                return;
            }

            if (_$teacherForm.find(this).val().length != 15) {
                _$teacherForm.find(this).removeClass("is-invalid");
                _$teacherForm.find(this).closest(".phone-container").find(".input-group-prepend").removeClass("is-invalid");
                _$teacherForm.find(this).closest(".phone-container").find(".input-group-append").removeClass("is-invalid");
                _$teacherForm.find(this).closest(".phone-container").find(".text-danger").remove();

                _$teacherForm.find(this).addClass("is-invalid");
                _$teacherForm.find(this).closest(".phone-container").find(".input-group-prepend").addClass("is-invalid");
                _$teacherForm.find(this).closest(".phone-container").find(".input-group-append").addClass("is-invalid");
                _$teacherForm.find(this).closest(".phone-container").find(".js-form-message").after('<p class="text-danger">' + l('CheckThe{0}', l('Phone')) + '</p>');
                abp.message.error(l('CheckThe{0}', l('Phone')));
                isValid = false;
                return;
            }

            if (_$teacherForm.find(this).val() != "") {
                phoneCount++;
            }

            phones.push({
                id: _$teacherForm.find(this).attr('data-phone-id'),
                number: _$teacherForm.find(this).val()
            });

        });

        var j = 0;
        _$teacherForm.find("select[name^='phoneCode']").each(function (i, v) {
            phones[j].code = _$teacherForm.find(this).val();
            j++;
        });

        var k = 0;
        _$teacherForm.find("select[name^='phoneType']").each(function (i, v) {
            phones[k].type = _$teacherForm.find(this).val();
            k++;
        });


        if (phoneCount != 0) {
            for (var i = 0; i < phones.length; i++) {
                if (phones[i].number == "") {
                    phones.splice(i, 1)
                }
            }
        }

        teacher.phones = phones;

        var addresses = [];
        var addressCount = 0;

        _$teacherForm.find("textArea[name^='addressDetail']").each(function (i, v) {
            if (_$teacherForm.find(this).val() == "") {
                _$teacherForm.find(this).removeClass("is-invalid");
                _$teacherForm.find(this).closest(".row").find(".text-danger").remove();

                _$teacherForm.find(this).addClass("is-invalid");
                _$teacherForm.find(this).closest(".row").find(".input-group").after('<p class="text-danger">' + l('ThisFieldRequired') + '</p>');
                abp.message.error(l('CheckThe{0}', l('Address')));
                isValid = false;
                return;
            }

            if (_$teacherForm.find(this).val() != "") {
                addressCount++;
            }

            addresses.push({
                id: _$teacherForm.find(this).attr('data-address-id'),
                detail: _$teacherForm.find(this).val()
            });
        });

        var m = 0;
        _$teacherForm.find("select[name^='addressCountry']").each(function (i, v) {
            if (_$teacherForm.find(this).val() == "") {
                _$teacherForm.find(this).closest(".col-sm-9").find(".input-group").removeClass("is-invalid");
                _$teacherForm.find(this).closest(".col-sm-9").find(".text-danger").remove();

                _$teacherForm.find(this).closest(".col-sm-9").find(".input-group").addClass("is-invalid");
                _$teacherForm.find(this).closest(".col-sm-9").find(".input-group").after('<p class="text-danger">' + l('ThisFieldRequired') + '</p>');
                abp.message.error(l('CheckThe{0}', l('Address')));
                isValid = false;
                return;
            }

            addresses[m].countryId = _$teacherForm.find(this).val();
            m++;
        });

        var n = 0;
        _$teacherForm.find("select[name^='addressCity']").each(function (i, v) {
            if (_$teacherForm.find(this).val() == "") {
                _$teacherForm.find(this).closest(".col-sm-9").find(".input-group").removeClass("is-invalid");
                _$teacherForm.find(this).closest(".col-sm-9").find(".text-danger").remove();

                _$teacherForm.find(this).closest(".col-sm-9").find(".input-group").addClass("is-invalid");
                _$teacherForm.find(this).closest(".col-sm-9").find(".input-group").after('<p class="text-danger">' + l('ThisFieldRequired') + '</p>');
                abp.message.error(l('CheckThe{0}', l('Address')));
                isValid = false;
                return;
            }
            addresses[n].cityId = _$teacherForm.find(this).val();
            n++;
        });

        var o = 0;
        _$teacherForm.find("select[name^='addressTown']").each(function (i, v) {
            if (_$teacherForm.find(this).val() == "") {
                _$teacherForm.find(this).closest(".col-sm-9").find(".input-group").removeClass("is-invalid");
                _$teacherForm.find(this).closest(".col-sm-9").find(".text-danger").remove();

                _$teacherForm.find(this).closest(".col-sm-9").find(".input-group").addClass("is-invalid");
                _$teacherForm.find(this).closest(".col-sm-9").find(".input-group").after('<p class="text-danger">' + l('ThisFieldRequired') + '</p>');
                abp.message.error(l('CheckThe{0}', l('Address')));
                isValid = false;
                return;
            }


            addresses[o].townId = _$teacherForm.find(this).val();
            o++;
        });

        var p = 0;
        _$teacherForm.find("select[name^='addressType']").each(function (i, v) {
            addresses[p].type = _$teacherForm.find(this).val();
            p++;
        });

        if (addressCount != 0) {
            for (var i = 0; i < addresses.length; i++) {
                if (addresses[i].detail == "") {
                    addresses.splice(i, 1)
                }
            }
        }

        teacher.addresses = addresses;

        teacher.teacherSpecializedFields = [];
        var teacherSpecializedFieldData = _$teacherForm.find("select[name^='specializedField']").val();
        for (var i = 0; i < teacherSpecializedFieldData.length; i++) {
            teacher.teacherSpecializedFields.push({
                specializedField: teacherSpecializedFieldData[i]
            });
        }

        if ($('#Teacher_IsActive').prop('checked')) {
            teacher.isActive = true;
        }
        else {
            teacher.isActive = false;
        }


        if (isValid == true) {
            abp.ui.setBusy(_$teacherForm);
            _teachersService.createOrEdit(
                teacher
            ).done(function () {
                _$teacherModal.modal('hide');
                abp.notify.info(l('SavedSuccessfully'));
                abp.event.trigger('teacher.createOrEditTeacherModalSaved');
            }).always(function () {
                abp.ui.clearBusy(_$teacherForm);
            });
        }
    }

    function verifyIdentityNumber(idNumber) {
        idNumber = String(idNumber);

        if (idNumber.substring(0, 1) === '0') {
            return false;
        }

        if (idNumber.length !== 11) {
            return false;
        }


        var firstTenArray = idNumber.substr(0, 10).split('');
        var firstTenTotal = singleDigit = doubleDigit = 0;

        for (var i = j = 0; i < 9; ++i) {
            j = parseInt(firstTenArray[i], 10);
            if (i & 1) {
                doubleDigit += j;
            } else {
                singleDigit += j;
            }
            firstTenTotal += j;
        }


        if ((singleDigit * 7 - doubleDigit) % 10 !== parseInt(idNumber.substr(-2, 1), 10)) {
            return false;
        }


        firstTenTotal += parseInt(firstTenArray[9], 10);
        if (firstTenTotal % 10 !== parseInt(idNumber.substr(-1), 10)) {
            return false;
        }

        return true;
    }

    function mask() {
        _$teacherForm.find('.js-masked-input').each(function () {
            var mask = $.HSCore.components.HSMask.init(_$teacherForm.find(this));
        });
    }

    //Flatpckr
    _$teacherForm.find('.js-flatpickr').each(function () {
        $.HSCore.components.HSFlatpickr.init(_$teacherForm.find(this));
    });

    //Select2
    _$teacherForm.find('.js-select2-custom').each(function () {
        var select2 = $.HSCore.components.HSSelect2.init(_$teacherForm.find(this));
    });

    //Tabs 
    _$teacherForm.find('.js-tabs-to-dropdown').each(function () {
        var transformTabsToBtn = new HSTransformTabsToBtn(_$teacherForm.find(this)).init();
    });

    //Mask input 
    mask();
})(jQuery);

