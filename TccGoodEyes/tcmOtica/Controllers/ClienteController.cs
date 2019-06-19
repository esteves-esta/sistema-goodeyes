using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using tccGoodEyes.Repositorio.Acoes;
using tccGoodEyes.Models;

namespace tccGoodEyes.Controllers
{
    [HandleError]
    public class ClienteController : Controller
    {
        //--------------
        //DECLARAR CLASSES
        //ACOES
        Cliente acaoCliente = new Cliente();
        Login acaoLogin = new Login();
        ProdutoNotificacao acaoNotificacao = new ProdutoNotificacao();

        //MODELS
        ClienteModel cliente = new ClienteModel();
        LoginModel login = new LoginModel();
        ProdutoNotificacaoModel notificacao = new ProdutoNotificacaoModel();

        //--------------------------------------------
        //Metodo que retorna com as informações do usuario logado
        public ClienteModel UsuarioLogado()
        {
            string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
            HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //Get the cookie by it's name
            if (authCookie != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
                string email = ticket.Name;

                ClienteModel cliente = acaoCliente.pesquisarClienteporEmail(email);
                return cliente;
            }
            else
            {
                cliente.cd_cliente = "0";
                return cliente;
            }

        }

        //---------------------------------------------------------
        //verifica se há usuário logado
        public bool verificaLogado()
        {
            if (UsuarioLogado().cd_cliente != "0")
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        // GET: Cliente
        public ActionResult Login(int erro)
        {
            ViewBag.logado = verificaLogado();
            if (erro == 1)
            {
                ViewBag.MsgError = "Usuário ou Senha Incorretos";
            }
            else if(erro == 2){
                ViewBag.MsgError = "Sua senha foi redifinida;";
            }
            else if(erro == 3)
            {
                ViewBag.MsgError = "Seu cadastro foi realizado com sucesso. Por favor realize seu login para entrar em sua conta.";
            }
            else if (erro == 4)
            {
                ViewBag.MsgError = "Sua conta foi excluida com sucesso.";
            }
            else if (erro == 5)
            {
                ViewBag.MsgError = "Ocorreu um erro e sua senha não foi redefinida.";
            }
            else if (erro == 6)
            {
                ViewBag.MsgError = "Este email já está cadastrado no sistema.";
            }
            return View(ViewBag.logado);
        }

        [HttpPost]
        public ActionResult Login(FormCollection frm)
        {
            ViewBag.logado = verificaLogado();

            login.email = frm["txtEmail"];
            login.senha = frm["txtSenha"];
            if (login.senha.Length > 8)
            {
                return RedirectToAction("Login", "Cliente", new { erro = 1 });
            }

            if (acaoLogin.autenticaUsu(login) == false)
            {

                return RedirectToAction("Login", "Cliente", new { erro = 1 });
            }
            else
            {
                login.cd_login = acaoLogin.getCodigo(login);
                if (acaoLogin.verificaNivel(login) == 1)
                {
                    return RedirectToAction("MinhaConta", "Cliente", new { aviso = 0 });
                }
                else if (acaoLogin.verificaNivel(login) == 2)
                {

                    return RedirectToAction("Index", "Sistema");
                }
                else if (acaoLogin.verificaNivel(login) == 3)
                {

                    return RedirectToAction("Index", "Sistema");
                }
            }

            return View(ViewBag.logado);
        }

        //---------------------------------------------------------

        [HttpGet]
        public ActionResult RedefinirSenha()
        {
            ViewBag.logado = verificaLogado();
            return View(ViewBag.logado);
        }

        [HttpPost]
        public ActionResult RedefinirSenha(FormCollection frm)
        {

            //PEGA EMAIL DO CLIENTE PARA MANDA EMAIL
            string email = frm["email"];

            ViewBag.logado = verificaLogado();
            if (acaoCliente.verificaEmail(email) == true)
            {
                ClienteModel cliente = acaoCliente.pesquisarClienteporEmail(email);
                //CÓDIGO PARA MANDAR EMAIL PARA CLIENTE
                try
                {
                    // Initialize WebMail helper
                    WebMail.SmtpServer = "smtp-mail.outlook.com";
                    WebMail.SmtpPort = 587;
                    WebMail.UserName = "goodresults@outlook.com.br";
                    WebMail.Password = "Projetoinfo";
                    WebMail.From = "goodresults@outlook.com.br";
                    WebMail.EnableSsl = true;
                    // Send email
                    WebMail.Send(to: email,
                        subject: "Redefinição de Senha da Ótica GoodEyes",
                        body: "<h2>Prezado(a) " + cliente.nome + " ,</h2>" +
                        "<p>Para redefinir a senha, clique neste link.</p>" +
                        "<p><a href='http://localhost:55070/Cliente/Redefinir' targert='blank'>http://localhost:55070/Cliente/Redefinir</a></p>" +
                        "<p>Observação:</p><p> por motivos de segurança, o link expirará 72 horas após o momento do envio.</p>" +
                        "<p>Se não for possível acessar o link, copie e cole o URL inteiro no navegador.</p>" +
                        "<p>A Equipe da Ótica GoodEyes.</p>",
                        isBodyHtml: true, priority: "High"
                        );

                    ViewBag.ErroEmail = "Sucesso";
                    ViewBag.emailCLi = frm["email"];
                    return View(ViewBag.logado);
                }
                catch (Exception e)
                {
                    ViewBag.ErroEmail = e.Message;
                    return View(ViewBag.logado);
                }
            }
            else
            {
                ViewBag.ErroEmail = "Esse email não está cadastrado no sistema";
                return View(ViewBag.logado);
            }
        }

        //---------------------------------------------------------
        [HttpGet]
        public ActionResult Redefinir()
        {
            ViewBag.logado = verificaLogado();
            return View(ViewBag.logado);
        }

        [HttpPost]
        public ActionResult Redefinir(FormCollection frm)
        {
            try
            {
                //atualizar banco de dados
                login.email = frm["email"].ToLower();
                login.senha = frm["txtSenha"];
                login.nivel_acesso = "1";
                acaoLogin.atualizarLogin(login);

                ViewBag.logado = verificaLogado();

                return RedirectToAction("Login", "Cliente", new { erro = 2 });
            }
            catch
            {
                return RedirectToAction("Login", "Cliente", new { erro = 5 });
            }
        }

        //---------------------------------------------------------
        public ActionResult Cadastro(string modo)
        {
            ViewBag.CadCliTitulo = modo;
            ViewBag.logado = verificaLogado();

            if (modo != "cad")
            {
                ClienteModel cliente = UsuarioLogado();

                ViewBag.nomeCli = cliente.nome;
                ViewBag.sobrenomeCli = cliente.sobrenome;
                ViewBag.dtNascimentoCli = cliente.dt_nascimento;
                ViewBag.cpfCli = cliente.no_cpf;
                ViewBag.telCli = cliente.no_tel;
                ViewBag.celCli = cliente.no_cel;
                ViewBag.emailCli = cliente.email;
                ViewBag.ruaCli = cliente.nm_rua;
                ViewBag.numCli = cliente.no_rua;
                ViewBag.cepCli = cliente.no_cep;
                ViewBag.complementoCli = cliente.complemento;
                ViewBag.bairroCli = cliente.bairro;
                ViewBag.cidadeCli = cliente.cidade;
                ViewBag.estadoCli = cliente.estado;
                ViewBag.ufCli = cliente.sg_uf;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Cadastro(string modo,FormCollection frm)
        {
            ViewBag.logado = verificaLogado();
            ViewBag.CadCliTitulo = modo;

            //PEGAR INFORMACOES DO CLIENTE
            cliente.nome = frm["txtNome"];
            cliente.sobrenome = frm["txtSobrenome"];
            cliente.dt_nascimento = frm["txtData"];
            cliente.no_cpf = frm["txtCPF"];
            cliente.no_tel = frm["txtTel"];
            cliente.no_cel = frm["txtCel"];
            cliente.email = frm["txtEmail"];
            cliente.nm_rua = frm["txtEnd"];
            cliente.no_rua = frm["txtNum"];
            cliente.no_cep = frm["txtCEP"];
            cliente.complemento = frm["txtComp"];
            cliente.bairro = frm["txtBairro"];
            cliente.cidade = frm["txtCidade"];
            cliente.estado = frm["txtEstado"];
            cliente.sg_uf = frm["txtUF"];
            //INFORMAÇÕES DE LOGIN
            login.email = frm["txtEmail"];
            login.senha = frm["txtSenha"];
            login.nivel_acesso = "1";

            try
            {
                if (acaoLogin.verificaEmail(login) == true)
                { 
                    if (modo == "cad")
                    {
                        acaoLogin.inserirLogin(login);
                        cliente.cd_login = acaoLogin.getCodigo(login);
                        acaoCliente.inserirCliente(cliente);

                        return RedirectToAction("Login", "Cliente", new { erro = 3 });
                    }
                    else
                    {
                        //PEGAR CODIGO DO CLIENTE
                        ClienteModel cli = new ClienteModel();
                        cli = acaoCliente.pesquisarClienteporEmail(UsuarioLogado().email);
                        cliente.cd_cliente = cli.cd_cliente;
                        // cliente.email = cli.email;
                        acaoCliente.atualizarCliente(cliente);

                        //SÓ ATUALIZA LOGIN SE FOR ALTERAÇÃO DE DADOS PESSOAIS
                        if (modo != "AlterarEndereco")
                        {
                            // login.email = cli.email;
                            acaoLogin.atualizarLogin(login);
                        }

                        return RedirectToAction("MinhaConta", new { aviso = 1 });
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Cliente", new { erro = 6 });
                }
            }
          catch(Exception e){

                ViewBag.MsgError = "Houve um erro interno, por favor tente novamente. Nos desculpamos pelo inconveniente.";
               return View();
            }
        }

        //---------------------------------------------------------

        public ActionResult MinhaConta(int aviso)
        {
            if (aviso == 1)
            {
                ViewBag.MsgError = "Sua alteração foi realizada com sucesso";
            }else if (aviso == 2)
            {
                ViewBag.MsgError = "O alerta foi fechado.";
            }
            ClienteModel cliente = UsuarioLogado();
            ViewBag.logado = verificaLogado();

            List<string> nomeProd = new List<string>();
            ViewBag.favorito = acaoNotificacao.verificaFavoritos(cliente.cd_cliente);

            if (acaoNotificacao.verificaFavoritos(cliente.cd_cliente).Count > 0)
            {
                foreach (ProdutoNotificacaoModel i in acaoNotificacao.verificaFavoritos(cliente.cd_cliente)) {
                    Produto acaoProduto = new Produto();
                    ProdutoModel produto = acaoProduto.pesquisaProdutoPorCodigo(i.cd_produto);
                    nomeProd.Add(produto.nm_marca);
                }
            }
            ViewBag.nomeProdutos = nomeProd;
            return View(cliente);
        }

        //-------------------------------------------------------

        public ActionResult ExcluirConta()
        {
            cliente = UsuarioLogado();
            acaoCliente.deletarCliente(cliente);
            acaoLogin.deslogar();
            return RedirectToAction("Login", new {erro = 4 });
        }

        //---------------------------------------------------------
        public ActionResult MeusPedidos()
        {
            ViewBag.logado = verificaLogado();
            Pedido acaoPedido = new Pedido();
            PedidoReceita acaoPedReceita = new PedidoReceita();
            Pagamento acaoPagamento = new Pagamento();
            PedidoItem acaoPedItem = new PedidoItem();
            PedidoSuperModel pedidos = new PedidoSuperModel();

            ClienteModel cliente = UsuarioLogado();

            pedidos.clienteModel = cliente;
            pedidos.pedidoModelLista = acaoPedido.consultaPedido(cliente);
            pedidos.receitaModelLista = acaoPedReceita.consultaPedidoReceita(cliente.cd_cliente, "0");
            pedidos.pagamentoModelLista = acaoPagamento.consultaFormaPagamentoLista(cliente);
            pedidos.produtoModelLista = acaoPedItem.consultaPedidoItens(cliente.cd_cliente, "0");

            if (acaoPedido.verificaSeExistePedido(cliente.cd_cliente) == false)
            {
                ViewBag.MsgError = "Você ainda não realizou nenhum pedido.";
            }

            return View(pedidos);
        }

        //---------------------------------------------------------

        public ActionResult FavoritoDeletar(string cod)
        {
            try
            {
                acaoNotificacao.deletarFavoritos(cod);
                return RedirectToAction("MinhaConta", new { aviso = 2 });
            }
            catch
            {
                return RedirectToAction("MinhaConta", new { aviso = 0 });
            }
        }

        //---------------------------------------------------------

        public ActionResult Logout()
        {
            //LOGOUT
            acaoLogin.deslogar();

            //DEPOIS DE LOGOUT REDIRECIONA PARA A HOME
            return RedirectToAction("Index", "Home");
        }

    }
}