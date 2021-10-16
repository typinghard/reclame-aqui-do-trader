$(document).ready(function () {
    HandleAsyncViews();
})

function HandleSyncPartialContent(element) {
    var url = $(element).data('url');
    $(element).load(url);
}

function HandleAsyncViews() {

    $(".asyncPartialContent").each(function () {
        $(this).html('');
        HandleSyncPartialContent(this);
    });
}