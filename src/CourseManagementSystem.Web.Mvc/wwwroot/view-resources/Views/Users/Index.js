(function ($) {
    var _userService = abp.services.app.user;
    var l = abp.localization.getSource('CourseManagementSystem');
    var _$modal = $('#UserCreateModal');
    var _$form = _$modal.find('form');
    var _$table = $('#datatable');

    var _$usersTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        language: {
            zeroRecords: '<div class="text-center p-4">' +
                '<img class="mb-3" src="/front-admin/assets/svg/illustrations/sorry.svg" alt="' + l('NoDataToShow') + '" style="width: 7rem;">' +
                '<p class="mb-0">' + l('NoDataToShow') + '</p>' +
                '</div>'
        },
        ajax: function (data, callback, settings) {
            var filter = $('#UsersSearchForm').serializeFormToObject(true);
            filter.maxResultCount = data.length;
            filter.skipCount = data.start;

            abp.ui.setBusy(_$table);
            _userService.getAll(filter).done(function (result) {
                callback({
                    recordsTotal: result.totalCount,
                    recordsFiltered: result.totalCount,
                    data: result.items
                });
            }).always(function () {
                abp.ui.clearBusy(_$table);
            });
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$usersTable.draw(false)
            }
        ],
        responsive: {
            details: {
                type: 'column'
            }
        },
        columnDefs: [
            {
                className: 'table-column-pr-0',
                targets: 0,
                data: "id",
                render: function (id) {
                    if (id) {
                        return '<div class="custom-control custom-checkbox">' +
                            '<input type="checkbox" class= "custom-control-input" data-id="' + id + '" id="check' + id + '">' +
                            '</div>';
                    }
                    return "";
                },
                sortable: false
            },
            {
                className: 'clickView',
                targets: 1,
                data: 'userName',
                sortable: false
            },
            {
                className: 'clickView',
                targets: 2,
                data: 'fullName',
                sortable: false
            },
            {
                className: 'clickView',
                targets: 3,
                data: 'emailAddress',
                sortable: false
            },
            {
                className: 'clickView',
                targets: 4,
                data: 'isActive',
                sortable: false,
                render: (data, type, row, meta) => {
                    var result = l("No");
                    if (row.isActive == true) {
                        result = l("Yes");
                    }

                    return result;
                }
            },
            {
                className: 'text-align-right',
                targets: 5,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    return [
                        ` <a class="btn btn-sm btn-outline-danger delete-user" href="javascript:;" data-user-id="${row.id}" data-user-name="${row.name}">`,
                        `    <i class= "tio-delete-outlined"></i> `, l('Delete'),
                        ` </a>`
                    ].join('');
                }
            }
        ]
    });

    _$form.validate({
        rules: {
            Password: "required",
            ConfirmPassword: {
                equalTo: "#Password"
            }
        }
    });

    _$form.find('.save-button').on('click', (e) => {
        e.preventDefault();

        if (!_$form.valid()) {
            return;
        }

        var user = _$form.serializeFormToObject();
        user.roleNames = [];
        var _$roleCheckboxes = _$form[0].querySelectorAll("input[name='role']:checked");
        if (_$roleCheckboxes) {
            for (var roleIndex = 0; roleIndex < _$roleCheckboxes.length; roleIndex++) {
                var _$roleCheckbox = $(_$roleCheckboxes[roleIndex]);
                user.roleNames.push(_$roleCheckbox.val());
            }
        }

        abp.ui.setBusy(_$modal);
        _userService.create(user).done(function () {
            _$modal.modal('hide');
            _$form[0].reset();
            abp.notify.info(l('SavedSuccessfully'));
            _$usersTable.ajax.reload();
        }).always(function () {
            abp.ui.clearBusy(_$modal);
        });
    });

    $(document).on('click', '.delete-user', function () {
        var userId = $(this).attr("data-user-id");
        var userName = $(this).attr('data-user-name');

        deleteUser(userId, userName);
    });

    function deleteUser(userId, userName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                userName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _userService.delete({
                        id: userId
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        _$usersTable.ajax.reload();
                    });
                }
            }
        );
    }

    $(document).on('click', '.edit-user', function (e) {
        var userId = $(this).attr("data-user-id");

        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'Users/EditModal?userId=' + userId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#UserEditModal div.modal-content').html(content);
            },
            error: function (e) { }
        });
    });

    $(document).on('click', 'a[data-target="#UserCreateModal"]', (e) => {
        $('.nav-tabs a[href="#user-details"]').tab('show')
    });

    abp.event.on('user.edited', (data) => {
        _$usersTable.ajax.reload();
    });

    _$modal.on('shown.bs.modal', () => {
        _$modal.find('input:not([type=hidden]):first').focus();
    }).on('hidden.bs.modal', () => {
        _$form.clearForm();
    });

    $('.btn-search').on('click', (e) => {
        _$usersTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$usersTable.ajax.reload();
            return false;
        }
    });

    //Edit
    $('#datatable tbody').on('click', '.clickView', function () {
        var data = _$usersTable.row($(this).parents('tr')).data();

        if (data) {
            $("#EditUserButton").trigger("click");
            abp.ajax({
                url: abp.appPath + 'Users/EditModal?userId=' + data.id,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    $('#UserEditModal div.modal-content').html(content);
                },
                error: function (e) { }
            });        }
    });
})(jQuery);
