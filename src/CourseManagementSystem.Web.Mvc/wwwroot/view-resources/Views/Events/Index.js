(function () {
    $(function () {
        var l = abp.localization.getSource('CourseManagementSystem');
        var _$eventsTable = $('#datatable');
        var _eventsService = abp.services.app.events;

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

                abp.ui.setBusy(_$eventsTable);
                _eventsService.getAll(filter).done(function (result) {
                    callback({
                        recordsTotal: result.totalCount,
                        recordsFiltered: result.totalCount,
                        data: result.items
                    });
                }).always(function () {
                    abp.ui.clearBusy(_$eventsTable);
                });
            },
            columnDefs: [
                //{
                //    className: 'table-column-pr-0',
                //    targets: 0,
                //    data: "event.id",
                //    render: function (id) {
                //        if (id) {
                //            return '<div class="custom-control custom-checkbox">' +
                //                '<input type="checkbox" class= "custom-control-input" data-id="' + id + '" id="check' + id + '">' +
                //                '<label class="custom-control-label" for="check' + id + '"></label>' +
                //                '</div>';
                //        }
                //        return "";
                //    },
                //    sortable: false
                //},
                {
                    className: 'clickView',
                    targets: 0,
                    data: 'event.title',
                    name: 'title'
                },
                {
                    className: 'clickView',
                    targets: 1,
                    data: 'event.description',
                    name: 'description'
                },
                {
                    className: 'clickView',
                    targets: 2,
                    data: 'studentName',
                    name: 'studentName'
                },
                {
                    className: 'clickView',
                    targets: 3,
                    data: 'teacherName',
                    name: 'teacherName'
                },
                {
                    className: 'clickView',
                    targets: 4,
                    data: "event.startDate",
                    name: 'startDate',
                    render: function (startDate) {
                        return  moment(startDate).format('lll');
                    },
                },
                {
                    className: 'clickView',
                    targets: 5,
                    data: "event.endDate",
                    name: 'endDate',
                    render: function (endDate) {
                        if (endDate) {
                            return  moment(endDate).format('lll');
                        }
                        return "";
                    },
                },
                {
                    className: 'clickView',
                    targets: 6,
                    data: 'event.totalHours',
                    name: 'totalHours'
                },
                {
                    className: 'clickView',
                    targets: 7,
                    data: "event.type",
                    render: function (type) {
                        return l('Enum_EventType_' + type);
                    },
                    name: 'gender'
                },
                {
                    className: 'clickView',
                    targets: 8,
                    data: "event.status",
                    render: function (status) {
                        if (status == 0) {
                            return '<span class="badge-lg badge-soft-warning">' + l('Enum_EventStatus_' + status) + '</a>';
                        }

                        if (status == 1) {
                            return '<span class="badge-lg badge-soft-success">' + l('Enum_EventStatus_' + status) + '</a>';
                        }

                        if (status == 2) {
                            return '<span class="badge-lg badge-soft-danger">' + l('Enum_EventStatus_' + status) + '</a>';
                        }
                    },
                    name: 'status'
                },
                {
                    className: 'text-align-right',
                    targets: 9,
                    data: null,
                    sortable: false,
                    autoWidth: false,
                    defaultContent: '',
                    render: (data, type, row, meta) => {
                        var actionsHtml = "";

                        if (row.event.type != 1) {
                            actionsHtml = ' <a class="btn btn-sm btn-outline-danger" href="javascript:;" name="btnDelete">' +
                                '    <i class= "tio-delete-outlined"></i> ' + l("Delete") +
                                '</a>';
                        }

                        return [
                            actionsHtml
                        ].join('');
                    }
                },
            ]
        });

        //Create
        $('#CreateNewEventButton').click(function () {
            createOrEdit();
        });

        //Edit
        $('#datatable tbody').on('click', '.clickView', function () {

            var data = datatable.row($(this).parents('tr')).data();

            if (data && data.event.type != 1) {
                $("#EditEventButton").trigger("click");
                createOrEdit(data)
            }
        });

        //Delete
        $('#datatable tbody').on('click', '[name*=btnDelete]', function () {
            var data = datatable.row($(this).parents('tr')).data();

            if (data) {
                deleteEvent(data.event);
            }
        });

        //DeleteSelectedAll
        $("#DeleteSelectedAllButton").click(function myfunction() {
            var selectedRows = datatable.rows('.selected').data();

            var events = [];

            for (var i = 0; i < selectedRows.length; i++) {
                events.push(selectedRows[i].event.id)
            }

            deleteSelectedAllEvents(events);
        });

        function createOrEdit(data) {
            var url = abp.appPath + 'Events/CreateOrEditModal/';

            if (data) {
                url = url + data.event.id
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

        function deleteEvent(data) {
            console.log(data)
            abp.message.confirm(
                abp.utils.formatString(
                    l('AreYouSureWantToDelete'),
                    data.title),
                null,
                (isConfirmed) => {
                    if (isConfirmed) {
                        _eventsService.delete({
                            id: data.id
                        }).done(() => {
                            _eventsService.refreshPayments(data.studentId).done(function () {
                                getEvents(true);
                                abp.notify.info(l('SuccessfullyDeleted'));
                            }).always(function () {

                            });                           
                        });
                    }
                }
            );
        }

        function deleteSelectedAllEvents(data) {
            abp.message.confirm(
                abp.utils.formatString(
                    l('AreYouSureWantToDeleteSelectedAll')),
                null,
                (isConfirmed) => {
                    if (isConfirmed) {
                        _eventsService.deleteAll(
                            data
                        ).done(() => {
                            getEvents(true);
                            abp.notify.info(l('SuccessfullyDeleted'));
                            $("#datatableCounterInfo").css("display", "none");
                        });
                    }
                }
            );
        }

        function viewEvent(data) {
            abp.ajax({
                url: abp.appPath + 'Events/ViewModal/' + data.event.id,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    $('#ViewModal div.modal-content').html(content);
                },
                error: function (e) { }
            });
        }

        function getEvents() {
            datatable.ajax.reload();
        }

        abp.event.on('event.createOrEditEventModalSaved', function () {
            getEvents();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getEvents();
            }
        });

        //HideColumns
        $('#toggleColumn_title').change(function (e) {
            datatable.columns(0).visible(e.target.checked)
        });

        $('#toggleColumn_description').change(function (e) {
            datatable.columns(1).visible(e.target.checked)
        });

        $('#toggleColumn_student').change(function (e) {
            datatable.columns(2).visible(e.target.checked)
        });

        $('#toggleColumn_teacher').change(function (e) {
            datatable.columns(3).visible(e.target.checked)
        });

        $('#toggleColumn_startDate').change(function (e) {
            datatable.columns(4).visible(e.target.checked)
        });

        $('#toggleColumn_endDate').change(function (e) {
            datatable.columns(5).visible(e.target.checked)
        });

        $('#toggleColumn_totalHours').change(function (e) {
            datatable.columns(6).visible(e.target.checked)
        });

        $('#toggleColumn_type').change(function (e) {
            datatable.columns(7).visible(e.target.checked)
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