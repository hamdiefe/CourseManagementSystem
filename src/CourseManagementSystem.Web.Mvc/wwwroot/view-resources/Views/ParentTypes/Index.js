(function () {
    $(function () {
        var l = abp.localization.getSource('CourseManagementSystem');
        var _$parentTypesTable = $('#datatable');
        var _parentTypesService = abp.services.app.parentTypes;

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

                abp.ui.setBusy(_$parentTypesTable);
                _parentTypesService.getAll(filter).done(function (result) {
                    callback({
                        recordsTotal: result.totalCount,
                        recordsFiltered: result.totalCount,
                        data: result.items
                    });
                }).always(function () {
                    abp.ui.clearBusy(_$parentTypesTable);
                });
            },
            columnDefs: [
                {
                    className: 'table-column-pr-0',
                    targets: 0,
                    data: "parentType.id",
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
                    data: 'parentType.name',
                    name: 'name'
                },
                {
                    className: 'clickView',
                    targets: 2,
                    data: "parentType.degree",
                    render: function (degree) {
                        return l('Enum_ParentDegree_' + degree);
                    },
                    name: 'degree'
                },
                {
                    className: 'text-align-right',
                    targets: 3,
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
        $('#CreateNewParentTypeButton').click(function () {
            createOrEdit();
        });

        //Edit
        $('#datatable tbody').on('click', '.clickView', function () {
            var data = datatable.row($(this).parents('tr')).data();

            if (data) {
                $("#EditParentTypeButton").trigger("click");
                createOrEdit(data)
            }
        });

        //Delete
        $('#datatable tbody').on('click', '[name*=btnDelete]', function () {
            var data = datatable.row($(this).parents('tr')).data();
            if (data) {
                deleteParentType(data.parentType);
            }
        });

        //DeleteSelectedAll
        $("#DeleteSelectedAllButton").click(function myfunction() {
            var selectedRows = datatable.rows('.selected').data();

            var parentTypes = [];

            for (var i = 0; i < selectedRows.length; i++) {
                parentTypes.push(selectedRows[i].parentType.id)
            }

            deleteSelectedAllParentTypes(parentTypes);
        });

        //View
        $('#datatable tbody').on('click', '[name*=btnView]', function () {
            var data = datatable.row($(this).parents('tr')).data();

            viewParentType(data);
        });

        function createOrEdit(data) {
            var url = abp.appPath + 'ParentTypes/CreateOrEditModal/';

            if (data) {
                url = url + data.parentType.id
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

        function deleteParentType(data) {
            abp.message.confirm(
                abp.utils.formatString(
                    l('AreYouSureWantToDelete'),
                    data.name),
                null,
                (isConfirmed) => {
                    if (isConfirmed) {
                        _parentTypesService.delete({
                            id: data.id
                        }).done(() => {
                            getParentTypes(true);
                            abp.notify.info(l('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        }

        function deleteSelectedAllParentTypes(data) {
            abp.message.confirm(
                abp.utils.formatString(
                    l('AreYouSureWantToDeleteSelectedAll')),
                null,
                (isConfirmed) => {
                    if (isConfirmed) {
                        _parentTypesService.deleteAll(
                            data
                        ).done(() => {
                            getParentTypes(true);
                            abp.notify.info(l('SuccessfullyDeleted'));
                            $("#datatableCounterInfo").css("display", "none");
                        });
                    }
                }
            );
        }

        function viewParentType(data) {
            abp.ajax({
                url: abp.appPath + 'ParentTypes/ViewModal/' + data.parentType.id,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    $('#ViewModal div.modal-content').html(content);
                },
                error: function (e) { }
            });
        }

        function getParentTypes() {
            datatable.ajax.reload();
        }

        abp.event.on('parentType.createOrEditParentTypeModalSaved', function () {
            getParentTypes();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getParentTypes();
            }
        });

        //HideColumns
        $('#toggleColumn_name').change(function (e) {
            datatable.columns(1).visible(e.target.checked)
        });

        $('#toggleColumn_parentDegree').change(function (e) {
            datatable.columns(2).visible(e.target.checked)
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