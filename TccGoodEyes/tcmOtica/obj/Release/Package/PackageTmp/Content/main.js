$(document).ready(function () {

    //Mascara atraves do jQuery Plugin Mask
    //http://igorescobar.github.io/jQuery-Mask-Plugin/docs.html
    $('#txtCEP').mask('00000-000');
    $('#txtCel').mask('(00) 00000-0000');
    $('#txtTel').mask('(00) 0000-0000');
    $('#txtCPF').mask('000.000.000-00');
    $('#numcartao').mask('0000.0000.0000.0000');
    $('#txtDataReceita').mask('00/00/0000');
    $('#txtDataValidade').mask('00/00/0000');
    $('#txtData').mask('00/00/0000');
    $('#codseg').mask('000');
    $('#txtNum').mask('00000');
    atualizaSelect("#uf", "txtUF");


    //VERIFICAR SE O CAMPO ESTA COMPLETO SE NÃO FICA VERMELHO

    //cpf
    $('#txtCPF').blur(function () {
        ChecarInput($(this), 14);
    });

    //cel
    $('#txtCel').blur(function () {
        ChecarInput($(this), 15);
    });

    //tel
    $('#txtTel').blur(function () {
        ChecarInput($(this), 14);
    });

    //cep
    $('#txtCEP').blur(function () {
        ChecarInput($(this), 9);
    });

    //numero do cartao
    $('#numcartao').blur(function () {
        ChecarInput($(this), 19);
    });

    //codigo de seguranca
    $('#codseg').blur(function () {
        ChecarInput($(this), 3);
    });

    //codigo de seguranca
    $('#txtData').blur(function () {
        ChecarInput($(this), 10);
    });

    //codigo de seguranca
    $('#txtDataValidade').blur(function () {
        ChecarInput($(this), 10);
    });

    //codigo de seguranca
    $('#txtDataReceita').blur(function () {
        ChecarInput($(this), 10);
    });

    //CONFIRMAÇÃO DA SENHA
    var $senha = $("#txtSenha");
    var $confsenha = $("#txtConfSenha");

    $("#txtConfSenha").blur(function () {
        if ($senha.val() !== $confsenha.val()) {

            $senha.css("border-bottom", "2px solid salmon");
            $confsenha.css("border-bottom", "2px solid salmon");

            $confsenha.css("margin-bottom", "8px");
            $("#confirmesenha").text("As senha não são iguais, por favor redigite");
        }
        else if ($senha.val().length < 8) {
            $senha.css("border-bottom", "2px solid salmon");
            $confsenha.css("border-bottom", "2px solid salmon");

            $("#confirmesenha").text("A senha deve conter 8 caracteres.");
        }
        else if ($senha.val() === $confsenha.val()) {
            $senha.css("border-bottom", "1px solid #d9d9d9");
            $confsenha.css("border-bottom", "1px solid #d9d9d9");

            $("#confirmesenha").text("");
        }
    });


    //aviso ao digitar senha
    $senha.focus(function () {
        $("#senhaCondicoes").show();
    });

    $senha.blur(function () {
        $("#senhaCondicoes").hide();
    });





    //SE SENHAS NÃO FOREM IGUAIS NOS DOIS CAMPOS
    //OU CAMPOS ESPECIAIS NÃO ESTIEREM COMPLETOS
    //IRA CANCELAR O ENVIO DO FORMULARIO
    $(".formCad").each(function () { 
        $(this).submit(function (e) {
            if ($senha.val() !== $confsenha.val()) {
                $confsenha.focus();
                alert("As senha não são iguais, por favor redigite.");

                e.preventDefault();
                return false;
            }

            if (ChecarInput($('#txtData'), 10) === true) {
                $('#txtData').focus();
                $('#txtData').css("background-color", "#D3EDF0");
                alert("Digite uma data válida");
                e.preventDefault();
                return false;
            }
            if (ChecarInput($('#txtCPF'), 14) === true) {
                $('#txtCPF').focus();
                $('#txtCPF').css("background-color", "#D3EDF0");
                alert("Digite um número válido");
                e.preventDefault();
                return false;
            }
            if (ChecarInput($('#txtCel'), 15) === true) {
                $('#txtCel').focus();
                $('#txtCel').css("background-color", "#D3EDF0");
                alert("Digite um número válido");
                e.preventDefault();
                return false;
            }
            if (ChecarInput($('#txtTel'), 14) === true) {
                $('#txtTel').focus();
                $('#txtTel').css("background-color", "#D3EDF0");
                alert("Digite um número válido");
                e.preventDefault();
                return false;
            }
            if (ChecarInput($('#txtCEP'), 9) === true) {
                $('#txtCEP').focus();
                $('#txtCEP').css("background-color", "#D3EDF0");
                alert("Digite um número válido");
                e.preventDefault();
                return false;
            }
        });
    });


    //-----------------

    // limpa conteudo da input de 
    // pesquisa nas paginas de consulta
    $(".txtPesquisa").each(function () {
        $(this).focus(function () {
            $(this).val(null);
        });
    });

     //-----------------


    //PRA PAGINA DE CONSULTADO PRODUTO
    $(".formConPro h4").click(function () {
        $(this).next().slideToggle();
    });
    //ADICIONA SETA PRA BAIXO EM CADA H4
    $(".formConPro h4").append('<svg xmlns="http://www.w3.org/2000/svg" width="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-chevron-down"><polyline points="6 9 12 15 18 9"></polyline></svg>');



    //-----------------

    //volta para ultima pagina
    $('#volta').click(function () {
        parent.history.back();
        return false;
    });



    //retorna messahem de erro em caixa de alerta
    $(document).ready(function () {
        if ($("#erroNoti").text() != "") {
            alert($("#erroNoti").text());
        }
    });


    //PESQUISA DO FORM CONSULTA DO SISTEMA
    $('.pesquisa select').change(function () {
        $(this).css('backgroundColor', 'var(--azul-primario)');
        $(this).css('color', '#fff');
        $("form").submit();
    });

    $('.pesquisa input').change(function () {
        $(this).css('backgroundColor', 'var(--azul-primario)');
        $(this).css('color', '#fff');
        $("form").submit();
    });

    $('.pesquisa input').click(function () {
        $(this).css('backgroundColor', '#fff');
        $(this).css('color', '#000');
    });


});




// Funcoes utillizadas nos cadastro
// para checar se caixas de textos fdram devidamente preenchidas

//VERIFICAR SE O CAMPO ESTA COMPLETO SE NÃO FICA COM LINHA VERMELHO
function ChecarInput(input, num) {
  
    if (input.val().length !== num) {
        input.css("border-bottom", "2px solid salmon");
        
        return true;
    }
    else {
        input.css("border-bottom", "1px solid #d9d9d9");
        input.css("background-color", "#fff");
        return false;
    }
}


// FUNÇÃO PARA CRIAR UM MENU RESPONSIVO
/* Toggle between adding and removing the "responsive" class to topnav 
 * when the user clicks on the icon */
function myFunction() {
    var x = $('.links');
    if (x.hasClass("responsive") === false) {
        x.addClass("responsive");
        console.log("asdf");
    } else {
        x.removeClass("responsive");
        console.log("ss");
    }
} 


//PEGA INFORMACOES DO INPUT HIDDEN QUE RECEBE A VIEWBAG E SELECIONA ESSE VALOR NO SELECT
function atualizaSelect(escondido, seleciona) {
    if ($(escondido).val() != "") {
        $("select[name='" + seleciona + "'] option").each(function () {
            if ($(this).val().toLowerCase() == $(escondido).val().toLowerCase()) {
                $(this).prop('selected', 'selected');
            }
        });
    }
}

//PEGA INFORMACOES DO INPUT HIDDEN QUE RECEBE A VIEWBAG CHECKA ESSE VALOR
function atualizaInput(escondido, input) {
    if ($(escondido).val() != "") {
        $("input[name='" + input + "']").each(function () {
            if ($(this).val().toLowerCase() == $(escondido).val().toLowerCase()) {
                $(this).prop('checked', 'checked');
            }
        });
    }
}

//PEGA O VALOR CHECKADO NO CHECKBOX E MANDA PRO INPUT HIDDEN PRA PEGAR VALOR NO CONTROLLER
function pegaValor(escondido, checkbox) {
    $("input[name='" + checkbox + "']").change(function () {
        $(escondido).val($("input[name='" + checkbox + "']:checked").val());
    });
}



$(".excluir").click(function (e) {
        var r = confirm("Tem certeza que deseja excluir esse item permanentemente?" +
        "Essa ação não poderá ser revertida.");
    if (r == false) {
        e.preventDefault();
        e.stopPropagation();
    }
}); 

$(".edicao").click(function (e) {
    var r = confirm("Tem certeza que deseja alterar este item?");
    if (r == false) {
        e.preventDefault();
        e.stopPropagation();
    }
}); 



$("#exclusao").click(function (e) {
    var r = confirm("Tem certeza que deseja excluir sua conta permanentemente? Essa ação não poderá ser revertida.");
    if (r == false) {
        //alert("Sua conta será excluída");
        e.preventDefault();
        e.stopPropagation();
    }
});

$(".fechar").click(function (e) {
    var r = confirm("Tem certeza que deseja fechar esta notificação?");
    if (r == false) {
        e.preventDefault();
        e.stopPropagation();
    }
});
















