$(document).ready(function () {

    CarregaComponentes();

    ValueObjects();


    function ValueObjects() {
        AdicionarRedeSocial();
    }

    function CarregaComponentes() {
        listaRedesSociais = new Array();

        var listaRedesSociaisAux = [];
        var listaComprovantesAux = [];

        $('.ladda-button-demo').ladda();

        if ($('#resposta .input-group.date').length > 0)
            $('#resposta .input-group.date').datepicker({
                todayBtn: "linked",
                keyboardNavigation: false,
                forceParse: false,
                calendarWeeks: true,
                autoclose: true,
                format: "dd/mm/yyyy"
            });

        if ($('#expiracaoss .input-group.date').length > 0)
            var em = $('#expiracao .input-group.date').datepicker({
                todayBtn: "linked",
                keyboardNavigation: false,
                forceParse: false,
                calendarWeeks: true,
                autoclose: true,
                format: "dd/mm/yyyy"
            });


        carregaSelect2Cadastro();
    }

    function formataIconeRedeSocial(state) {
        if (!state.id) {
            return state.text;
        }

        var baseUrl = "/user/pages/images/flags";
        var $state = $(
            '<span>'
            + RetornaIcone(state)
            + '<span></span></span>'
        );

        // Use .text() instead of HTML string concatenation to avoid script injection issues
        $state.find("span").text(state.text);
        //$state.find("img").attr("src", baseUrl + "/" + state.element.value.toLowerCase() + ".png");

        return $state;
    }

    function RetornaIcone(state) {
        var icone;
        switch (state.element.index) {
            case 0:
                icone = '<i class="fa fa-facebook-square"></i>';
                break;
            case 1:
                icone = '<i class="fa fa-instagram"></i>';
                break;
            default:
                icone = '';
                break;
        }

        return icone;
    }

    function carregaSelect2Cadastro() {

        $(".select2_mentor").select2({
            dropdownParent: $('.modal-body'),
            theme: 'bootstrap4',
            placeholder: "Selecione um Mentor",
            allowClear: true
        });

        $(".select2_redesocial").select2({
            dropdownParent: $('.modal-body'),
            theme: 'bootstrap4',
            placeholder: "Selecione uma Rede Social",
            allowClear: true,
            templateSelection: formataIconeRedeSocial
        });


        $('.select2_mentor').on('select2:unselect', function (e) {
            var data = e.params.data;
            //limpa o select2_redeSocial e desabilita
            $('.select2_redesocial').val(null).trigger('change');
        });

        $('.select2_mentor').on('select2:select', function (e) {
            var data = e.params.data;
            //ajax para recuperar as redes sociais do mentor escolhido

        });


        $('.select2_redesocial').on('select2:unselect', function (e) {
            var data = e.params.data;

        });

        $('.select2_redesocial').on('select2:select', function (e) {
            var data = e.params.data;

        });
    }

    function RemoverRedeSocial() {
        $('.btn-remover-redeSocial').unbind('click');
        $('.btn-remover-redeSocial').on("click", function (e) {

            var index = listaRedesSociais.findIndex(n => n.Url === $(this).data('redeSocial'));

            listaRedesSociais.splice(index, 1);
            $(this).parent().remove();
        });
    }

    function AdicionarRedeSocial() {

        $('.btn-salvar-redeSocial').on("click", function (e) {

            if ($("#campoRedeSocial").val().length === 0
                || $("#campoUrl").val().length === 0)
                return;

            var tipoRedeSocial = parseInt($('#campoRedeSocial').val());
            var url = $('#campoUrl').val();
            var usuario = "";

            var conteudolista = url + '/' + usuario;
            listaRedesSociais.push({
                Url: url,
                Usuario: usuario,
                Tipo: tipoRedeSocial
            });

            var colunaAcao = ' <a data-toggle="tooltip" class="col-sm btn btn-remover-redeSocial acao-telefone" data-url="' + conteudolista + '"  title="Remover">'
                + '<i class="fa fa-times text-danger" data-toggle="tooltip" title="Remover" style="width:22px;"></i>'
                + '</a>';

            var html = '<div class="col-sm row pb-1 border-bottom border-top m-0">'
                + '<div class="col-sm pt-2 numero-telefone" data-url="' + url + '" data-tipo="' + tipoRedeSocial + '">'
                + '<span data-toggle="tooltip" title="' + url + '">'
                + '<a href="' + url + '" target="_blank">'
                + url
                + '</a>'
                + '</span>'
                + '</div>'
                + colunaAcao
                + '</div>';

            console.log(html);

            $('#divRedesSociais').append(html);

            $('.select2_redesocial').val(null).trigger('change');
            $('#campoUrl').val("");

            RemoverRedeSocial();
        });
    }

    function VerificaNome() {
        if ($('.select2_mentor').val().length === 0) {
            $('.form-group-nome').toggleClass('has-error');
            $('.msg-validacao-mentor').toggleClass('invisible');
            $('.select2_mentor').focus();
            return false;
        }
        else {

            if ($('.form-group-nome').hasClass('has-error')) {
                $('.form-group-nome').toggleClass('has-error');
                $('.msg-validacao-mentor').toggleClass('invisible');
            }


        }

        return true;
    }


    function AtualizarPagina(result) {

        console.log(result);

        if ($('.painel-principal').length > 0) {
            $('#modalGenerico').modal('hide');
            window.location.href = result.url_painel_principal;
        }

        if ($('#lista_avaliacoes').length > 0) {
            $('#modalGenerico').modal('hide');
            window.location.href = result.url_avaliacoes;
        }

        if ($('#lista_avaliacoes_publicadas').length > 0) {
            $('#modalGenerico').modal('hide');
            window.location.href = result.lista_avaliacoes_publicadas;
        }
    }

    $('.btn-salvar').on("click", function (e) {

        if (VerificaNome()) {

            var form = CriarFormData(false);
            var t = $("input[name='__RequestVerificationToken']").val();

            $('.ladda-button-demo').ladda('start');

            $.ajax({
                headers:
                {
                    "RequestVerificationToken": t
                },
                url: $(this).data('url'),
                type: 'post',
                contentType: false,
                processData: false,
                data: form,
                success: function (result) {

                    AtualizarPagina(result);

                    toastr.success('Avaliação cadastrada com sucesso! Dashboard será atualizado!', 'Sucesso');
                },
                error: function (result) {

                    if (result.responseJSON !== undefined) {
                        $('.form-group-nome').toggleClass('has-error');
                        $('#nome').focus();
                        $('.msg-validacao').val("Campo avaliação não pode ser vazio!");
                    }

                    TratarErro(result);

                },
                complete: function () {
                    $('.ladda-button-demo').ladda('stop');
                }
            });
        }
    });

    $('.btn-alterar').on("click", function (e) {

        if (VerificaNome()) {

            var form = CriarFormData(true);
            var t = $("input[name='__RequestVerificationToken']").val();

            $('.ladda-button-demo').ladda('start');

            $.ajax({
                headers:
                {
                    "RequestVerificationToken": t
                },
                url: $(this).data('url'),
                type: 'post',
                contentType: false,
                processData: false,
                data: form,
                success: function (result) {

                    AtualizarPagina(result);

                    toastr.success('Avaliação atualizada com sucesso! Dashboard será atualizado!', 'Sucesso');
                },
                error: function (result) {
                    if (result.responseJSON === undefined) {
                        $('.form-group-nome').toggleClass('has-error');
                        $('#nome').focus();
                        $('.msg-validacao').val("Campo avaliação não pode ser vazio!");
                    }

                    TratarErro(result);
                },
                complete: function () {
                    $('.ladda-button-demo').ladda('stop');
                }
            });
        }
    });

    $('.btn-inativar').on("click", function (e) {

        var objMentor = {
            Id: $('#id').val()
        };

        console.log(objMentor);
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
            data: JSON.stringify(objMentor),
            success: function (result) {

                AtualizarPagina(result);

                toastr.success('Avaliacao inativado com sucesso!', 'Sucesso');
            },
            error: function (result) {
                TratarErro(result);
            },
            complete: function () {
                $('.ladda-button-demo').ladda('stop');
            }
        });

    });

    $('.btn-remover-publicacao').on("click", function (e) {

        var objAvaliacao = {
            Id: $('#id').val()
        };

        console.log(objAvaliacao);
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
            data: JSON.stringify(objAvaliacao),
            success: function (result) {
                AtualizarPagina(result);
                toastr.success('Status removido com sucesso!', 'Sucesso');
            },
            error: function (result) {
                $('.ladda-button-demo').ladda('stop');
                TratarErro(result);
            },
            complete: function () {
                $('.ladda-button-demo').ladda('stop');
            }
        });

    });

    function CriarFormData(alterar = false) {

        var form = new FormData();

        if (alterar !== undefined && alterar === true)
            form.append("Id", $('#id').val());

        form.append("MentorId", $('.select2_mentor').select().val());
        form.append("Texto", $('#texto').val());
        form.append("Positivo", $('#chkPositivo').is(":checked"));
        form.append("Expiracao", $('.mask-expiracao').val());
        form.append("Resposta", $('#resposta').val());

        for (var i = 0; i < listaRedesSociais.length; i++) {
            console.log(listaRedesSociais[i]);
            form.append("RedesSociais[" + i + "].Url", listaRedesSociais[i].Url);
            form.append("RedesSociais[" + i + "].Tipo", listaRedesSociais[i].Tipo);
            form.append("RedesSociais[" + i + "].Usuario", listaRedesSociais[i].Usuario);

        }

        for (var c = 0; c < $("#comprovantes")[0].files.length; c++) {
            form.append("Comprovantes", $("#comprovantes")[0].files[c]);
        }

        return form;
    }

});
