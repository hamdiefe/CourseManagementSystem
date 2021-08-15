(function ($) {

    abp.event.on('mySettings.profileImageSaved', function () {
        $("#ProfileImageSaved").show();
        window.location.reload();
    });

    abp.event.on('mySettings.basicInformationsSaved', function () {
        $("#BasicInformationsSaved").show();
    });

    abp.event.on('mySettings.passwordSaved', function () {
        $("#PasswordSaved").show();
    });

    // INITIALIZATION OF FILE ATTACH
    // =======================================================
    $('.js-file-attach').each(function () {
        var customFile = new HSFileAttach($(this)).init();
    });


    // INITIALIZATION OF MASKED INPUT
    // =======================================================
    $('.js-masked-input').each(function () {
        var mask = $.HSCore.components.HSMask.init($(this));
    });


    // INITIALIZATION OF STICKY BLOCKS
    // =======================================================
    $('.js-sticky-block').each(function () {
        var stickyBlock = new HSStickyBlock($(this), {
            targetSelector: $('#header').hasClass('navbar-fixed') ? '#header' : null
        }).init();
    });


    // INITIALIZATION OF SCROLL NAV
    // =======================================================
    var scrollspy = new HSScrollspy($('body'), {
        // !SETTING "resolve" PARAMETER AND RETURNING "resolve('completed')" IS REQUIRED
        beforeScroll: function (resolve) {
            if (window.innerWidth < 992) {
                $('#navbarVerticalNavMenu').collapse('hide').on('hidden.bs.collapse', function () {
                    return resolve('completed');
                });
            } else {
                return resolve('completed');
            }
        }
    });


    // INITIALIZATION OF PASSWORD STRENGTH MODULE
    // =======================================================
    $('.js-pwstrength').each(function () {
        var pwstrength = $.HSCore.components.HSPWStrength.init($(this));
    });

})(jQuery);