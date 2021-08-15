(function () {
    $(function () {
        var l = abp.localization.getSource('CourseManagementSystem');
        var _$auditLogsTable = $('#datatable');
        var _auditLogsService = abp.services.app.auditLogs;

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
                    '<img class="mb-3" src="/assets/svg/illustrations/sorry.svg" alt="' + l('NoData') + '" style="width: 7rem;">' +
                    '<p class="mb-0">' + l('NoData') + '</p>' +
                    '</div>'
            },
            ajax: function (data, callback, settings) {
                var filter = $('#datatableSearch').serializeFormToObject(true);
                filter.maxResultCount = data.length;
                filter.skipCount = data.start;
                console.log(filter);
                if (data.order[0]) {
                    filter.sorting = data.columns[data.order[0].column].name + ' ' + data.order[0].dir;
                }

                abp.ui.setBusy(_$auditLogsTable);
                _auditLogsService.getAll(filter).done(function (result) {
                    callback({
                        recordsTotal: result.totalCount,
                        recordsFiltered: result.totalCount,
                        data: result.items
                    });
                }).always(function () {
                    abp.ui.clearBusy(_$auditLogsTable);
                });
            },
            columnDefs: [
                {
                    className: 'clickView',
                    targets: 0,
                    data: 'auditLog.clientIpAddress',
                    name: 'clientIpAddress'
                },
                {
                    className: 'clickView',
                    targets: 1,
                    data: 'userName',
                    name: 'userName'
                },
                {
                    className: 'clickView',
                    targets: 2,
                    data: 'auditLog.executionDuration',
                    name: 'executionDuration'
                },
                {
                    className: 'clickView',
                    targets: 3,
                    data: "auditLog.executionTime",
                    name: "executionTime",
                    render: function (executionTime) {
                        if (executionTime) {
                            return moment(executionTime).format('L') + " " + moment(executionTime).format('LTS');
                        }
                        return "";
                    }
                },
                {
                    className: 'clickView',
                    targets: 4,
                    data: 'auditLog.serviceName',
                    name: 'serviceName'
                },
                {
                    className: 'clickView',
                    targets: 5,
                    data: 'auditLog.methodName',
                    name: 'methodName'
                },
                {
                    className: 'clickView',
                    targets: 6,
                    data: 'exceptionStatus',
                    name: 'exceptionStatus'
                }

            ]
        });

        //View
        $('#datatable tbody').on('click', '.clickView', function () {
            var data = datatable.row($(this).parents('tr')).data();

            $("#ViewAuditLogButton").trigger("click");
            viewAuditLog(data);
        });

        function viewAuditLog(data) {
            abp.ajax({
                url: abp.appPath + 'AuditLogs/ViewModal/' + data.auditLog.id,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    $('#ViewModal div.modal-content').html(content);
                },
                error: function (e) { }
            });
        }

        function getAuditLogs() {
            datatable.ajax.reload();
        }

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getAuditLogs();
            }
        });

        $('.js-select2-custom').each(function () {
            var select2 = $.HSCore.components.HSSelect2.init($(this));
        });

        $("select[name='exceptionFilter']").on('change', function () {
            getAuditLogs();
        });
    });
})();