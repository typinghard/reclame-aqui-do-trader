$(document).ready(function () {
    CarregaComponentes();

    ValueObjects();

    MascaraTelefone();


    function ValueObjects() {
        AdicionarTelefones();
        AdicionarEmail();
        AdicionarRedeSocial();
    }

    function CarregaComponentes() {
        listaTelefones = [];
        listaEmails = [];
        listaRedesSociais = [];

        var listaTelefonesAux;
        var listaEmailsAux;
        var listaRedesSociaisAux;

        $('.ladda-button-demo').ladda();

        carregaSelect2CadastroMentor();
    }

    function carregaSelect2CadastroMentor() {
        $(".select2_redesocial").select2({
            dropdownParent: $('.modal-body'),
            theme: 'bootstrap4',
            placeholder: "Selecione um Tipo",
            allowClear: true
        });

        $(".select2_areaAtuacao").select2({
            dropdownParent: $('.modal-body'),
            theme: 'bootstrap4',
            placeholder: "Escreva uma area de atuação",
            tags: true,
            createTag: function (params) {
                return {
                    id: params.term,
                    text: params.term,
                    newOption: true
                };
            },
            templateResult: function (data) {
                var $result = $("<span></span>");

                $result.text(data.text);

                if (data.newOption) {
                    $result.append(" <em>(novo)</em>");
                }

                return $result;
            }
        });

        if (parseInt($('.select2_redesocial').val()) === 1) {
            $('#campoUrl').val('www.instagram.com');
        }

        $('.select2_redesocial').on('select2:unselect', function (e) {
            var data = e.params.data;
            $('#campoUrl').val("");
            console.log(data);

        });

        $('.select2_redesocial').on('select2:select', function (e) {
            var data = e.params.data;
            console.log(data);
            switch (data.text.trim().toLocaleLowerCase()) {

                case 'instagram':
                    $('#campoUrl').val('www.instagram.com');
                    break;
                case 'facebook':
                    $('#campoUrl').val('www.facebook.com');
                    break;
                default:
                    $('#campoUrl').val("");
                    break;
            }

        });

        $('.select2_redesocial').on('select2:change', function (e) {
            var data = e.params.data;
            console.log(data);
            switch (data.text.trim().toLocaleLowerCase()) {

                case 'instagram':
                    $('#campoUrl').val('www.instagram.com');
                    break;
                case 'facebook':
                    $('#campoUrl').val('www.facebook.com');
                    break;
                default:
                    $('#campoUrl').val("");
                    break;
            }

        });
    }

    function MascaraTelefone() {
        var SPMaskBehavior = function (val) {
            return val.replace(/\D/g, '').length === 11 ? '(00) 00000-0000' : '(00) 0000-00009';
        },
            spOptions = {
                onKeyPress: function (val, e, field, options) {
                    field.mask(SPMaskBehavior.apply({}, arguments), options);
                }
            };

        $('#campoTelefone').mask(SPMaskBehavior, spOptions);
    }

    function AdicionarTelefones() {

        $('.btn-salvar-telefone').on("click", function (e) {

            var numero = $('#campoTelefone').val();

            listaTelefones.push({ Numero: numero });

            var colunaAcao = ' <a data-toggle="tooltip" class="col-sm btn btn-remover-telefone acao-telefone" data-numero="' + numero + '" title="Remover">'
                + '<i class="fa fa-times text-danger" data-toggle="tooltip" title="Remover" style="width:22px;"></i>'
                + '</a>';

            var html = '<div class="col-sm row pb-1 border-bottom border-top m-0">'
                + '<div class="col-sm pt-2 numero-telefone">'
                + '<span>'
                + numero
                + '</span>'
                + '</div>'
                + colunaAcao
                + '</div>';

            //console.log(html);

            $('#divTelefones').append(html);

            $('#campoTelefone').val("");

            RemoverTelefone();
        });
    }

    function RemoverTelefone() {
        $('.btn-remover-telefone').unbind('click');
        $('.btn-remover-telefone').on("click", function (e) {

            var index = listaTelefones.findIndex(n => n.Numero === $(this).data('numero'));

            listaTelefones.splice(index, 1);
            $(this).parent().remove();

        });
    }

    function RemoverEmail() {
        $('.btn-remover-email').unbind('click');
        $('.btn-remover-email').on("click", function (e) {

            console.log(listaEmails);

            var index = listaEmails.findIndex(n => n.Endereco === $(this).data('endereco'));

            listaEmails.splice(index, 1);
            $(this).parent().remove();
        });
    }

    function RemoverRedeSocial() {
        $('.btn-remover-redeSocial').unbind('click');
        $('.btn-remover-redeSocial').on("click", function (e) {

            var index = listaRedesSociais.findIndex(n => (n.Url + '/' + n.Usuario) === $(this).data('redeSocial'));

            listaRedesSociais.splice(index, 1);
            $(this).parent().remove();
        });
    }

    function AdicionarEmail() {
        $('.btn-salvar-email').on("click", function (e) {

            var email = $('#campoEmail').val();

            listaEmails.push({ Endereco: email });
            console.log(listaEmails);

            var colunaAcao = ' <a data-toggle="tooltip" class="col-sm btn btn-remover-email acao-telefone" data-endereco="' + email + '" title="Remover">'
                + '<i class="fa fa-times text-danger" data-toggle="tooltip" title="Remover" style="width:22px;"></i>'
                + '</a>';

            var html = '<div class="col-sm row pb-1 border-bottom border-top m-0">'
                + '<div class="col-sm pt-2 numero-telefone">'
                + '<span data-toggle="tooltip" title="' + email + '">'
                + email
                + '</span>'
                + '</div>'
                + colunaAcao
                + '</div>';

            console.log(html);

            $('#divEmails').append(html);

            $('#campoEmail').val("");

            RemoverEmail();
        });
    }

    function AdicionarRedeSocial(nomeTabela, identificadorCampo) {

        $('.btn-salvar-redeSocial').on("click", function (e) {

            if ($("#campoRedeSocial").val().length === 0
                || $("#campoUrl").val().length === 0
                || $("#campoUsuario").val().length === 0)
                return;


            if ($('.form-group-nome-usuario').hasClass('has-error')) {

                $('.form-group-nome-usuario').toggleClass('has-error');
                $('.msg-validacao-rede-social').toggleClass('invisible');

            }

            var tipoRedeSocial = parseInt($('#campoRedeSocial').val());
            var url = $('#campoUrl').val();
            var usuario = $('#campoUsuario').val();

            var conteudolista = url + '/' + usuario;
            listaRedesSociais.push({
                Url: url,
                Usuario: usuario,
                Tipo: tipoRedeSocial
            });

            var usuarioFormatado = ExibeNomeUsuarioFormatado(tipoRedeSocial, usuario);

            var colunaAcao = ' <a data-toggle="tooltip" class="col-sm btn btn-remover-redeSocial acao-telefone" data-url="' + conteudolista + '"  title="Remover">'
                + '<i class="fa fa-times text-danger" data-toggle="tooltip" title="Remover" style="width:22px;"></i>'
                + '</a>';

            var html = '<div class="col-sm row pb-1 border-bottom border-top m-0">'
                + '<div class="col-sm pt-2 numero-telefone" data-url="' + url + '" data-tipo="' + tipoRedeSocial + '" data-usuario="' + usuario + '">'
                + '<span data-toggle="tooltip" title="' + url + '">'
                + usuarioFormatado
                + '</span>'
                + '</div>'
                + colunaAcao
                + '</div>';

            //console.log(html);

            $('#divRedesSociais').append(html);

            $('.select2_redesocial').val(null).trigger('change');
            $('#campoUrl').val("");
            $('#campoUsuario').val("");

            RemoverRedeSocial();
        });
    }

    function ExibeNomeUsuarioFormatado(tipo, usuario) {

        var retorno = '';
        switch (tipo) {
            case 0:
                retorno = '<i class="fa fa-facebook-square"></i>';
                break;
            case 1:
                retorno = '<i class="fa fa-instagram"></i>';
                break;
            default:
                retorno = '<i class="fa fa-question"></i>';
                break;
        }

        retorno += ' ' + usuario;

        return retorno;
    }

    function VerificaNome() {
        if ($('#nome').val().length === 0) {
            $('.form-group-nome').toggleClass('has-error');
            $('.msg-validacao').toggleClass('invisible');
            $('#nome').focus();
            return false;
        }
        else {

            if ($('.form-group-nome').hasClass('has-error')) {
                $('.form-group-nome').toggleClass('has-error');
                $('.msg-validacao').toggleClass('invisible');
            }


        }

        return true;
    }

    function ValidaDadosObrigatorios() {
        if (VerificaNome() && VerificaRedeSocial())
            return true;
        else return false;
    }

    function VerificaRedeSocial() {
        if (listaRedesSociais.length === 0) {
            //$('.form-group-rede-social').toggleClass('has-error');
            $('.form-group-nome-usuario').toggleClass('has-error');
            //$('.form-group-link').toggleClass('has-error');
            $('.msg-validacao-rede-social').toggleClass('invisible');
            $('#campoUsuario').focus();
            return false;
        }
        else {

            if ($('.form-group-nome-usuario').hasClass('has-error')) {

                //$('.form-group-rede-social').toggleClass('has-error');
                $('.form-group-nome-usuario').toggleClass('has-error');
                //$('.form-group-link').toggleClass('has-error');
                $('.msg-validacao-rede-social').toggleClass('invisible');

            }


        }

        return true;
    }

    $('.btn-salvar').on("click", function (e) {

        if (ValidaDadosObrigatorios()) {

            var objMentor = {
                Nome: $('#nome').val(),
                Site: $('#site').val(),
                AreasDeAtuacao: $('.select2_areaAtuacao').select().val(),
                Telefones: listaTelefones,
                Emails: listaEmails,
                RedesSociais: listaRedesSociais
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

                    if ($('.painel-principal').length > 0) {
                        $('#modalGenerico').modal('hide');
                        window.location.href = result.url_painel_principal;
                    }

                    //if ($('.painel-mentor').length > 0)
                    //    $('.painel-mentor').load(result.url, function () {
                    //        $('#modalGenerico').modal('hide');
                    //    });

                    if ($('#lista_mentores').length > 0)
                        window.location.href = result.url_lista_mentores;

                    toastr.success('Mentor cadastrado com sucesso!', 'Sucesso');
                },
                error: function (result) {

                    if (result.responseJSON !== undefined) {
                        $('.form-group-nome').toggleClass('has-error');
                        $('#nome').focus();
                        $('.msg-validacao').val("Campo nome não pode ser vazio!");
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

        if (ValidaDadosObrigatorios()) {

            var objMentor = {
                Id: $('#id').val(),
                Nome: $('#nome').val(),
                Site: $('#site').val(),
                AreasDeAtuacao: $('.select2_areaAtuacao').select().val(),
                Telefones: listaTelefones,
                Emails: listaEmails,
                RedesSociais: listaRedesSociais
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

                    console.log(result);

                    if ($('.painel-principal').length > 0) {
                        $('#modalGenerico').modal('hide');
                        window.location.href = result.url_painel_principal;
                    }

                    //if ($('.painel-mentor').length > 0)
                    //    $('.painel-mentor').load(result.url, function () {
                    //        $('#modalGenerico').modal('hide');
                    //    });

                    if ($('#conteudo-detalhes').length > 0)
                        window.location.href = result.url_detalhes;

                    if ($('#lista_mentores').length > 0)
                        window.location.href = result.url_lista_mentores;

                    toastr.success('Mentor atualizado com sucesso!', 'Sucesso');
                },
                error: function (result) {

                    if (result.responseJSON !== undefined) {
                        $('.form-group-nome').toggleClass('has-error');
                        $('#nome').focus();
                        $('.msg-validacao').val("Campo nome não pode ser vazio!");
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
            Id: $('#Id').val()
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

                console.log(result);

                if ($('.painel-principal').length > 0)
                    $('.painel-principal').load(result.url_painel_principal, function () {
                        $('#modalGenerico').modal('hide');
                        window.location.href = result.url_painel_principal;
                    });

                if ($('#lista_mentores_inativos').length > 0)
                    window.location.href = result.url;

                if ($('#lista_mentores').length > 0)
                    window.location.href = result.url_mentores;

                toastr.success('Mentor inativado com sucesso!', 'Sucesso');
            },
            error: function (result) {
                TratarErro(result);
            },
            complete: function () {
                $('.ladda-button-demo').ladda('stop');
            }
        });

    });

});
