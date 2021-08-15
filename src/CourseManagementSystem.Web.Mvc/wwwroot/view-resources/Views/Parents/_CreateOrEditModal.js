(function ($) {
    var l = abp.localization.getSource('CourseManagementSystem');
    var _parentsService = abp.services.app.parents;
    var _$parentTabContent = $("#CreateOrEditParentModalTabContent");
    var _$parentModal = _$parentTabContent.closest("#CreateOrEditModal");
    var _$parentForm = _$parentTabContent.find('form');

    var _$studentTabContent = $("#CreateOrEditStudentModalTabContent");
    var _$studentForm = _$studentTabContent.find('form');

    //Save
    _$parentForm.closest('div.modal-content').find(".save-button").click(function (e) {
        e.preventDefault();
        save();
    });

    //AddAddress
    _$parentForm.find("#AddAddress").click(function () {
        abp.ajax({
            url: 'Parents/AddAddressField/',
            type: 'POST',
            dataType: 'html',
            success: function (content) {

                _$parentForm.find("#addAddressFieldContainer").append(content);

            },
            error: function (e) { }
        });
    });

    //AddPhone
    _$parentForm.find("#AddPhone").click(function () {
        abp.ajax({
            url: 'Parents/AddPhoneField/',
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                _$parentForm.find("#addPhoneFieldContainer").append(content);
                mask();
            },
            error: function (e) { }
        });
    });

    //DeleteAddress
    _$parentForm.find(".delete-address").click(function () {
        var element = _$parentForm.find(this);
        element.closest(".address-container").remove();
    });

    //DeletePhone
    _$parentForm.find(".delete-phone").click(function () {
        var element = _$parentForm.find(this);
        element.closest(".phone-container").remove();
    });

    //Validations
    _$parentForm.find("input[name^='phoneNumber']").on('change', function () {
        if (_$parentForm.find(this).val() != "" && _$parentForm.find(this).val().length == 15) {
            _$parentForm.find(this).removeClass("is-invalid");
            _$parentForm.find(this).closest(".phone-container").find(".input-group-prepend").removeClass("is-invalid");
            _$parentForm.find(this).closest(".phone-container").find(".input-group-append").removeClass("is-invalid");
            _$parentForm.find(this).closest(".phone-container").find(".text-danger").remove();
        }
    });

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

    _$parentForm.find("select[name^='addressCity']").on('change', function () {
        _$parentForm.find(this).closest(".col-sm-9").find(".input-group").removeClass("is-invalid");
        _$parentForm.find(this).closest(".col-sm-9").find(".text-danger").remove();
    });

    _$parentForm.find("select[name^='addressTown']").on('change', function () {
        _$parentForm.find(this).closest(".col-sm-9").find(".input-group").removeClass("is-invalid");
        _$parentForm.find(this).closest(".col-sm-9").find(".text-danger").remove();
    });

    _$parentForm.find("input[name='name']").on('change', function () {
        if (_$parentForm.find("input[name='name']") != "") {
            _$parentForm.find("input[name='name']").removeClass("is-invalid");
            _$parentForm.find("input[name='name']").closest(".col-sm-9").find(".text-danger").remove();
        }
    });

    _$parentForm.find("input[name='surname']").on('change', function () {
        if (_$parentForm.find("input[name='surname']").val() != "") {
            _$parentForm.find("input[name='surname']").removeClass("is-invalid");
            _$parentForm.find("input[name='surname']").closest(".col-sm-9").find(".text-danger").remove();
        }
    });

    _$parentForm.find("input[name='identityNumber']").on('change', function () {
        if (_$parentForm.find("input[name='identityNumber']").val() != "") {
            _$parentForm.find("input[name='identityNumber']").removeClass("is-invalid");
            _$parentForm.find("input[name='identityNumber']").closest(".col-sm-9").find(".text-danger").remove();
        }
    });

    //LocationDropdowns
    _$parentForm.find("select[name^='addressCountry']").on('change', function () {
        var element = _$parentForm.find(this);

        abp.ajax({
            url: 'Parents/AddressCityDropdown?countryId=' + this.value,
            type: 'POST',
            dataType: 'html',
            success: function (content) {

                element.closest(".locationDiv").find(".cityDiv").html(content);
            },
            error: function (e) { }
        });

        var url = 'Parents/AddressTownDropdown?cityId=';

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

    if (_$parentForm.find("input[name='addressCityId']").val() != "" && _$parentForm.find("input[name='addressCityId']").val() != null) {
        _$parentForm.find("select[name^='addressCountry']").each(function (i, v) {
            var element = _$parentForm.find(this);
            var selectedValue = _$parentForm.find(this).closest(".locationDiv").find("input[name^='addressCityId'").val();
            var url = 'Parents/AddressCityDropdown?countryId=' + this.value + '&selectedValue=' + selectedValue;

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

        if (!_$parentForm.valid()) {
            isValid = false;
            return;
        }

        if (_$parentForm.find("input[name^='name']").val() == "" || _$parentForm.find("input[name^='surname']").val() == "") {

            if (_$parentForm.find("input[name^='name']").val() == "") {
                _$parentForm.find("input[name^='name']").addClass("is-invalid");
                _$parentForm.find("input[name^='name']").closest(".js-form-message").after('<p class="text-danger">' + l('ThisFieldRequired') + '</p>');
            }

            if (_$parentForm.find("input[name^='surname']").val() == "") {
                _$parentForm.find("input[name^='surname']").addClass("is-invalid");
                _$parentForm.find("input[name^='surname']").closest(".js-form-message").after('<p class="text-danger">' + l('ThisFieldRequired') + '</p>');
            }

            isValid = false;
            abp.message.error(l('CheckThe{0}', l('GeneralInformations')));
            return;
        }

        if (_$parentForm.find("input[name^='identityNumber']").val().length != 11) {
            _$parentForm.find("input[name^='identityNumber']").addClass("is-invalid");
            _$parentForm.find("input[name^='identityNumber']").closest(".js-form-message").after('<p class="text-danger">' + l('IdentityNumberCouldNotVerify') + '</p>');

            abp.message.error(l('IdentityNumberCouldNotVerify'));
            isValid = false;
            return;
        }

        if (_$parentForm.find('#Parent_IdentityNumber').val() != "") {
            var identityNumberResult = verifyIdentityNumber(_$parentForm.find('#Parent_IdentityNumber').val());

            if (identityNumberResult == false) {
                _$parentForm.find("input[name^='identityNumber']").addClass("is-invalid");
                _$parentForm.find("input[name^='identityNumber']").closest(".js-form-message").after('<p class="text-danger">' + l('IdentityNumberCouldNotVerify') + '</p>');
                abp.message.error(l('IdentityNumberCouldNotVerify'));
                isValid = false;
                return;
            }
        }

        var parent = _$parentForm.serializeFormToObject();

        var phones = [];
        var phoneCount = 0;

        _$parentForm.find("input[name^='phoneNumber']").each(function (i, v) {

            //Validations
            if (_$parentForm.find(this).val() == "") {
                _$parentForm.find(this).removeClass("is-invalid");
                _$parentForm.find(this).closest(".phone-container").find(".input-group-prepend").removeClass("is-invalid");
                _$parentForm.find(this).closest(".phone-container").find(".input-group-append").removeClass("is-invalid");
                _$parentForm.find(this).closest(".phone-container").find(".text-danger").remove();

                _$parentForm.find(this).addClass("is-invalid");
                _$parentForm.find(this).closest(".phone-container").find(".input-group-prepend").addClass("is-invalid");
                _$parentForm.find(this).closest(".phone-container").find(".input-group-append").addClass("is-invalid");
                _$parentForm.find(this).closest(".phone-container").find(".js-form-message").after('<p class="text-danger">' + l('ThisFieldRequired') + '</p>');
                abp.message.error(l('CheckThe{0}', l('Phone')));
                isValid = false;
                return;
            }

            if (_$parentForm.find(this).val().length != 15) {
                _$parentForm.find(this).removeClass("is-invalid");
                _$parentForm.find(this).closest(".phone-container").find(".input-group-prepend").removeClass("is-invalid");
                _$parentForm.find(this).closest(".phone-container").find(".input-group-append").removeClass("is-invalid");
                _$parentForm.find(this).closest(".phone-container").find(".text-danger").remove();

                _$parentForm.find(this).addClass("is-invalid");
                _$parentForm.find(this).closest(".phone-container").find(".input-group-prepend").addClass("is-invalid");
                _$parentForm.find(this).closest(".phone-container").find(".input-group-append").addClass("is-invalid");
                _$parentForm.find(this).closest(".phone-container").find(".js-form-message").after('<p class="text-danger">' + l('CheckThe{0}', l('Phone')) + '</p>');
                abp.message.error(l('CheckThe{0}', l('Phone')));
                isValid = false;
                return;
            }

            if (_$parentForm.find(this).val() != "") {
                phoneCount++;
            }

            phones.push({
                id: _$parentForm.find(this).attr('data-phone-id'),
                number: _$parentForm.find(this).val()
            });

        });

        var j = 0;
        _$parentForm.find("select[name^='phoneCode']").each(function (i, v) {
            phones[j].code = _$parentForm.find(this).val();
            j++;
        });

        var k = 0;
        _$parentForm.find("select[name^='phoneType']").each(function (i, v) {
            phones[k].type = _$parentForm.find(this).val();
            k++;
        });


        if (phoneCount != 0) {
            for (var i = 0; i < phones.length; i++) {
                if (phones[i].number == "") {
                    phones.splice(i, 1)
                }
            }
        }

        parent.phones = phones;

        var addresses = [];
        var addressCount = 0;

        _$parentForm.find("textArea[name^='addressDetail']").each(function (i, v) {
            if (_$parentForm.find(this).val() == "") {
                _$parentForm.find(this).removeClass("is-invalid");
                _$parentForm.find(this).closest(".row").find(".text-danger").remove();

                _$parentForm.find(this).addClass("is-invalid");
                _$parentForm.find(this).closest(".row").find(".input-group").after('<p class="text-danger">' + l('ThisFieldRequired') + '</p>');
                abp.message.error(l('CheckThe{0}', l('Address')));
                isValid = false;
                return;
            }

            if (_$parentForm.find(this).val() != "") {
                addressCount++;
            }

            addresses.push({
                id: _$parentForm.find(this).attr('data-address-id'),
                detail: _$parentForm.find(this).val()
            });
        });

        var m = 0;
        _$parentForm.find("select[name^='addressCountry']").each(function (i, v) {
            if (_$parentForm.find(this).val() == "") {
                _$parentForm.find(this).closest(".col-sm-9").find(".input-group").removeClass("is-invalid");
                _$parentForm.find(this).closest(".col-sm-9").find(".text-danger").remove();

                _$parentForm.find(this).closest(".col-sm-9").find(".input-group").addClass("is-invalid");
                _$parentForm.find(this).closest(".col-sm-9").find(".input-group").after('<p class="text-danger">' + l('ThisFieldRequired') + '</p>');
                abp.message.error(l('CheckThe{0}', l('Address')));
                isValid = false;
                return;
            }

            addresses[m].countryId = _$parentForm.find(this).val();
            m++;
        });

        var n = 0;
        _$parentForm.find("select[name^='addressCity']").each(function (i, v) {
            if (_$parentForm.find(this).val() == "") {
                _$parentForm.find(this).closest(".col-sm-9").find(".input-group").removeClass("is-invalid");
                _$parentForm.find(this).closest(".col-sm-9").find(".text-danger").remove();

                _$parentForm.find(this).closest(".col-sm-9").find(".input-group").addClass("is-invalid");
                _$parentForm.find(this).closest(".col-sm-9").find(".input-group").after('<p class="text-danger">' + l('ThisFieldRequired') + '</p>');
                abp.message.error(l('CheckThe{0}', l('Address')));
                isValid = false;
                return;
            }
            addresses[n].cityId = _$parentForm.find(this).val();
            n++;
        });

        var o = 0;
        _$parentForm.find("select[name^='addressTown']").each(function (i, v) {
            if (_$parentForm.find(this).val() == "") {
                _$parentForm.find(this).closest(".col-sm-9").find(".input-group").removeClass("is-invalid");
                _$parentForm.find(this).closest(".col-sm-9").find(".text-danger").remove();

                _$parentForm.find(this).closest(".col-sm-9").find(".input-group").addClass("is-invalid");
                _$parentForm.find(this).closest(".col-sm-9").find(".input-group").after('<p class="text-danger">' + l('ThisFieldRequired') + '</p>');
                abp.message.error(l('CheckThe{0}', l('Address')));
                isValid = false;
                return;
            }


            addresses[o].townId = _$parentForm.find(this).val();
            o++;
        });

        var p = 0;
        _$parentForm.find("select[name^='addressType']").each(function (i, v) {
            addresses[p].type = _$parentForm.find(this).val();
            p++;
        });

        for (var i = 0; i < addresses.length; i++) {
            if (addresses[i].detail == "") {
                addresses.splice(i, 1)
            }
        }

        parent.addresses = addresses;

        if (isValid == true) {
            abp.ui.setBusy(_$parentForm);
            _parentsService.createOrEdit(
                parent
            ).done(function (result) {
                _$parentModal.modal('hide');
                abp.notify.info(l('SavedSuccessfully'));
                abp.event.trigger('parent.createOrEditParentModalSaved');

                //Bu scope'a girerse öğrenciler sayfasından açılmış demektir.
                if ($("#CreateOrEditStudentModalTabContent").length != 0) {

                    var willAppend = true;
                    _$studentForm.find("input[name='parentId']").each(function (i, v) {
                        if (_$studentForm.find(this).val() == result.parent.id) {

                            var element = $(this);
                            abp.ajax({
                                url: 'Students/AddParentField?parentId=' + result.parent.id,
                                type: 'POST',
                                dataType: 'html',
                                success: function (content) {
                                    element.closest(".parent-container").html(content);
                                },
                                error: function (e) { }
                            });

                            willAppend = false;
                        }
                    });

                    if (willAppend == true) {
                        abp.ajax({
                            url: 'Students/AddParentField?parentId=' + result.parent.id,
                            type: 'POST',
                            dataType: 'html',
                            success: function (content) {

                                _$studentForm.find("#addParentFieldContainer").append(content);

                            },
                            error: function (e) { }
                        });
                    }
                }
            }).always(function () {
                abp.ui.clearBusy(_$parentForm);
                _$parentTabContent.find(".close-button").trigger("click");
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
        _$parentForm.find('.js-masked-input').each(function () {
            var mask = $.HSCore.components.HSMask.init(_$parentForm.find(this));
        });
    }

    //Flatpckr
    _$parentForm.find('.js-flatpickr').each(function () {
        $.HSCore.components.HSFlatpickr.init(_$parentForm.find(this));
    });

    //Select2
    _$parentForm.find('.js-select2-custom').each(function () {
        var select2 = $.HSCore.components.HSSelect2.init(_$parentForm.find(this));
    });

    //Tabs 
    _$parentForm.find('.js-tabs-to-dropdown').each(function () {
        var transformTabsToBtn = new HSTransformTabsToBtn(_$parentForm.find(this)).init();
    });

    //Mask input 
    mask();
})(jQuery);

