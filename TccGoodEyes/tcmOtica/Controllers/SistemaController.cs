using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using tccGoodEyes.Repositorio.Acoes;
using tccGoodEyes.Models;
using tccGoodEyes.Relatorio;
using tccGoodEyes.Repositorio;

namespace tccGoodEyes.Controllers
{
    public class SistemaController : Controller
    {
        //--------------
        //DECLARAR CLASSES
        //BLL
        Cliente acaoCliente = new Cliente();
        Repositorio.Acoes.Login acaoLogin = new Repositorio.Acoes.Login();
        Funcionario acaoFuncionario = new Funcionario();
        Produto acaoProduto = new Produto();
        Estoque acaoEstoque = new Estoque();

        CarrinhoDeCompra acaoCarrinho = new CarrinhoDeCompra();
        Pedido acaoPedido = new Pedido();
        FornecedorMarca acaoFornMarca = new FornecedorMarca();
        Fornecedor acaoFornecedor = new Fornecedor();
        ProdutoNotificacao acaoNotificacao = new ProdutoNotificacao();

        //DTO
        ClienteModel cliente = new ClienteModel();
        LoginModel login = new LoginModel();
        FuncionarioModel funcionario = new FuncionarioModel();
        ProdutoModel produto = new ProdutoModel();
        EstoqueModel estoque = new EstoqueModel();
        FornecedorModel fornecedor = new FornecedorModel();

        CarrinhoDeCompraModel carrinho = new CarrinhoDeCompraModel();
        PedidoModel pedido = new PedidoModel();
        FornecedorMarcaModel fornMarca = new FornecedorMarcaModel();
        ProdutoNotificacaoModel notificacao = new ProdutoNotificacaoModel();


        //RETORNA O NIVEL DO USUÁRIO
        //SE NÃO 
        public int VerificaGerente()
        {
            string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
            HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //Get the cookie by it's name
            if (authCookie != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
                string email = ticket.Name;

                login.email = email;
                login.cd_login = acaoLogin.getCodigo(login);
                //RETORNA O NIVEL DO USUARIO
                return acaoLogin.verificaNivel(login);
            }
            else
            {
                //SE FOR 0 NÃO ESTÁ LOGADO
                return 0;
            }
        }

        public void Notificacao(int aviso)
        {
            switch (aviso)
            {
                case 1:
                    ViewBag.MsgError = "A exclusão foi realizada com sucesso";
                    break;
                case 2:
                    ViewBag.MsgError = "A alteração foi realizada com sucesso.";
                    break;
                case 3:
                    ViewBag.MsgError = "Ocorreu um erro e a ação não foi realizado.";
                    break;
            }
        }

        //--------------------------------------------------------------------

        public ActionResult Index()
        {
            ViewBag.Gerente = VerificaGerente();

            string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
            HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //Get the cookie by it's name
            if (authCookie != null && VerificaGerente() != 1)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
                string email = ticket.Name;

                //RETORNAR NOME DO USUARIO
                ViewBag.Usuario = acaoFuncionario.pesquisarFuncionarioPorEmail(email);

                //TOTAL DE ITENS DE CADA TABELA
                ViewBag.VendaCount = acaoCarrinho.Count("tbPedido");
                ViewBag.ClienteCount = acaoCarrinho.Count("tbCliente");
                ViewBag.ProdutoCount = acaoCarrinho.Count("tbProduto");
                ViewBag.FuncionarioCount = acaoCarrinho.Count("tbFuncionario");

                //TABELAS NO INDEX
                ViewBag.foraEstoque = acaoProduto.consultaprodutosForaEstoque();

                ViewBag.maisVendidos = tabela(acaoProduto.consultaprodutosMaisVendidos()).ToString();
                ViewBag.menosVendidos = tabela(acaoProduto.consultaprodutosMenosVendidos()).ToString();

                return View();
            }
            else
            {
                return RedirectToAction("acessoNegado", "Erro");
            }
        }


        //METODO COM INFO PARA PEGAR DATATABLE E CONVERTER EM TABELA HTML
        public StringWriter tabela(DataTable td)
        {
            GridView dgv = new GridView(); //mesma coisa do datagridview do windowsfrom
            dgv.DataSource = td;
            dgv.DataBind();
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            dgv.RenderControl(htw);
            return sw;
        }

        //RETONAR PDF COM RELATORIO DE VENDAS
        public ActionResult Relatorio()
        {
            RelatorioVendas relatorio = new RelatorioVendas();

            byte[] abytes = relatorio.PrepareReport(acaoPedido.consultaVendas());
            return File(abytes, "application/pdf");
        }

        //-------------------------------------------------------------
        // CADASTRO DO FUNCIONARIO
        public ActionResult CadFuncionario(string cod, int aviso)
        {
            ViewBag.Gerente = VerificaGerente();
            ViewBag.cod = cod;
            //SE FOR 0 NÃO ESTÁ LOGADO
            //SE 1 É CLIENTE
            if (VerificaGerente() != 0 && VerificaGerente() != 1)
            {
                if (aviso == 1)
                {
                    ViewBag.MsgError = "O cadastro foi realizado com sucesso";
                }
                else if (aviso == 2)
                {
                    ViewBag.MsgError = "Ocorreu um erro e o cadastro não foi realizado.";
                }
                if (cod != "0")
                {
                    //TRAZER INFORMAÇOES PARA ALTERAÇÃO
                    ViewBag.cod = cod;
                    //CODIGO DO FUNCIONARIO
                    funcionario.cd_funcionario = cod;

                    //PESQUISAR FUNCIONARIO PELO CODIGO
                    FuncionarioModel dto = acaoFuncionario.pesquisarFunc(funcionario);

                    //RETORNAR INFORMACOES NA VIEWBAG
                    ViewBag.nome = dto.nome;
                    ViewBag.sobrenome = dto.sobrenome;
                    ViewBag.data = dto.dt_nascimento;
                    ViewBag.cpf = dto.no_cpf;
                    ViewBag.tel = dto.no_tel;
                    ViewBag.cel = dto.no_cel;
                    ViewBag.Email = dto.email;
                    ViewBag.EndFun = dto.nm_rua;
                    ViewBag.NumFun = dto.no_rua;
                    ViewBag.CepFun = dto.no_cep;
                    ViewBag.CompleFun = dto.complemento;
                    ViewBag.BairroUfFun = dto.bairro;
                    ViewBag.CidadeFun = dto.cidade;
                    ViewBag.EstadoFun = dto.estado;
                    ViewBag.uf = dto.sg_uf;
                    ViewBag.cargo = dto.cargo;
                    ViewBag.email = dto.email;
                }

                return View();
            }
            else
            {
                return RedirectToAction("acessoNegado", "Erro");

            }
        }

        [HttpPost]
        public ActionResult CadFuncionario(string cod, string modo, FormCollection frm)
        {
            ViewBag.Gerente = VerificaGerente();
            if (VerificaGerente() != 0 && VerificaGerente() != 1)
            {
                //LOGIN
                login.email = frm["txtEmail"];
                login.senha = frm["txtSenha"];

                if (frm["txtCargo"] == "Gerente")
                {
                    login.nivel_acesso = "3";
                }
                else
                {
                    login.nivel_acesso = "2";
                }

                //FUNCIONARIO
                funcionario.nome = frm["txtNome"];
                funcionario.sobrenome = frm["txtSobrenome"];
                funcionario.dt_nascimento = frm["txtData"];
                funcionario.no_cpf = frm["txtCPF"];
                funcionario.no_tel = frm["txtTel"];
                funcionario.no_cel = frm["txtCel"];
                funcionario.email = frm["txtEmail"];
                funcionario.nm_rua = frm["txtEnd"];
                funcionario.no_rua = frm["txtNum"];
                funcionario.no_cep = frm["txtCEP"];
                funcionario.complemento = frm["txtComp"];
                funcionario.bairro = frm["txtBairro"];
                funcionario.cidade = frm["txtCidade"];
                funcionario.estado = frm["txtEstado"];
                funcionario.sg_uf = frm["txtUF"];
                funcionario.cargo = frm["txtCargo"];

                //INSERIR OU ALTERAR DADOS
                if (modo == "cadastro")
                {
                    acaoLogin.inserirLogin(login);
                    acaoFuncionario.inserirFuncionario(funcionario);

                    return RedirectToAction("CadFuncionario", new { cod = 0, aviso = 1 });
                }
                else
                {
                    funcionario.cd_funcionario = cod;

                    FuncionarioModel func = acaoFuncionario.pesquisarFunc(funcionario);
                    funcionario.email = func.email;

                    acaoFuncionario.atualizarFuncionario(funcionario);
                    acaoLogin.atualizarLogin(login);

                    return RedirectToAction("ConsFuncionario", new { aviso = 2 });
                }
            }
            else
            {
                return RedirectToAction("acessoNegado", "Erro");
            }
        }

        public ActionResult ConsFuncionario(int aviso)
        {
            Notificacao(aviso);

            ViewBag.Gerente = VerificaGerente();
            if (VerificaGerente() != 0 && VerificaGerente() != 1)
            {
                //TOTAL DE FUNCIONARIOS
                ViewBag.QtdeFunc = acaoCarrinho.Count("tbFuncionario");
                //CARREGAR CAIXA DE PESQUISA COM INFORMACAO
                ViewBag.ListFuncionarios = acaoFuncionario.listaNomeFuncionario();
                ViewBag.ListFunCPF = acaoFuncionario.listaCPFFuncionario();
                //RETONAR LISTA COM TODOS FUNCIONARIOS
                return View(acaoFuncionario.consultaFuncionario());
            }
            else
            {
                return RedirectToAction("acessoNegado", "Erro");
            }
        }

        [HttpPost]
        public ActionResult ConsFuncionario(FormCollection frm)
        {
            ViewBag.Gerente = VerificaGerente();

            //TOTAL DE FUNCIONARIOS
            ViewBag.QtdeFunc = acaoCarrinho.Count("tbFuncionario");
            //CARREGAR CAIXA DE PESQUISA COM INFORMACAO
            ViewBag.ListFuncionarios = acaoFuncionario.listaNomeFuncionario();
            ViewBag.ListFunCPF = acaoFuncionario.listaCPFFuncionario();

            FuncionarioModel fun = new FuncionarioModel();
            //fun.cod = "0";
            if (frm["NomeFun"] != string.Empty)
            {
                string[] nome = frm["NomeFun"].Split(' ');
                fun.nome = nome[0];
                fun.sobrenome = nome[1];
            }
            else
            {
                fun.nome = "";
                fun.sobrenome = "";
            }
            fun.no_cpf = frm["CPFFun"];
            fun.cargo = frm["cargo"];

            return View(acaoFuncionario.pesquisarFuncionario(fun));
        }

        public ActionResult DetalheFuncionario(string cod)
        {
            ViewBag.Gerente = VerificaGerente();
            if (VerificaGerente() != 0 && VerificaGerente() != 1)
            {

                funcionario.cd_funcionario = cod;

                return View(acaoFuncionario.pesquisarFunc(funcionario));
            }
            else
            {
                return RedirectToAction("acessoNegado", "Erro");
            }
        }


        public ActionResult ExcluirFunc(string cod)
        {
            try
            {
                acaoFuncionario.deletarFuncionario(cod);

                return RedirectToAction("ConsFuncionario", new { aviso = 1 });
            }
            catch
            {
                return RedirectToAction("ConsFuncionario", new { aviso = 3 });
            }
        }

        //**
        //-------------------------------

        public ActionResult CadProduto(string cod, int aviso)
        {
            ViewBag.Gerente = VerificaGerente();
            ViewBag.cod = cod;
            if (VerificaGerente() != 0 && VerificaGerente() != 1)
            {
                if (aviso == 1)
                {
                    ViewBag.MsgError = "O cadastro foi realizado com sucesso";
                }
                else if (aviso == 2)
                {
                    ViewBag.MsgError = "Ocorreu um erro e o cadastro não foi realizado.";
                }

                //TRAZER DADOS PARA ATUALIZAR
                if (cod != "0")
                {
                    //TRAZER INFORMAÇOES PARA ALTERAÇÃO
                    ViewBag.cod = cod;
                    ProdutoModel dto = acaoProduto.pesquisaProdutoPorCodigo(cod);

                    ViewBag.mar = dto.nm_marca;
                    ViewBag.TipoPro = dto.tipo;
                    ViewBag.Preco = dto.vl_preco_unitario;
                    ViewBag.Garantia = dto.garantia;

                    ViewBag.Qtde = acaoProduto.getQuantidadeEstoque(cod);

                    string[] descri = dto.descricao.Split(' ');
                    if (dto.tipo != "Lente")
                    {
                        string[] aspecto = dto.aspecto.Split(' ');
                        ViewBag.MaterialOculos = descri[0];
                        ViewBag.Modelo = descri[1];
                        ViewBag.genF = descri[2];
                        ViewBag.FaixaEtaria = descri[3];
                        ViewBag.CorArm = descri[4];
                        ViewBag.TratamentoOculos = descri[5];
                        ViewBag.CorLente = descri[6];

                        ViewBag.Tamanho = aspecto[0];
                        ViewBag.Comprimento = aspecto[1];
                        ViewBag.Altura = aspecto[2];

                        ViewBag.Imagem = dto.caminho_imagem;
                    }
                    else
                    {
                        ViewBag.MaterialLente = descri[0];
                        ViewBag.foco = descri[1];
                        ViewBag.tratamento = dto.aspecto;

                        ViewBag.Imagem = "/imagemProdutos/lentes.jpg";
                    }

                }

                //LISTA COM MARCAS
                ViewBag.ListaDeMarcas = acaoFornMarca.consultaMarca();
                ViewBag.ListaDeMarcasLente = acaoFornMarca.consultaMarcaLente();

                return View();
            }
            else
            {
                return RedirectToAction("acessoNegado", "Erro");
            }
        }

        [HttpPost]
        public ActionResult CadProduto(string cod, string modo, FormCollection frm, HttpPostedFileBase file)
        {
            //LISTA COM MARCAS
            ViewBag.ListaDeMarcas = acaoFornMarca.consultaMarca();
            ViewBag.ListaDeMarcasLente = acaoFornMarca.consultaMarcaLente();

            ViewBag.Gerente = VerificaGerente();

            ViewBag.cod = cod;
            try
            {
                ProdutoModel dto = new ProdutoModel();

                dto.tipo = frm["txtTipoProd"];
                if (dto.tipo != "Lente")
                {
                    //ÓCULOS DE SOL OU GRAU
                    dto.nm_marca = frm["ListaDeMarcas"];
                    dto.descricao = frm["txtMaterialOculos"]
                        + " " + frm["txtModelo"]
                         + " " + frm["rdgen"]
                          + " " + frm["txtFaixa"]
                           + " " + frm["txtCorArm"]
                             + " " + frm["txtTratamentoOculos"]
                               + " " + frm["txtCorLente"];

                    dto.aspecto = frm["txtTamanho"]
                         + " " + frm["txtComprimento"]
                          + " " + frm["txtAltura"];

                    //CODIGO DE SALVAR IMAGEM
                    string extensao = Path.GetExtension(file.FileName).ToLower();
                    if ((extensao != ".jpg" && extensao != ".png"))
                    {
                        ViewBag.Message = "Tipo de arquivo inválido. Escolha apenas arquivos png e jpg";

                        return RedirectToAction("CadProduto", new { cod = 0, aviso = 2 });
                    }
                    else
                    {
                        if (file.ContentLength > 0)
                        {
                            string _FileName = Path.GetFileName(file.FileName);
                            string _path = Path.Combine(Server.MapPath("~/imagemProdutos"), dto.nm_marca + ' ' + dto.descricao + extensao);
                            file.SaveAs(_path);

                            dto.caminho_imagem = "/imagemProdutos/" + dto.nm_marca + ' ' + dto.descricao + extensao;

                        }
                    }
                }
                else
                {
                    //LENTE
                    dto.nm_marca = frm["ListaDeMarcasLente"];
                    dto.descricao = frm["txtMaterialLente"]
                                 + " " + frm["rdFoco"];

                    dto.aspecto = frm["trataEscolhida"].Replace(',', ' ');
                    //IMAGEM PADRÃO DAS LENTES
                    dto.caminho_imagem = "/imagemProdutos/lentes.jpg";
                }
                dto.vl_preco_unitario = frm["txtPreco"];
                dto.garantia = frm["txtGarantia"];
                //ESTOQUE
                estoque.qt_estoque = frm["txtQtde"];

                //INSERIR OU ALTERAR DADOS
                if (modo == "cadastro")
                {

                    acaoProduto.inserirProduto(dto);

                    estoque.cd_produto = acaoProduto.getUltimoCadastrado();
                    acaoEstoque.inserirEstoque(estoque);
                    return RedirectToAction("CadProduto", new { cod = 0, aviso = 1 });
                }
                else
                {
                    dto.cd_produto = cod;
                    acaoProduto.atualizarProduto(dto);

                    estoque.cd_produto = cod;
                    acaoEstoque.atualizarEstoque(estoque);

                    return RedirectToAction("ConsEstoque", new { aviso = 2 });
                }
            }
            catch
            {
                return RedirectToAction("CadProduto", new { cod = 0, aviso = 2 });
            }

        }

        public ActionResult ConsEstoque(int aviso)
        {
            Notificacao(aviso);

            if (VerificaGerente() != 0 && VerificaGerente() != 1)
            {
                ViewBag.Gerente = VerificaGerente();

                ViewBag.QtdePro = acaoCarrinho.Count("tbProduto");
                ViewBag.QtdeMarcas = acaoCarrinho.Count("tbFornecedorMarca");

                ViewBag.ListaDeMarcas = acaoFornMarca.consultaMarcaLista();
                ViewBag.ListaDeMarcasLentes = acaoFornMarca.consultaMarcaListaLente();

                return View(acaoProduto.consultaProduto());
            }
            else
            {
                return RedirectToAction("acessoNegado", "Erro");
            }
        }

        [HttpPost]
        public ActionResult ConsEstoque(FormCollection frm)
        {

            ViewBag.Gerente = VerificaGerente();

            ViewBag.QtdePro = acaoCarrinho.Count("tbProduto");
            ViewBag.QtdeMarcas = acaoCarrinho.Count("tbFornecedorMarca");

            ViewBag.ListaDeMarcas = acaoFornMarca.consultaMarcaLista();
            ViewBag.ListaDeMarcasLentes = acaoFornMarca.consultaMarcaListaLente();

            ProdutoModel dto = new ProdutoModel();

            if (frm["txtTipoProd"] != null)
            {
                dto.tipo = frm["txtTipoProd"];
            }
            else
            {
                dto.tipo = "";
            }
            string marca;

            if (frm["txtMarca"] != null)
            {
                marca = frm["txtMarca"];
            }
            else if (frm["txtMarcaLente"] != null)
            {
                marca = frm["txtMarcaLente"];
            }
            else
            {
                marca = "";
            }
            dto.nm_marca = marca;


            string material;
            if (frm["txtMaterialOculos"] != null)
            {
                material = frm["txtMaterialOculos"];
            }
            else if (frm["txtMaterialLente"] != null)
            {
                material = frm["txtMaterialLente"];
            }
            else
            {
                material = "";
            }
            dto.descricao = material;


            if (frm["txtPreco"] != null)
            {
                dto.vl_preco_unitario = frm["txtPreco"];
            }
            else
            {
                dto.vl_preco_unitario = "0-10000";
            }
            dto.aspecto = "";

            return View(acaoProduto.pesquisaCompletaProduto(dto));

        }

        public ActionResult DetalheEstoque(string cod)
        {
            if (VerificaGerente() != 0 && VerificaGerente() != 1)
            {
                ViewBag.Gerente = VerificaGerente();

                ViewBag.qtde = acaoProduto.getQuantidadeEstoque(cod);

                return View(acaoProduto.pesquisaProdutoPorCodigo(cod));
            }
            else
            {
                return RedirectToAction("acessoNegado", "Erro");
            }
        }


        public ActionResult ExcluirPro(string cod)
        {
            try
            {
                //excluir 
                acaoProduto.deletarProduto(cod);

                return RedirectToAction("ConsEstoque", new { aviso = 1 });
            }
            catch
            {
                return RedirectToAction("ConsEstoque", new { aviso = 3 });
            }
        }

        //-----------------------------------------------

        public ActionResult AlteraCliente(string cod)
        {
            ViewBag.Gerente = VerificaGerente();
            ClienteModel cli = new ClienteModel();
            cli.cd_cliente = cod;

            ClienteModel cliente = acaoCliente.pesquisarCliente(cli);
            ViewBag.cod = cod;
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

            return View();
        }

        [HttpPost]
        public ActionResult AlteraCliente(string cod, FormCollection frm)
        {
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
                //PEGAR CODIGO DO CLIENTE
                cliente.cd_cliente = cod;
                ClienteModel cli = acaoCliente.pesquisarCliente(cliente);
                cliente.email = cli.email;
                acaoCliente.atualizarCliente(cliente);

                login.email = cli.email;
                acaoLogin.atualizarLogin(login);

                return RedirectToAction("ConsCliente", new { aviso = 2 });

            }
            catch (Exception e)
            {
                return RedirectToAction("ConsCliente", new { aviso = 3 });
            }

        }

        public ActionResult ConsCliente(int aviso)
        {

            Notificacao(aviso);

            if (VerificaGerente() != 0 && VerificaGerente() != 1)
            {
                ViewBag.Gerente = VerificaGerente();

                ViewBag.QtdeCli = acaoCarrinho.Count("tbCliente");

                ViewBag.ListCliente = acaoCliente.listaNomeCliente();
                ViewBag.ListCPF = acaoCliente.listaCPFCliente();

                return View(acaoCliente.consultaCliente());
            }
            else
            {
                return RedirectToAction("acessoNegado", "Erro");
            }
        }

        [HttpPost]
        public ActionResult ConsCliente(FormCollection frm)
        {
            ViewBag.Gerente = VerificaGerente();

            ViewBag.QtdeCli = acaoCarrinho.Count("tbCliente");

            ViewBag.ListCliente = acaoCliente.listaNomeCliente();
            ViewBag.ListCPF = acaoCliente.listaCPFCliente();

            ClienteModel cli = new ClienteModel();
            cli.cd_cliente = "0";
            if (frm["NomeCli"] != string.Empty)
            {
                string[] nome = frm["NomeCli"].Split(' ');
                cli.nome = nome[0];
                cli.sobrenome = nome[1];
            }
            else
            {
                cli.nome = " ";
                cli.sobrenome = " ";
            }
            cli.no_cpf = frm["CPF"];

            return View(acaoCliente.pesquisarClienteLista(cli));
        }

        //-----------------------------------------------------------
        public ActionResult DetalheCliente(string cod)
        {
            if (VerificaGerente() != 0 && VerificaGerente() != 1)
            {
                ViewBag.Gerente = VerificaGerente();

                ClienteModel cli = new ClienteModel();
                cli.cd_cliente = cod;

                return View(acaoCliente.pesquisarCliente(cli));
            }
            else
            {
                return RedirectToAction("acessoNegado", "Erro");
            }
        }

        public ActionResult ExcluirClie(string cod)
        {
            try
            {
                //excluir 
                cliente.cd_cliente = cod;
                acaoCliente.deletarCliente(cliente);

                return RedirectToAction("ConsCliente", new { aviso = 1 });
            }
            catch
            {
                return RedirectToAction("ConsCliente", new { aviso = 3 });
            }
        }

        //-------------------------------------------------------------------

        //-------------------------------

        public ActionResult CadMarca(string cod, int aviso)
        {
            if (VerificaGerente() != 0 && VerificaGerente() != 1)
            {
                ViewBag.cod = cod;
                ViewBag.Gerente = VerificaGerente();

                ViewBag.ListaDeMarcas = acaoFornMarca.consultaMarcaLista();
                ViewBag.ListaDeMarcasLente = acaoFornMarca.consultaMarcaListaLente();


                if (aviso == 1)
                {
                    ViewBag.MsgError = "O cadastro foi realizado com sucesso";
                }
                else if (aviso == 2)
                {
                    ViewBag.MsgError = "Ocorreu um erro e o cadastro não foi realizado.";
                }

                //pesquisar codigo e retornar info para viewbagd

                ViewBag.cod = cod;
                if (cod != "0")
                {
                    FornecedorMarcaModel dto = acaoFornMarca.pesquisarMarca(cod);

                    ViewBag.Marca = dto.nm_marca;
                    ViewBag.codForne = dto.cd_fornecedor;
                }
                ViewBag.fornecedores = acaoFornecedor.listarFornecedores();
                return View();
            }
            else
            {
                return RedirectToAction("acessoNegado", "Erro");
            }
        }

        [HttpPost]
        public ActionResult CadMarca(string cod, string modo, FormCollection frm)
        {
            if (VerificaGerente() != 0 && VerificaGerente() != 1)
            {
                ViewBag.cod = cod;
                ViewBag.Gerente = VerificaGerente();

                fornMarca.nm_marca = frm["txtMarca"];
                fornMarca.cd_fornecedor = frm["txtforne"];
                ViewBag.fornecedores = acaoFornecedor.listarFornecedores();
                //inserir ou alterar dados
                if (modo == "cadastro")
                {
                    acaoFornMarca.inserirFornecedorMarcaBLL(fornMarca);

                    return RedirectToAction("CadMarca", new { cod = 0, aviso = 1 });
                }
                else
                {
                    acaoFornMarca.atualizarFornecedorMarca(fornMarca);

                    return RedirectToAction("ConsMarca", new { aviso = 2 });
                }
            }
            else
            {
                return RedirectToAction("acessoNegado", "Erro");
            }
        }

        public ActionResult ConsMarca(int aviso)
        {

            Notificacao(aviso);
            if (VerificaGerente() != 0 && VerificaGerente() != 1)
            {
                ViewBag.Gerente = VerificaGerente();
                ViewBag.Qtde = acaoCarrinho.Count("tbFornecedorMarca");

                //VIEWBAG COM MARCAS
                ViewBag.ListaDeMarcas = acaoFornMarca.consultaMarcaLista();
                ViewBag.ListaDeMarcasLentes = acaoFornMarca.consultaMarcaListaLente();

                return View(acaoFornMarca.exibirfornMarcas());

            }
            else
            {
                return RedirectToAction("acessoNegado", "Erro");
            }
        }

        [HttpPost]
        public ActionResult ConsMarca(FormCollection frm)
        {

            ViewBag.Gerente = VerificaGerente();
            ViewBag.Qtde = acaoCarrinho.Count("tbFornecedorMarca");

            //VIEWBAG COM MARCAS
            ViewBag.ListaDeMarcas = acaoFornMarca.consultaMarcaLista();
            ViewBag.ListaDeMarcasLentes = acaoFornMarca.consultaMarcaListaLente();

            string marca;

            if (frm["txtMarcaLente"] != null)
            {
                marca = frm["txtMarcaLente"];
            }
            else
            {
                marca = frm["txtMarca"];

            }

            return View(acaoFornMarca.pesqfornMarcas(marca));
        }


        public ActionResult ExcluirMarca(string cod)
        {
            try
            {
                //excluir 
                acaoFornMarca.deletarFornecedorMarca(cod);

                return RedirectToAction("ConsMarca", new { aviso = 1 });
            }
            catch
            {
                return RedirectToAction("ConsMarca", new { aviso = 3 });
            }
        }

        //-------------------------------

        public ActionResult CadFornecedor(string cod, int aviso)
        {
            if (VerificaGerente() != 0 && VerificaGerente() != 1)
            {
                ViewBag.cod = cod;
                ViewBag.Gerente = VerificaGerente();

                if (aviso == 1)
                {
                    ViewBag.MsgError = "O cadastro foi realizado com sucesso";
                }
                else if (aviso == 2)
                {
                    ViewBag.MsgError = "Ocorreu um erro e o cadastro não foi realizado.";
                }

                //pesquisar codigo e retornar info para viewbagd

                ViewBag.cod = cod;
                FornecedorModel dto = acaoFornecedor.pesquisaFornCod(cod);

                ViewBag.NomeForn = dto.nome;
                ViewBag.SobreForn = dto.sobrenome;
                ViewBag.EmailForn = dto.email;
                ViewBag.TelForn = dto.no_tel;
                ViewBag.CNPJ = dto.no_cnpj;


                return View();
            }
            else
            {
                return RedirectToAction("acessoNegado", "Erro");
            }
        }

        [HttpPost]
        public ActionResult CadFornecedor(string cod, string modo, FormCollection frm)
        {
            ViewBag.Gerente = VerificaGerente();
            try
            {
                //FORNECEDOR
                fornecedor.nome = frm["txtNome"];
                fornecedor.sobrenome = frm["txtSobrenome"];
                fornecedor.email = frm["txtEmail"];
                fornecedor.no_tel = frm["txtTel"];
                fornecedor.no_cnpj = frm["txtCNPJ"];

                //inserir ou alterar dados
                if (modo == "cadastro")
                {
                    acaoFornecedor.inserirFornecedor(fornecedor);

                    return RedirectToAction("CadFornecedor", new { cod = 0, aviso = 1 });
                }
                else
                {
                    fornecedor.cd_fornecedor = cod;

                    acaoFornecedor.atualizaForne(fornecedor);

                    return RedirectToAction("ConsFornecedor", new { aviso = 2 });
                }
            }

            catch
            {
                return RedirectToAction("CadFornecedor", new { cod = 0, aviso = 2 });
            }

        }



        public ActionResult ConsFornecedor(int aviso)
        {

            Notificacao(aviso);
            if (VerificaGerente() != 0 && VerificaGerente() != 1)
            {
                ViewBag.Gerente = VerificaGerente();
                ViewBag.Qtde = acaoCarrinho.Count("tbFornecedor");

                //VIEWBAG COM MARCAS
                ViewBag.ListaDeMarcas = acaoFornMarca.consultaMarcaLista();
                ViewBag.ListaDeMarcasLentes = acaoFornMarca.consultaMarcaListaLente();
                //CNPJ DOS FORNECEDORES
                ViewBag.ListCNPJ = acaoFornecedor.listaCNPJ();
                //MARCAS DE CADA FORNECEDOR
                ViewBag.ForneMarcas = acaoFornMarca.exibirfornecedorMarcas();

                return View(acaoFornecedor.consultaForne());

            }
            else
            {
                return RedirectToAction("acessoNegado", "Erro");
            }
        }

        [HttpPost]
        public ActionResult ConsFornecedor(FormCollection frm)
        {
            ViewBag.Gerente = VerificaGerente();
            ViewBag.Qtde = acaoCarrinho.Count("tbFornecedor");

            //VIEWBAG COM MARCAS
            ViewBag.ListaDeMarcas = acaoFornMarca.consultaMarcaLista();
            ViewBag.ListaDeMarcasLentes = acaoFornMarca.consultaMarcaListaLente();
            //CNPJ DOS FORNECEDORES
            ViewBag.ListCNPJ = acaoFornecedor.listaCNPJ();
            //MARCAS DE CADA FORNECEDOR
            ViewBag.ForneMarcas = acaoFornMarca.exibirfornecedorMarcas();


            FornecedorModel forn = new FornecedorModel();

            forn.no_cnpj = frm["CNPJ"];
            if (frm["txtMarcaLente"] != null)
            {
                forn.nm_marca = frm["txtMarcaLente"];
            }
            else
            {
                forn.nm_marca = frm["txtMarca"];

            }

            return View(acaoFornecedor.pesquisaForn(forn));
        }


        public ActionResult ExcluirFor(string cod)
        {
            try
            {
                //excluir 
                acaoFornecedor.deletarFornecedor(cod);

                return RedirectToAction("ConsFornecedor", new { aviso = 1 });
            }
            catch
            {
                return RedirectToAction("ConsFornecedor", new { aviso = 3 });
            }
        }


        //-------------------------------
        public ActionResult ConsVendas()
        {
            if (VerificaGerente() != 0 && VerificaGerente() != 1)
            {
                ViewBag.Gerente = VerificaGerente();
                ViewBag.QtdeVendas = acaoCarrinho.Count("tbPedido");

                ViewBag.ListCliente = acaoCliente.listaNomeCliente();

                return View(acaoPedido.consultaVendasLista());
            }
            else
            {
                return RedirectToAction("acessoNegado", "Erro");
            }
        }

        [HttpPost]
        public ActionResult ConsVendas(FormCollection frm)
        {
            ViewBag.Gerente = VerificaGerente();
            ViewBag.QtdeVendas = acaoCarrinho.Count("tbPedido");

            ViewBag.ListCliente = acaoCliente.listaNomeCliente();

            VendasModel dtoVenda = new VendasModel();
            string nomej = frm["NomeCli"];
            if (frm["NomeCli"] != string.Empty)
            {
                string[] nome = frm["NomeCli"].Split(' ');
                dtoVenda.nm_cliente = nome[0];
                dtoVenda.cliente_sobrenome = nome[1];
            }
            else
            {
                dtoVenda.nm_cliente = "";
                dtoVenda.cliente_sobrenome = "";
            }

            if (frm["mes"] != string.Empty)
            {
                dtoVenda.dt_pedido = "/" + frm["mes"] + "/";
            }
            else if (frm["ano"] != string.Empty)
            {
                dtoVenda.dt_pedido = "/" + frm["ano"];
            }
            else
            {
                dtoVenda.dt_pedido = "";
            }

            if (frm["parc"] != null)
            {
                dtoVenda.parcelamento = frm["parc"];
            }
            else
            {
                dtoVenda.parcelamento = "";
            }

            if (frm["statusPedido"] != null)
            {
                dtoVenda.status_pedido = frm["statusPedido"];
            }
            else
            {
                dtoVenda.status_pedido = "";
            }

            return View(acaoPedido.pesquisarVendasLista(dtoVenda));
        }

        public ActionResult DetalheVendas(string cod, string cli)
        {
            ViewBag.Gerente = VerificaGerente();
            if (VerificaGerente() != 0 && VerificaGerente() != 1)
            {
                PedidoSuperModel pedidos = new PedidoSuperModel();

                //PEDIDO
                Pedido pedido = new Pedido();
                pedidos.pedidoModelLista = pedido.consultaPedidoCod(cod);

                //RECEITA
                PedidoReceita bll2 = new PedidoReceita();
                pedidos.receitaModelLista = bll2.consultaPedidoReceita("0", cod);

                //FORMA DE PAGAMENTO
                Pagamento bll3 = new Pagamento();
                pedidos.pagamentoModel = bll3.pesquisaFormaPagamento("0", cod);
                //ITENS DO PEDIDO
                PedidoItem bll4 = new PedidoItem();
                pedidos.produtoModelLista = bll4.consultaPedidoItens("0", cod);
                //CLIENTE
                ClienteModel clidto = new ClienteModel();
                clidto.cd_cliente = cli;
                pedidos.clienteModel = acaoCliente.pesquisarCliente(clidto);

                return View(pedidos);
            }
            else
            {
                return RedirectToAction("acessoNegado", "Erro");
            }
        }

    }
}