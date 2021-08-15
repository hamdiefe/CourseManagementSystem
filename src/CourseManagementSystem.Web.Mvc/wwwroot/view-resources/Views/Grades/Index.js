﻿(function () {
    $(function () {
        var l = abp.localization.getSource('CourseManagementSystem');
        var _$gradesTable = $('#datatable');
        var _gradesService = abp.services.app.grades;

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

                abp.ui.setBusy(_$gradesTable);
                _gradesService.getAll(filter).done(function (result) {
                    callback({
                        recordsTotal: result.totalCount,
                        recordsFiltered: result.totalCount,
                        data: result.items
                    });
                }).always(function () {
                    abp.ui.clearBusy(_$gradesTable);
                });
            },
            columnDefs: [
                {
                    className: 'table-column-pr-0',
                    targets: 0,
                    data: "grade.id",
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
                    data: 'grade.name',
                    name:'name'
                },
                {
                    className: 'text-align-right',
                    targets: 2,
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
        $('#CreateNewGradeButton').click(function () {
            createOrEdit();
        });

        //Edit
        $('#datatable tbody').on('click', '.clickView', function () {
            var data = datatable.row($(this).parents('tr')).data();

            if (data) {
                $("#EditGradeButton").trigger("click");
                createOrEdit(data)
            }
        });

        //Delete
        $('#datatable tbody').on('click', '[name*=btnDelete]', function () {
            var data = datatable.row($(this).parents('tr')).data();
            if (data) {
                deleteGrade(data.grade);
            }
        });

        //DeleteSelectedAll
        $("#DeleteSelectedAllButton").click(function myfunction() {
            var selectedRows = datatable.rows('.selected').data();

            var grades = [];

            for (var i = 0; i < selectedRows.length; i++) {
                grades.push(selectedRows[i].grade.id)
            }

            deleteSelectedAllGrades(grades);
        });

        //View
        $('#datatable tbody').on('click', '[name*=btnView]', function () {
            var data = datatable.row($(this).parents('tr')).data();

            viewGrade(data);
        });

        function createOrEdit(data) {
            var url = abp.appPath + 'Grades/CreateOrEditModal/';

            if (data) {
                url = url + data.grade.id
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

        function deleteGrade(data) {
            abp.message.confirm(
                abp.utils.formatString(
                    l('AreYouSureWantToDelete'),
                    data.name),
                null,
                (isConfirmed) => {
                    if (isConfirmed) {
                        _gradesService.delete({
                            id: data.id
                        }).done(() => {
                            getGrades(true);
                            abp.notify.info(l('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        }

        function deleteSelectedAllGrades(data) {
            abp.message.confirm(
                abp.utils.formatString(
                    l('AreYouSureWantToDeleteSelectedAll')),
                null,
                (isConfirmed) => {
                    if (isConfirmed) {
                        _gradesService.deleteAll(
                            data
                        ).done(() => {
                            getGrades(true);
                            abp.notify.info(l('SuccessfullyDeleted'));
                            $("#datatableCounterInfo").css("display", "none");
                        });
                    }
                }
            );
        }

        function viewGrade(data) {
            abp.ajax({
                url: abp.appPath + 'Grades/ViewModal/' + data.grade.id,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    $('#ViewModal div.modal-content').html(content);
                },
                error: function (e) { }
            });
        }

        function getGrades() {
            datatable.ajax.reload();
        }

        abp.event.on('grade.createOrEditGradeModalSaved', function () {
            getGrades();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getGrades();
            }
        });

        //HideColumns
        $('#toggleColumn_name').change(function (e) {
            datatable.columns(1).visible(e.target.checked)
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