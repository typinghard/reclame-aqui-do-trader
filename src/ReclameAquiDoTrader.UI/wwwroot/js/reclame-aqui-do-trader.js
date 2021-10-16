$.ajaxSetup({ cache: false });
$(document).on('click', 'a.abre-modal-generico', function () {
    CarregarConteudoDoModal(this.href);
    return false;
});

function CarregarConteudoDoModal(url) {
    $('#modalGenerico .modal-content').load(url, function () {
        $('#modalGenerico').modal('show');
    });
}

function TratarErro(resultError) {

    console.log(resultError);

    if (resultError.responseJSON === undefined) {
        toastr.error('Ops, ocorreu um erro inesperado! Recarrega a página e tente novamente.', 'Erro');
    }

    let erros = resultError.responseJSON.erros;
    $(erros).each(function (i, erro) {
        toastr.error(erro.mensagem, 'Erro');
    });
}
