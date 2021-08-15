(function () {
    $(function () {
        var l = abp.localization.getSource('CourseManagementSystem');
        var _$parentsTable = $('#datatable');
        var _parentsService = abp.services.app.parents;

        var datatable = $.HSCore.components.HSDatatables.init($('#datatable'), {
            dom: 'Bfrtip',
            serverSide: true,
            paging: true,
            buttons: [
                {
                    extend: 'copy',
                    className: 'd-none'
                },
                {
                    extend: 'excel',
                    className: 'd-none'
                },
                {
                    extend: 'csv',
                    className: 'd-none'
                },
                {
                    extend: 'pdf',
                    className: 'd-none'
                },
                {
                    extend: 'print',
                    className: 'd-none'
                },
            ],
            select: {
                style: 'multi',
                selector: 'td:first-child input[type="checkbox"]',
                classMap: {
                    checkAll: '#datatableCheckAll',
                    counter: '#datatableCounter',
                    counterInfo: '#datatableCounterInfo'
                }
            },
            language: {
                zeroRecords: '<div class="text-center p-4">' +
                    '<img class="mb-3" src="/front-admin/assets/svg/illustrations/sorry.svg" alt="' + l('NoDataToShow') + '" style="width: 7rem;">' +
                    '<p class="mb-0">' + l('NoDataToShow') + '</p>' +
                    '</div>'
            },
            ajax: function (data, callback, settings) {
                var filter = $('#datatableSearch').serializeFormToObject(true);
                filter.maxResultCount = data.length;
                filter.skipCount = data.start;

                if (data.order[0]) {
                    filter.sorting = data.columns[data.order[0].column].name + ' ' + data.order[0].dir;
                }

                abp.ui.setBusy(_$parentsTable);
                _parentsService.getAll(filter).done(function (result) {
                    callback({
                        recordsTotal: result.totalCount,
                        recordsFiltered: result.totalCount,
                        data: result.items
                    });
                }).always(function () {
                    abp.ui.clearBusy(_$parentsTable);
                });
            },
            columnDefs: [
                {
                    className: 'table-column-pr-0',
                    targets: 0,
                    data: "parent.id",
                    render: function (id) {
                        if (id) {
                            return '<div class="custom-control custom-checkbox">' +
                                '<input type="checkbox" class= "custom-control-input" data-id="' + id + '" id="check' + id + '">' +
                                '<label class="custom-control-label" for="check' + id + '"></label>' +
                                '</div>';
                        }
                        return "";
                    },
                    sortable: false
                },
                {
                    className: 'clickView',
                    targets: 1,
                    data: 'parent.identityNumber',
                    name: 'identityNumber'
                },
                {
                    className: 'clickView',
                    targets: 2,
                    data: 'parent.name',
                    name: 'name'
                },
                {
                    className: 'clickView',
                    targets: 3,
                    data: 'parent.surname',
                    name: 'surname'
                },
                {
                    className: 'clickView',
                    targets: 4,
                    data: "parent.gender",
                    render: function (gender) {
                        if (gender == 1) {
                            return '<span class="badge-lg badge-soft-primary">' + l('Enum_Gender_' + gender) + '</a>';
                        }

                        if (gender == 2) {
                            return '<span class="badge-lg badge-soft-danger">' + l('Enum_Gender_' + gender) + '</a>';
                        }

                        return '<span class="badge-lg badge-soft-warning">' + l('Enum_Gender_' + gender) + '</a>';
                    },
                    name: 'gender'
                },
                {
                    className: 'clickView',
                    targets: 5,
                    data: "parent.birthDate",
                    name: "birthDate",
                    render: function (birthDate) {
                        if (birthDate) {
                            return moment(birthDate).format('L');
                        }
                        return "";
                    }
                },
                {
                    className: 'clickView',
                    targets: 6,
                    data: "parent.birthDate",
                    name: "birthDate",
                    render: function (birthDate) {
                        if (birthDate) {
                            return moment().diff(moment(birthDate, 'YYYYMMDD'), 'years');
                        }
                        return "";
                    }
                },
               
                {
                    className: 'clickView',
                    targets: 7,
                    data: 'parent.birthPlace',
                    name: 'birthPlace'
                },
                {
                    className: 'clickView',
                    targets: 8,
                    data: "parent.educationalStatus",
                    render: function (educationalStatus) {    
                        return l('Enum_EducationalStatus_' + educationalStatus) ;
                    },
                    name: 'gender'
                },
                {
                    className: 'clickView',
                    targets: 9,
                    data: 'parentTypeName',
                    name: 'parentTypeFk.name'
                },
                {
                    className: 'clickView',
                    targets: 10,
                    data: 'jobName',
                    name: 'jobFk.name'
                },
                {
                    className: 'clickView',
                    targets: 11,
                    data: 'parent.email',
                    name: 'email'
                },
                {
                    className: 'clickView',
                    targets: 12,
                    data: "phones",
                    name: "phones",
                    render: function (phones) {
                        var phoneData = "";
                        for (var i = 0; i < phones.length; i++) {
                            phoneData += '<a href="tel:+' + phones[i].phone.code + phones[i].phone.number + '">+' + phones[i].phone.code + ' ' + phones[i].phone.number + ' (' + l('Enum_PhoneType_' + phones[i].phone.type) + ')</a><br>';
                        }
                        return phoneData;
                    }
                },
                {
                    className: 'clickView',
                    targets: 13,
                    data: "addresses",
                    name: "addresses",
                    render: function (addresses) {
                        var adressData = "";
                        var location = "";
                        for (var i = 0; i < addresses.length; i++) {
                            if (addresses[i].townName != "") {
                                location += addresses[i].townName + '/';
                            }
                            if (addresses[i].cityName != "") {
                                location += addresses[i].cityName + '/';
                            }
                            if (addresses[i].countryName != "") {
                                location += addresses[i].countryName;
                            }
                            adressData += addresses[i].address.detail + ' ' + location + '<br>';
                            location = "";
                        }
                        return adressData;
                    }
                },
                {
                    className: 'text-align-right',
                    targets: 14,
                    data: null,
                    sortable: false,
                    autoWidth: false,
                    defaultContent: '',
                    render: (data, type, row, meta) => {
                        return [
                            ` <a class="btn btn-sm btn-outline-danger" href="javascript:;" name="btnDelete">`,
                            `    <i class= "tio-delete-outlined"></i> `, l('Delete'),
                            ` </a>`
                        ].join('');
                    }
                },
            ]
        });

        //Create
        $('#CreateNewParentButton').click(function () {
            createOrEdit();
        });

        //Edit
        $('#datatable tbody').on('click', '.clickView', function () {
            var data = datatable.row($(this).parents('tr')).data();

            if (data) {
                $("#EditParentButton").trigger("click");
                createOrEdit(data)
            }
        });

        //Delete
        $('#datatable tbody').on('click', '[name*=btnDelete]', function () {
            var data = datatable.row($(this).parents('tr')).data();
            if (data) {
                deleteParent(data.parent);
            }
        });

        //DeleteSelectedAll
        $("#DeleteSelectedAllButton").click(function myfunction() {
            var selectedRows = datatable.rows('.selected').data();

            var parents = [];

            for (var i = 0; i < selectedRows.length; i++) {
                parents.push(selectedRows[i].parent.id)
            }

            deleteSelectedAllParents(parents);
        });

        //View
        $('#datatable tbody').on('click', '[name*=btnView]', function () {
            var data = datatable.row($(this).parents('tr')).data();

            viewParent(data);
        });

        function createOrEdit(data) {
            var url = abp.appPath + 'Parents/CreateOrEditModal/';

            if (data) {
                url = url + data.parent.id
            }

            abp.ajax({
                url: url,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    $('#CreateOrEditModal div.modal-content').html(content);
                },
                error: function (e) { }
            });
        }

        function deleteParent(data) {
            abp.message.confirm(
                abp.utils.formatString(
                    l('AreYouSureWantToDelete'),
                    data.name + " " + data.surname),
                null,
                (isConfirmed) => {
                    if (isConfirmed) {
                        _parentsService.delete({
                            id: data.id
                        }).done(() => {
                            getParents(true);
                            abp.notify.info(l('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        }

        function deleteSelectedAllParents(data) {
            abp.message.confirm(
                abp.utils.formatString(
                    l('AreYouSureWantToDeleteSelectedAll')),
                null,
                (isConfirmed) => {
                    if (isConfirmed) {
                        _parentsService.deleteAll(
                            data
                        ).done(() => {
                            getParents(true);
                            abp.notify.info(l('SuccessfullyDeleted'));
                            $("#datatableCounterInfo").css("display", "none");
                        });
                    }
                }
            );
        }

        function viewParent(data) {
            abp.ajax({
                url: abp.appPath + 'Parents/ViewModal/' + data.parent.id,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    $('#ViewModal div.modal-content').html(content);
                },
                error: function (e) { }
            });
        }

        function getParents() {
            datatable.ajax.reload();
        }

        abp.event.on('parent.createOrEditParentModalSaved', function () {
            getParents();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getParents();
            }
        });

        //HideColumns
        $('#toggleColumn_identityNumber').change(function (e) {
            datatable.columns(1).visible(e.target.checked)
        });

        $('#toggleColumn_name').change(function (e) {
            datatable.columns(2).visible(e.target.checked)
        });

        $('#toggleColumn_surname').change(function (e) {
            datatable.columns(3).visible(e.target.checked)
        });

        $('#toggleColumn_gender').change(function (e) {
            datatable.columns(4).visible(e.target.checked)
        });

        $('#toggleColumn_birthDate').change(function (e) {
            datatable.columns(5).visible(e.target.checked)
        });

        $('#toggleColumn_age').change(function (e) {
            datatable.columns(6).visible(e.target.checked)
        });

        $('#toggleColumn_birthPlace').change(function (e) {
            datatable.columns(7).visible(e.target.checked)
        });

        $('#toggleColumn_educationalStatus').change(function (e) {
            datatable.columns(8).visible(e.target.checked)
        });

        $('#toggleColumn_parentType').change(function (e) {
            datatable.columns(9).visible(e.target.checked)
        });

        $('#toggleColumn_job').change(function (e) {
            datatable.columns(10).visible(e.target.checked)
        });
      
        $('#toggleColumn_email').change(function (e) {
            datatable.columns(11).visible(e.target.checked)
        });

        $('#toggleColumn_phone').change(function (e) {
            datatable.columns(12).visible(e.target.checked)
        });

        $('#toggleColumn_address').change(function (e) {
            datatable.columns(13).visible(e.target.checked)
        });

        //Export
        $('#export-copy').click(function () {
            datatable.button('.buttons-copy').trigger()
        });

        $('#export-excel').click(function () {
            datatable.button('.buttons-excel').trigger()
        });

        $('#export-csv').click(function () {
            datatable.button('.buttons-csv').trigger()
        });

        $('#export-pdf').click(function () {
            datatable.button('.buttons-pdf').trigger()
        });

        $('#export-print').click(function () {
            datatable.button('.buttons-print').trigger()
        });
    });
})();