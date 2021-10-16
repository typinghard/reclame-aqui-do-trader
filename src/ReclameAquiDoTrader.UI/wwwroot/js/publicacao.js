$(document).ready(function () {
    CarregaComponentes();

    function CarregaComponentes() {


        $('.ladda-button-demo').ladda();

    }

    $('.btn-download').on("click", function (e) {

        var obj = {
            Id: $('#id').val()
        };
        console.log(obj);

        var t = $("input[name='__RequestVerificationToken']").val();

        $('.ladda-button-demo').ladda('start');

        $.ajax({
            headers:
            {
                "RequestVerificationToken": t
            },
            url: $(this).data('url'),
            type: 'post',
            contentType: "application/json;",
            data: JSON.stringify(obj),
            success: function (result) {

                toastr.success('Download realizado!', 'Sucesso');

                Download(result.nomeArquivo, result.content);

                if ($('.painel-avaliacao').length > 0)
                    $('.painel-avaliacao').load(result.url, function () {
                        $('#modalGenerico').modal('hide');
                    });
            },
            error: function (result) {

                if (result.responseJSON === undefined) {
                    toastr.error('Ops, ocorreu um erro inesperado! Recarrega a página e tente novamente.', 'Erro');
                }

            },
            complete: function () {
                $('.ladda-button-demo').ladda('stop');
            }
        });

    });


    function Download(filename, text) {
        var element = document.createElement('a');
        element.setAttribute('href', 'data:' +text.contentType+';charset=utf-8,' + encodeURIComponent(text.fileContents));
        element.setAttribute('download', filename);

        element.style.display = 'none';
        document.body.appendChild(element);

        element.click();

        document.body.removeChild(element);
    }

});
