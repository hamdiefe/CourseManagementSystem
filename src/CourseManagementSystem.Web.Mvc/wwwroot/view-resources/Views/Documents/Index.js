(function () {
    $(function () {
        var l = abp.localization.getSource('CourseManagementSystem');
        var _$documentsTable = $('#datatable');
        var _documentsService = abp.services.app.documents;

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

                abp.ui.setBusy(_$documentsTable);
                _documentsService.getAll(filter).done(function (result) {
                    callback({
                        recordsTotal: result.totalCount,
                        recordsFiltered: result.totalCount,
                        data: result.items
                    });
                }).always(function () {
                    abp.ui.clearBusy(_$documentsTable);
                });
            },
            columnDefs: [
                {
                    className: 'table-column-pr-0',
                    targets: 0,
                    data: "document.id",
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
                    data: "document.date",
                    name: "date",
                    render: function (date) {
                        if (date) {
                            return moment(date).format('L');
                        }
                        return "";
                    }
                },                
                {
                    className: 'clickView',
                    targets: 2,
                    data: "document.type",
                    render: function (type) {
                        if (type == 1) {
                            return '<span class="badge-lg badge-soft-primary">' + l('Enum_DocumentType_' + type) + '</a>';
                        }

                        if (type == 2) {
                            return '<span class="badge-lg badge-soft-danger">' + l('Enum_DocumentType_' + type) + '</a>';
                        }

                        return '<span class="badge-lg badge-soft-warning">' + l('Enum_DocumentType_' + type) + '</a>';
                    },
                    name: 'type'
                },   
                {
                    className: 'clickView',
                    targets: 3,
                    data: 'document.serie',
                    name: 'serie'
                }, {
                    className: 'clickView',
                    targets: 4,
                    data: 'document.no',
                    name: 'no'
                }, {
                    className: 'clickView',
                    targets: 5,
                    data: 'document.description',
                    name: 'description'
                },  
                {
                    className: 'clickView',
                    targets: 6,
                    data: 'document.debt',
                    name: 'debt'
                }, 
                {
                    className: 'clickView',
                    targets: 7,
                    data: 'document.credit',
                    name: 'credit'
                },  
                {
                    className: 'clickView',
                    targets: 8,
                    data: 'document.remaining',
                    name: 'remaining'
                },  
                {
                    className: 'clickView',
                    targets: 9,
                    data: 'studentName',
                    name: 'studentName'
                },  
                {
                    className: 'text-align-right',
                    targets: 10,
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
        $('#CreateNewDocumentButton').click(function () {
            createOrEdit();
        });

        //Edit
        $('#datatable tbody').on('click', '.clickView', function () {
            var data = datatable.row($(this).parents('tr')).data();

            if (data) {
                $("#EditDocumentButton").trigger("click");
                createOrEdit(data)
            }
        });

        //Delete
        $('#datatable tbody').on('click', '[name*=btnDelete]', function () {
            var data = datatable.row($(this).parents('tr')).data();
            if (data) {
                deleteDocument(data.document);
            }
        });

        //DeleteSelectedAll
        $("#DeleteSelectedAllButton").click(function myfunction() {
            var selectedRows = datatable.rows('.selected').data();

            var documents = [];

            for (var i = 0; i < selectedRows.length; i++) {
                documents.push(selectedRows[i].document.id)
            }

            deleteSelectedAllDocuments(documents);
        });

        //View
        $('#datatable tbody').on('click', '[name*=btnView]', function () {
            var data = datatable.row($(this).parents('tr')).data();

            viewDocument(data);
        });

        function createOrEdit(data) {
            var url = abp.appPath + 'Documents/CreateOrEditModal/';

            if (data) {
                url = url + data.document.id
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

        function deleteDocument(data) {
            abp.message.confirm(
                abp.utils.formatString(
                    l('AreYouSureWantToDelete'),
                    data.name),
                null,
                (isConfirmed) => {
                    if (isConfirmed) {
                        _documentsService.delete({
                            id: data.id
                        }).done(() => {
                            getDocuments(true);
                            abp.notify.info(l('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        }

        function deleteSelectedAllDocuments(data) {
            abp.message.confirm(
                abp.utils.formatString(
                    l('AreYouSureWantToDeleteSelectedAll')),
                null,
                (isConfirmed) => {
                    if (isConfirmed) {
                        _documentsService.deleteAll(
                            data
                        ).done(() => {
                            getDocuments(true);
                            abp.notify.info(l('SuccessfullyDeleted'));
                            $("#datatableCounterInfo").css("display", "none");
                        });
                    }
                }
            );
        }

        function viewDocument(data) {
            abp.ajax({
                url: abp.appPath + 'Documents/ViewModal/' + data.document.id,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    $('#ViewModal div.modal-content').html(content);
                },
                error: function (e) { }
            });
        }

        function getDocuments() {
            datatable.ajax.reload();
        }

        abp.event.on('document.createOrEditDocumentModalSaved', function () {
            getDocuments();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getDocuments();
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