(function ($) {
    var l = abp.localization.getSource('CourseManagementSystem');
    var _studentsService = abp.services.app.students;
    var _$studentTabContent = $("#CreateOrEditStudentModalTabContent");
    var _$studentModal = _$studentTabContent.closest("#CreateOrEditModal");
    var _$studentForm = _$studentTabContent.find('form');

    var _$parentModal = $("#CreateOrEditParentModal");

    //AddParent
    _$studentForm.find("#AddParent").click(function () {
        createOrEditParent();
    });

    //EditParent
    _$studentForm.find("a[name='editParent']").click(function () {
        var parentId = _$studentForm.find(this).closest(".parent-container").find("input[name='parentId']").val();
        //e.preventDefault();

        createOrEditParent(parentId);
    });

    //Save
    _$studentForm.closest('div.modal-content').find(".save-button").click(function (e) {
        e.preventDefault();
        save();
    });

    //AddAddress
    _$studentForm.find("#AddAddress").click(function () {
        abp.ajax({
            url: 'Students/AddAddressField/',
            type: 'POST',
            dataType: 'html',
            success: function (content) {

                _$studentForm.find("#addAddressFieldContainer").append(content);

            },
            error: function (e) { }
        });
    });

    //AddPhone
    _$studentForm.find("#AddPhone").click(function () {
        abp.ajax({
            url: 'Students/AddPhoneField/',
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                _$studentForm.find("#addPhoneFieldContainer").append(content);
                mask();
            },
            error: function (e) { }
        });
    });

    //DeleteAddress
    _$studentForm.find(".delete-address").click(function () {
        var element = _$studentForm.find(this);
        element.closest(".address-container").remove();
    });

    //DeletePhone
    _$studentForm.find(".delete-phone").click(function () {
        var element = _$studentForm.find(this);
        element.closest(".phone-container").remove();
    });

    //DeleteParent
    _$studentForm.find(".delete-parent").click(function () {
        var element = _$studentForm.find(this);
        element.closest(".parent-container").remove();
    });

    //Validations
    _$studentForm.find("input[name^='phoneNumber']").on('change', function () {
        if (_$studentForm.find(this).val() != "" && _$studentForm.find(this).val().length == 15) {
            _$studentForm.find(this).removeClass("is-invalid");
            _$studentForm.find(this).closest(".phone-container").find(".input-group-prepend").removeClass("is-invalid");
            _$studentForm.find(this).closest(".phone-container").find(".input-group-append").removeClass("is-invalid");
            _$studentForm.find(this).closest(".phone-container").find(".text-danger").remove();
        }
    });

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

    _$studentForm.find("select[name^='addressCity']").on('change', function () {
        _$studentForm.find(this).closest(".col-sm-9").find(".input-group").removeClass("is-invalid");
        _$studentForm.find(this).closest(".col-sm-9").find(".text-danger").remove();
    });

    _$studentForm.find("select[name^='addressTown']").on('change', function () {
        _$studentForm.find(this).closest(".col-sm-9").find(".input-group").removeClass("is-invalid");
        _$studentForm.find(this).closest(".col-sm-9").find(".text-danger").remove();
    });

    _$studentForm.find("input[name='name']").on('change', function () {
        if (_$studentForm.find("input[name='name']") != "") {
            _$studentForm.find("input[name='name']").removeClass("is-invalid");
            _$studentForm.find("input[name='name']").closest(".col-sm-9").find(".text-danger").remove();
        }
    });

    _$studentForm.find("input[name='surname']").on('change', function () {
        if (_$studentForm.find("input[name='surname']").val() != "") {
            _$studentForm.find("input[name='surname']").removeClass("is-invalid");
            _$studentForm.find("input[name='surname']").closest(".col-sm-9").find(".text-danger").remove();
        }
    });

    _$studentForm.find("input[name='identityNumber']").on('change', function () {
        if (_$studentForm.find("input[name='identityNumber']").val() != "") {
            _$studentForm.find("input[name='identityNumber']").removeClass("is-invalid");
            _$studentForm.find("input[name='identityNumber']").closest(".col-sm-9").find(".text-danger").remove();
        }
    });

    //LocationDropdowns
    _$studentForm.find("select[name^='addressCountry']").on('change', function () {
        var element = _$studentForm.find(this);

        abp.ajax({
            url: 'Students/AddressCityDropdown?countryId=' + this.value,
            type: 'POST',
            dataType: 'html',
            success: function (content) {

                element.closest(".locationDiv").find(".cityDiv").html(content);
            },
            error: function (e) { }
        });

        var url = 'Students/AddressTownDropdown?cityId=';

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

    if (_$studentForm.find("input[name='addressCityId']").val() != "" && _$studentForm.find("input[name='addressCityId']").val() != null) {
        _$studentForm.find("select[name^='addressCountry']").each(function (i, v) {
            var element = _$studentForm.find(this);
            var selectedValue = _$studentForm.find(this).closest(".locationDiv").find("input[name^='addressCityId'").val();
            var url = 'Students/AddressCityDropdown?countryId=' + this.value + '&selectedValue=' + selectedValue;

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

    function createOrEditParent(parentId) {
        abp.ui.setBusy(_$parentModal);

        var url = abp.appPath + 'Parents/CreateOrEditModal?id=';

        if (parentId) {
            url = url + parentId
        }

        abp.ajax({
            url: url,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#CreateOrEditParentModal div.modal-content').html(content);
                abp.ui.clearBusy(_$parentModal);
            },
            error: function (e) { }
        });
    }

    function save() {
        var isValid = true;

        if (!_$studentForm.valid()) {
            isValid = false;
            return;
        }

        if (_$studentForm.find("input[name^='name']").val() == "" || _$studentForm.find("input[name^='surname']").val() == "") {

            if (_$studentForm.find("input[name^='name']").val() == "") {
                _$studentForm.find("input[name^='name']").addClass("is-invalid");
                _$studentForm.find("input[name^='name']").closest(".js-form-message").after('<p class="text-danger">' + l('ThisFieldRequired') + '</p>');
            }

            if (_$studentForm.find("input[name^='surname']").val() == "") {
                _$studentForm.find("input[name^='surname']").addClass("is-invalid");
                _$studentForm.find("input[name^='surname']").closest(".js-form-message").after('<p class="text-danger">' + l('ThisFieldRequired') + '</p>');
            }

            isValid = false;
            abp.message.error(l('CheckThe{0}', l('GeneralInformations')));
            return;
        }

        if (_$studentForm.find("input[name^='identityNumber']").val().length != 11) {
            _$studentForm.find("input[name^='identityNumber']").addClass("is-invalid");
            _$studentForm.find("input[name^='identityNumber']").closest(".js-form-message").after('<p class="text-danger">' + l('IdentityNumberCouldNotVerify') + '</p>');

            abp.message.error(l('IdentityNumberCouldNotVerify'));
            isValid = false;
            return;
        }

        if (_$studentForm.find('#Student_IdentityNumber').val() != "") {
            var identityNumberResult = verifyIdentityNumber(_$studentForm.find('#Student_IdentityNumber').val());

            if (identityNumberResult == false) {
                _$studentForm.find("input[name^='identityNumber']").addClass("is-invalid");
                _$studentForm.find("input[name^='identityNumber']").closest(".js-form-message").after('<p class="text-danger">' + l('IdentityNumberCouldNotVerify') + '</p>');
                abp.message.error(l('IdentityNumberCouldNotVerify'));
                isValid = false;
                return;
            }
        }

        var student = _$studentForm.serializeFormToObject();

        var phones = [];
        var phoneCount = 0;

        _$studentForm.find("input[name^='phoneNumber']").each(function (i, v) {

            //Validations
            if (_$studentForm.find(this).val() == "") {
                _$studentForm.find(this).removeClass("is-invalid");
                _$studentForm.find(this).closest(".phone-container").find(".input-group-prepend").removeClass("is-invalid");
                _$studentForm.find(this).closest(".phone-container").find(".input-group-append").removeClass("is-invalid");
                _$studentForm.find(this).closest(".phone-container").find(".text-danger").remove();

                _$studentForm.find(this).addClass("is-invalid");
                _$studentForm.find(this).closest(".phone-container").find(".input-group-prepend").addClass("is-invalid");
                _$studentForm.find(this).closest(".phone-container").find(".input-group-append").addClass("is-invalid");
                _$studentForm.find(this).closest(".phone-container").find(".js-form-message").after('<p class="text-danger">' + l('ThisFieldRequired') + '</p>');
                abp.message.error(l('CheckThe{0}', l('Phone')));
                isValid = false;
                return;
            }

            if (_$studentForm.find(this).val().length != 15) {
                _$studentForm.find(this).removeClass("is-invalid");
                _$studentForm.find(this).closest(".phone-container").find(".input-group-prepend").removeClass("is-invalid");
                _$studentForm.find(this).closest(".phone-container").find(".input-group-append").removeClass("is-invalid");
                _$studentForm.find(this).closest(".phone-container").find(".text-danger").remove();

                _$studentForm.find(this).addClass("is-invalid");
                _$studentForm.find(this).closest(".phone-container").find(".input-group-prepend").addClass("is-invalid");
                _$studentForm.find(this).closest(".phone-container").find(".input-group-append").addClass("is-invalid");
                _$studentForm.find(this).closest(".phone-container").find(".js-form-message").after('<p class="text-danger">' + l('CheckThe{0}', l('Phone')) + '</p>');
                abp.message.error(l('CheckThe{0}', l('Phone')));
                isValid = false;
                return;
            }

            if (_$studentForm.find(this).val() != "") {
                phoneCount++;
            }

            phones.push({
                id: _$studentForm.find(this).attr('data-phone-id'),
                number: _$studentForm.find(this).val()
            });

        });

        var j = 0;
        _$studentForm.find("select[name^='phoneCode']").each(function (i, v) {
            phones[j].code = _$studentForm.find(this).val();
            j++;
        });

        var k = 0;
        _$studentForm.find("select[name^='phoneType']").each(function (i, v) {
            phones[k].type = _$studentForm.find(this).val();
            k++;
        });


        if (phoneCount != 0) {
            for (var i = 0; i < phones.length; i++) {
                if (phones[i].number == "") {
                    phones.splice(i, 1)
                }
            }
        }

        student.phones = phones;

        var addresses = [];
        var addressCount = 0;

        _$studentForm.find("textArea[name^='addressDetail']").each(function (i, v) {
            if (_$studentForm.find(this).val() == "") {
                _$studentForm.find(this).removeClass("is-invalid");
                _$studentForm.find(this).closest(".row").find(".text-danger").remove();

                _$studentForm.find(this).addClass("is-invalid");
                _$studentForm.find(this).closest(".row").find(".input-group").after('<p class="text-danger">' + l('ThisFieldRequired') + '</p>');
                abp.message.error(l('CheckThe{0}', l('Address')));
                isValid = false;
                return;
            }

            if (_$studentForm.find(this).val() != "") {
                addressCount++;
            }

            addresses.push({
                id: _$studentForm.find(this).attr('data-address-id'),
                detail: _$studentForm.find(this).val()
            });
        });

        var m = 0;
        _$studentForm.find("select[name^='addressCountry']").each(function (i, v) {
            if (_$studentForm.find(this).val() == "") {
                _$studentForm.find(this).closest(".col-sm-9").find(".input-group").removeClass("is-invalid");
                _$studentForm.find(this).closest(".col-sm-9").find(".text-danger").remove();

                _$studentForm.find(this).closest(".col-sm-9").find(".input-group").addClass("is-invalid");
                _$studentForm.find(this).closest(".col-sm-9").find(".input-group").after('<p class="text-danger">' + l('ThisFieldRequired') + '</p>');
                abp.message.error(l('CheckThe{0}', l('Address')));
                isValid = false;
                return;
            }

            addresses[m].countryId = _$studentForm.find(this).val();
            m++;
        });

        var n = 0;
        _$studentForm.find("select[name^='addressCity']").each(function (i, v) {
            if (_$studentForm.find(this).val() == "") {
                _$studentForm.find(this).closest(".col-sm-9").find(".input-group").removeClass("is-invalid");
                _$studentForm.find(this).closest(".col-sm-9").find(".text-danger").remove();

                _$studentForm.find(this).closest(".col-sm-9").find(".input-group").addClass("is-invalid");
                _$studentForm.find(this).closest(".col-sm-9").find(".input-group").after('<p class="text-danger">' + l('ThisFieldRequired') + '</p>');
                abp.message.error(l('CheckThe{0}', l('Address')));
                isValid = false;
                return;
            }
            addresses[n].cityId = _$studentForm.find(this).val();
            n++;
        });

        var o = 0;
        _$studentForm.find("select[name^='addressTown']").each(function (i, v) {
            if (_$studentForm.find(this).val() == "") {
                _$studentForm.find(this).closest(".col-sm-9").find(".input-group").removeClass("is-invalid");
                _$studentForm.find(this).closest(".col-sm-9").find(".text-danger").remove();

                _$studentForm.find(this).closest(".col-sm-9").find(".input-group").addClass("is-invalid");
                _$studentForm.find(this).closest(".col-sm-9").find(".input-group").after('<p class="text-danger">' + l('ThisFieldRequired') + '</p>');
                abp.message.error(l('CheckThe{0}', l('Address')));
                isValid = false;
                return;
            }


            addresses[o].townId = _$studentForm.find(this).val();
            o++;
        });

        var p = 0;
        _$studentForm.find("select[name^='addressType']").each(function (i, v) {
            addresses[p].type = _$studentForm.find(this).val();
            p++;
        });

        if (addressCount != 0) {
            for (var i = 0; i < addresses.length; i++) {
                if (addresses[i].detail == "") {
                    addresses.splice(i, 1)
                }
            }
        }

        student.addresses = addresses;

        var parents = [];

        _$studentForm.find("input[name='parentId']").each(function (i, v) {
            if (_$studentForm.find(this).val() != "") {
                parents.push({
                    id: _$studentForm.find(this).attr('data-studentParent-id'),
                    parentId: _$studentForm.find(this).val()
                });
            }
        });

        student.studentParents = parents;

        if ($('#Student_IsActive').prop('checked')) {
            student.isActive = true;
        }
        else {
            student.isActive = false;
        }

        if ($("#Student_HourlyPaymentAmount").val() == "") {
            student.hourlyPaymentAmount = "0";
        }

        if (isValid == true) {
            abp.ui.setBusy(_$studentForm);
            _studentsService.createOrEdit(
                student
            ).done(function () {
                _$studentModal.modal('hide');
                abp.notify.info(l('SavedSuccessfully'));
                abp.event.trigger('student.createOrEditStudentModalSaved');
            }).always(function () {
                abp.ui.clearBusy(_$studentForm);
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
        _$studentForm.find('.js-masked-input').each(function () {
            var mask = $.HSCore.components.HSMask.init(_$studentForm.find(this));
        });
        $("#Student_HourlyPaymentAmount").maskMoney({ thousands: '.', decimal: ',' });
    }

    //Flatpckr
    _$studentForm.find('.js-flatpickr').each(function () {
        $.HSCore.components.HSFlatpickr.init(_$studentForm.find(this));
    });

    //Select2
    _$studentForm.find('.js-select2-custom').each(function () {
        var select2 = $.HSCore.components.HSSelect2.init(_$studentForm.find(this));
    });

    //Tabs 
    _$studentForm.find('.js-tabs-to-dropdown').each(function () {
        var transformTabsToBtn = new HSTransformTabsToBtn(_$studentForm.find(this)).init();
    });

    //Mask inputs 
    mask();

})(jQuery);

