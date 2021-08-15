(function ($) {
    var _roleService = abp.services.app.role;
    var l = abp.localization.getSource('CourseManagementSystem');
    var _$modal = $('#RoleCreateModal');
    var _$form = _$modal.find('form');
    var _$table = $('#datatable');

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
                '<img class="mb-3" src="./assets/svg/illustrations/sorry.svg" alt="Image Description" style="width: 7rem;">' +
                '<p class="mb-0">No data to show</p>' +
                '</div>'
        },
        ajax: function (data, callback, settings) {
            var filter = $('#RolesSearchForm').serializeFormToObject(true);
            filter.maxResultCount = data.length;
            filter.skipCount = data.start;

            abp.ui.setBusy(_$table);
            _roleService.getAll(filter).done(function (result) {
                callback({
                    recordsTotal: result.totalCount,
                    recordsFiltered: result.totalCount,
                    data: result.items
                });
            }).always(function () {
                abp.ui.clearBusy(_$table);
            });
        },
        columnDefs: [
            {
                targets: 0,
                className: 'table-column-pr-0',
                defaultContent: '<div class="custom-control custom-checkbox">' +
                    '<input type="checkbox" class= "custom-control-input" id="ordersCheck1">' +
                    '<label class="custom-control-label" for="ordersCheck1"></label>' +
                    '</div>',
                sortable: false
            },
            {
                className: 'clickView',
                targets: 1,
                data: 'name',
                sortable: false
            },
            {
                className: 'clickView',
                targets: 2,
                data: 'displayName',
                sortable: false
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
                        ` <a class="btn btn-sm btn-outline-danger delete-role" data-role-id="${row.id}" data-role-name="${row.name}">`,
                        `    <i class= "tio-delete-outlined"></i> Sil`,
                        ` </a>`
                    ].join('');
                }
            },
        ]
    });


    //Save
    _$form.find('.save-button').on('click', (e) => {
        e.preventDefault();

        if (!_$form.valid()) {
            return;
        }

        var role = _$form.serializeFormToObject();
        role.grantedPermissions = [];
        var _$permissionCheckboxes = _$form[0].querySelectorAll("input[name='permission']:checked");
        if (_$permissionCheckboxes) {
            for (var permissionIndex = 0; permissionIndex < _$permissionCheckboxes.length; permissionIndex++) {
                var _$permissionCheckbox = $(_$permissionCheckboxes[permissionIndex]);
                role.grantedPermissions.push(_$permissionCheckbox.val());
            }
        }

        abp.ui.setBusy(_$modal);
        _roleService
            .create(role)
            .done(function () {
                _$modal.modal('hide');
                _$form[0].reset();
                abp.notify.info(l('SavedSuccessfully'));
                datatable.ajax.reload();
            })
            .always(function () {
                abp.ui.clearBusy(_$modal);
            });
    });

    //Delete
    $(document).on('click', '.delete-role', function () {
        var roleId = $(this).attr("data-role-id");
        var roleName = $(this).attr('data-role-name');

        deleteRole(roleId, roleName);
    });

    //Edit
    $('#datatable tbody').on('click', '.clickView', function () {
        var data = datatable.row($(this).parents('tr')).data();

        if (data) {
            $("#EditRoleButton").trigger("click");
            abp.ajax({
                url: abp.appPath + 'Roles/EditModal?roleId=' + data.id,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    $('#RoleEditModal div.modal-content').html(content);
                },
                error: function (e) { }
            })
        }
    });

    abp.event.on('role.edited', (data) => {
        datatable.ajax.reload();
    });

    function deleteRole(roleId, roleName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                roleName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _roleService.delete({
                        id: roleId
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        datatable.ajax.reload();
                    });
                }
            }
        );
    }

    _$modal.on('shown.bs.modal', () => {
        _$modal.find('input:not([type=hidden]):first').focus();
    }).on('hidden.bs.modal', () => {
        _$form.clearForm();
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

    $(document).keypress(function (e) {
        if (e.which === 13) {
            getRoles();
        }
    });

    function getRoles() {
        datatable.ajax.reload();
    }

    $('#toggleColumn_roleName').change(function (e) {
        datatable.columns(1).visible(e.target.checked)
    })

    $('#toggleColumn_displayName').change(function (e) {
        datatable.columns(2).visible(e.target.checked)
    })

    // INITIALIZATION OF TAGIFY
    // =======================================================
    $('.js-tagify').each(function () {
        var tagify = $.HSCore.components.HSTagify.init($(this));
    });


    $('.btn-search').on('click', (e) => {
        _$rolesTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$rolesTable.ajax.reload();
            return false;
        }
    });

})(jQuery);
