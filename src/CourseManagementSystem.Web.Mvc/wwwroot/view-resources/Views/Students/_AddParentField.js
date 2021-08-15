(function ($) {  
    var _$parentModal = $("#CreateOrEditParentModal");

    var _$studentTabContent = $("#CreateOrEditStudentModalTabContent");
    var _$studentForm = _$studentTabContent.find('form');

    //EditParent
    _$studentForm.find("a[name='editParent']").click(function (e) {
        var parentId = _$studentForm.find(this).closest(".parent-container").find("input[name='parentId']").val();
        e.preventDefault();

        createOrEditParent(parentId);
    });

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

    //DeleteParent
    _$studentForm.find(".delete-parent").click(function () {
        var element = _$studentForm.find(this);
        element.closest(".parent-container").remove();
    });
   
})(jQuery);

