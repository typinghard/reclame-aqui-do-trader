var loadingIconForAsyncView = '';
HandleAsyncViews();

function RecarregarAsyncViews() {
    $(".asyncPartialContent").unbind('click');
    $("a.partialContent").unbind('click');
    HandleAsyncViews();
}

function HandleSyncPartialContent(element) {
    var url = $(element).data('url');
    $(element).load(url);
}

function HandleAsyncViews() {

    $(".asyncPartialContent").each(function () {
        $(this).html(loadingIconForAsyncView);
        HandleSyncPartialContent(this);
    });

    /* Tabs - ClickEvent*/
    $('a.partialContent').click(function (evt) {
        if ($(this).hasClass('hasPreloader'))
            $.preloader.start({ modal: true, src: 'sprites.png' });
        evt.preventDefault();
        var url = $(this).data('url');
        var target = $(this).attr('href');
        var isModalAsync = $(this).hasClass('modal-async');
        $(target).find('.partialContentChild').load(url, function () {
            if (isModalAsync)
                $("#modalAsync").modal('show');
            $.preloader.stop();
        });
    });

    /* Hide/Show DIV by style */
    var targetsNode = document.getElementsByClassName('div.partialContent');
    $(targetsNode).each(function (index, targetNode) {
        var observer = new MutationObserver(function () {
            if ($(targetNode).is(":visible")) {
                var url = $(targetNode).data('url');
                $(targetNode.querySelector('.partialContentChild')).load(url);
            }
        });
        observer.observe(targetNode, { attributes: true, attributeFilter: ['style'], attributeOldValue: true });
    });
}