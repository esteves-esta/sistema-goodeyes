using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using tccGoodEyes.Repositorio.Acoes;
using tccGoodEyes.Models;
using tccGoodEyes.Relatorio;

namespace tccGoodEyes.Controllers
{
    public class ProdutoController : Controller
    {
        //--------------
        //DECLARAR CLASSES
        //ACOES
        Cliente acaoCliente = new Cliente();
        Login acaoLogin = new Login();
        Funcionario acaoFuncionario = new Funcionario();
        Produto acaoProduto = new Produto();
        Estoque acaoEstoque = new Estoque();

        CarrinhoDeCompra acaoCarrinho = new CarrinhoDeCompra();
        CarrinhoReceita acaoCarrinhoReceita = new CarrinhoReceita();

        Pedido acaoPedido = new Pedido();
        Receita acaoReceita = new Receita();
        PedidoReceita acaoPedidoReceita = new PedidoReceita();
        PedidoItem acaoItens = new PedidoItem();

        Pagamento acaoPagamento = new Pagamento();

        FornecedorMarca acaoFornMarca = new FornecedorMarca();
        Fornecedor acaoFornecedor = new Fornecedor();

        ProdutoNotificacao bllFavorito = new ProdutoNotificacao();

        //Model
        CarrinhoDeCompraModel carrinhoDeCompra = new CarrinhoDeCompraModel();
        CarrinhoReceitaModel carrinhoReceita = new CarrinhoReceitaModel();
        //ClienteModel Cliente = new ClienteModel();
        LoginModel login = new LoginModel();
        FuncionarioModel funcionario = new FuncionarioModel();
        ProdutoModel produto = new ProdutoModel();
        EstoqueModel estoque = new EstoqueModel();
        FornecedorModel fornecedor = new FornecedorModel();
        
        PedidoModel pedido = new PedidoModel();
        FornecedorMarcaModel fornMarca = new FornecedorMarcaModel();
        ProdutoNotificacaoModel notificacao = new ProdutoNotificacaoModel();

        //VERIFICA SE USUARIO ESTA LOGADO
        public ClienteModel UsuarioLogado()
        {
            string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
            HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //Get the cookie by it's name
            if (authCookie != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
                string email = ticket.Name;
                Cliente bllcli = new Cliente();
                ClienteModel cliente = bllcli.pesquisarClienteporEmail(email);
                return cliente;
            }
            else
            {
                //SE NÃO ESTIVER LOGADO RETORNA CODIGO 0
                ClienteModel cliente = new ClienteModel();
                cliente.cd_cliente = "0";
                return cliente;
            }

        }

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

        [HttpGet]
        public ActionResult ConsultaProdutos(string tipo, string genero)
        {
            ViewBag.logado = verificaLogado();

            if (tipo != string.Empty && genero != string.Empty)
            {
                ViewBag.tipoConsulta = tipo;
                ViewBag.genero = genero;
            }
            else
            {
                tipo = "grau";
                genero = "fem";
            }

            ViewBag.listaMarca = acaoFornMarca.consultaMarcaLista();

            produto.tipo = tipo;
            produto.descricao = genero;
            produto.nm_marca = "";
            produto.aspecto = "";
            produto.vl_preco_unitario = "0-10000";

            return View(acaoProduto.pesquisaCompletaProduto(produto));
        }

        string marca, mate, mode, idade, corarmacao, corlente, tratamentoLente, aspecto, preco;

        [HttpPost]
        public ActionResult ConsultaProdutos(string tipo, string genero, FormCollection frm)
        {
            ViewBag.logado = verificaLogado();
            ViewBag.tipoConsulta = tipo;
            ViewBag.genero = genero;

            ViewBag.listaMarca = acaoFornMarca.consultaMarcaLista();

            marca = frm["marca"];
            mate = frm["mate"];
            mode = frm["modelo"];
            idade = frm["faixa"];
            corarmacao = frm["corarm"];
            tratamentoLente = frm["trat"];
            corlente = frm["corlente"];
            aspecto = "";
            preco = frm["preco"];

            if (marca == null || marca == "todos")
            {
                marca = "";
            }
            if (mate == null)
            {
                mate = "";
            }
            if (mode == null)
            {
                mode = "";
            }
            if (idade == null)
            {
                idade = "";
            }
            if (corarmacao == null)
            {
                corarmacao = "";
            }
            if (tratamentoLente == null)
            {
                tratamentoLente = "";
            }
            if (corlente == null)
            {
                corlente = "";
            }
            if (preco == "")
            {
                preco = "0-100000";
            }

            ProdutoModel produto = new ProdutoModel();
            produto.tipo = tipo;
            produto.nm_marca = marca;
            produto.descricao = mate + "%" +
                        mode + "%" +
                        genero + "%" +
                        idade + "%" +
                        corarmacao + "%" +
                        tratamentoLente + "%" +
                        corlente + "%";
            produto.aspecto = aspecto;
            produto.vl_preco_unitario = preco;

            /*ViewBag.marca = marca;
            ViewBag.mate = mate;
            ViewBag.modelo = mode;
            ViewBag.faixa = idade;
            ViewBag.corarm = corarmacao;
            ViewBag.trat = tratamentoLente;
            ViewBag.corlente = corlente;
            ViewBag.preco = preco;
            */
            return View(acaoProduto.pesquisaCompletaProduto(produto));
        }

        //-------------------------------------
        //PRODUTO SELECIONADO

        public ActionResult ProdutoSelecionado(string cod, int erro)
        {

            ViewBag.logado = verificaLogado();
            //MENSAGEM DE AVISO
            if (erro == 1)
            {
                ViewBag.erro = "Por favor faça seu login para adicionar este item a seu carrinho.";
            }
            else if (erro == 2)
            {
                ViewBag.erro = "Houve um erro interno e o produto não foi adicionado a seu carrinho. " +
                    "Por favor tente novamente. Nos desculpamos pelo inconveniente.";
            }
            else if (erro == 3)
            {
                ViewBag.erro = "Houve um erro interno e o produto não foi adicionado a seus favoritos. " +
                    "Por favor tente novamente. Nos desculpamos pelo inconveniente.";
            }
            else if (erro == 4)
            {
                ViewBag.erro = "Você será avisado quando o produto estiver disponivel.";
            }
            else if (erro == 5)
            {
                ViewBag.erro = "A notificação já está ativada.";
            }

            //VERIFICAR QUANTIDADE NO ESTOQUE
            EstoqueModel estoque = new EstoqueModel();
            estoque.cd_produto = cod;
            if (acaoEstoque.verificaEstoque(estoque) == 0)
            {
                ViewBag.esgotado = true;
            }
            else
            {
                ViewBag.esgotado = false;
            }

            //RETORNAR INFORMAÇOES DO PRODUTO
            ProdutoModel produto = acaoProduto.pesquisaProdutoPorCodigo(cod);
            string[] aspectos = produto.aspecto.Split(' ');
            string[] descricao = produto.descricao.Split(' ');

            ViewBag.mate = descricao[0];
            ViewBag.modelo = descricao[1];
            ViewBag.gen = descricao[2];
            ViewBag.faixaEtaria = descricao[3].Replace("-", " a ") + " anos";
            ViewBag.corArma = descricao[4];
            ViewBag.tratamentoLente = descricao[5];
            ViewBag.corLente = descricao[6];

            ViewBag.tamPonte = aspectos[0];
            ViewBag.larguraLente = aspectos[1];
            ViewBag.altLente = aspectos[2];

            return View(produto);
        }

        [HttpPost]
        public ActionResult ProdutoSelecionado(string cod, FormCollection frm)
        {
            ViewBag.logado = verificaLogado();
            string codi = cod;

            try
            {
                if (UsuarioLogado().cd_cliente == "0")
                {
                    return RedirectToAction("ProdutoSelecionado", new { cod = codi, erro = 1 });
                }
                else
                {
                    ClienteModel cliente = UsuarioLogado();

                    /* SALVAR INFORMCAÇÕES QUE VIRAM DA PAGINA PRODUTO SELECIONADO PARA O CARRINHO */
                    CarrinhoDeCompraModel carrinhoDeCompra = new CarrinhoDeCompraModel();
                    carrinhoDeCompra.cd_cliente = cliente.cd_cliente;
                    carrinhoDeCompra.cd_produto = cod;
                    carrinhoDeCompra.qt_item = frm["txtQntd"];

                    acaoCarrinho.inserirCarrinho(carrinhoDeCompra);
                    return RedirectToAction("Lente");
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("ProdutoSelecionado", new { cod = codi, erro = 3 });
            }
        }

        //*****---------------------------------------------
        //FAVORITO
        public ActionResult Favorito(string cod)
        {
            string codi = cod;
            try
            {
                ClienteModel cliente = UsuarioLogado();
                if (bllFavorito.verificaSeJaFavoritou(cod, cliente.cd_cliente) == false)
                {
                    //INCLUIR FAVORITO
                    notificacao.cd_produto = cod;
                    notificacao.cd_cliente = cliente.cd_cliente;
                    bllFavorito.inserirFavoritos(notificacao);

                    return RedirectToAction("ProdutoSelecionado", new { cod = codi, erro = 4 });
                }
                else
                {
                    return RedirectToAction("ProdutoSelecionado", new { cod = codi, erro = 5 });
                }
            }
            catch
            {
                return RedirectToAction("ProdutoSelecionado", new { cod = codi, erro = 3 });
            }
        }


        //-------------------------------------
        //LENTE

        [HttpGet]
        public ActionResult Lente()
        {
            ViewBag.logado = verificaLogado();
            ViewBag.ListaLente = acaoFornMarca.consultaMarcaLenteprod();

            /*PESQUISAR SE CLIENTE TEM RECEITA E RETORNAR PARA A PAGINA RESULTADO*/


            //TERMINAR BLL DA RECEITA 
            ClienteModel cliente = UsuarioLogado();
            if (acaoReceita.VerificaSePossuiReceita(cliente) == true)
            {
                List<ReceitaModel> listaReceita = acaoReceita.pesquisarPorCliente(cliente);
                ViewBag.listReceita = listaReceita;
            }

            List<ProdutoModel> lista = (List<ProdutoModel>)acaoCarrinho.pesquisarCarrinho(cliente.cd_cliente);
            ViewBag.listaProd = lista;

            return View();
        }

        [HttpPost]
        public ActionResult Lente(FormCollection frm)
        {
            ViewBag.logado = verificaLogado();
            ClienteModel cliente = UsuarioLogado();

            string grau = frm["grau"];
            if (grau == "MONOFOCAL" || grau == "MULTIFOCAL")
            {

                if (frm["receitaCod"] != null)
                {
                    //salvar a receita no carrinho

                    carrinhoDeCompra.cd_cliente = cliente.cd_cliente;
                    carrinhoDeCompra.cd_produto = frm["lente"];
                    carrinhoDeCompra.qt_item = "2";

                    acaoCarrinho.inserirCarrinho(carrinhoDeCompra);

                    carrinhoReceita.cd_receita = frm["receitaCod"];
                    carrinhoReceita.cd_cliente = cliente.cd_cliente;
                    carrinhoReceita.cd_carrinho = acaoCarrinho.getUltimoCadastrado(cliente.cd_cliente);
                    acaoCarrinhoReceita.inserirPedidoReceita(carrinhoReceita);
                }
                else
                {
                    string oee, oec, oeei, oea, ode, odc, odei, oda;

                    oee = frm["txtOEEsf"];
                    oec = frm["txtOECil"];
                    oeei = frm["txtOEEixo"];
                    oea = frm["txtOEAdicao"];

                    ode = frm["txtODEsf"];
                    odc = frm["txtODCil"];
                    odei = frm["txtODEixo"];
                    oda = frm["txtODAdicao"];
                    if (oee == "")
                    {
                        oee = "0";
                    }
                    if (oec == "")
                    {
                        oec = "0";
                    }
                    if (oeei == "")
                    {
                        oeei = "0";
                    }
                    if (oea == "")
                    {
                        oea = "0";
                    }
                    if (ode == "")
                    {
                        ode = "0";
                    }
                    if (odc == "")
                    {
                        odc = "0";
                    }
                    if (odei == "")
                    {
                        odei = "0";
                    }
                    if (oda == "")
                    {
                        oda = "0";
                    }
                    ReceitaModel re = new ReceitaModel();
                    re.cd_cliente = cliente.cd_cliente;
                    re.nm_oftalmo = frm["txtOftalmo"];
                    re.sobrenome_oftalmo = frm["txtSobreOftalmo"];

                    re.dt_validade = frm["txtDataValidade"];
                    re.dt_receita = frm["txtDataReceita"];

                    re.olho_esquerdo = oee
                        + " " + oec
                        + " " + oeei
                        + " " + oea;

                    re.olho_direito = ode
                        + " " + odc
                        + " " + odei
                        + " " + oda;

                    re.distancia_pupilar = frm["txtDP"];
                    re.observacao = frm["observacao"];

                    //salvar receita no carrinho

                    acaoReceita.inserirReceita(re);

                    CarrinhoDeCompraModel carrinhoDeCompra = new CarrinhoDeCompraModel();
                    carrinhoDeCompra.cd_cliente = cliente.cd_cliente;
                    carrinhoDeCompra.cd_produto = frm["lente"];
                    carrinhoDeCompra.qt_item = "2";

                    acaoCarrinho.inserirCarrinho(carrinhoDeCompra);

                    carrinhoReceita.cd_receita = acaoReceita.getUltimoCadastrado(cliente.cd_cliente);
                    carrinhoReceita.cd_cliente = cliente.cd_cliente;
                    carrinhoReceita.cd_carrinho = acaoCarrinho.getUltimoCadastrado(cliente.cd_cliente);
                    acaoCarrinhoReceita.inserirPedidoReceita(carrinhoReceita);
                }
            }

            ViewBag.ListaLente = acaoFornMarca.consultaMarcaListaLente();

            return RedirectToAction("Carrinho");
        }


        //-------------------------------------
        //CARRINHO DE COMPRAS
        public ActionResult Carrinho()
        {
            ViewBag.logado = verificaLogado();

            ClienteModel cliente = UsuarioLogado();
            IEnumerable<ProdutoModel> vazio = new List<ProdutoModel>();
            if (UsuarioLogado().cd_cliente == "0")
            {
                ViewBag.erro = "Por favor faça seu login para ver seu carrinho.";
                ViewBag.carrinhoVazio = true;
                ViewBag.link = "/Cliente/Login?erro=0";

                return View(vazio);
            }
            else if (acaoCarrinho.VerificaCarrinhoVazio(cliente) == true)
            {
                ViewBag.erro = "Seu carrinho de compras esta vazio!";
                ViewBag.carrinhoVazio = true;
                ViewBag.link = "/Produto/ConsultaProdutos?tipo=grau&genero=fem";

                return View(vazio);
            }
            else
            {
                ViewBag.carrinhoVazio = false;

                return View(acaoCarrinho.pesquisarCarrinho(cliente.cd_cliente));
            }
        }

        [HttpPost]
        public ActionResult Carrinho(FormCollection frm)
        {
            ViewBag.logado = verificaLogado();
            /* salvar mudanças de quantidade de produtos*/

            CarrinhoDeCompraModel carrinhoDeCompra = new CarrinhoDeCompraModel();
            ClienteModel cliente = UsuarioLogado();
            List<ProdutoModel> lista = (List<ProdutoModel>)acaoCarrinho.pesquisarCarrinho(cliente.cd_cliente);

            //verificar qual produto teve quantidade mudada
            foreach (ProdutoModel produt in lista)
            {
                string txtInput = produt.cd_carrinho + "-" + produt.cd_produto;
                string qtd = frm[txtInput];
                if (produt.qt_item != qtd)
                {
                    carrinhoDeCompra.cd_carrinho = produt.cd_carrinho;
                    carrinhoDeCompra.cd_produto = produt.cd_produto;
                    carrinhoDeCompra.cd_cliente = cliente.cd_cliente;
                    carrinhoDeCompra.qt_item = frm[txtInput];
                    acaoCarrinho.atualizarCarrinho(carrinhoDeCompra);
                }
            }

            return View(acaoCarrinho.pesquisarCarrinho(cliente.cd_cliente));
        }


        public ActionResult DeletarItenPedido(string cod)
        {
            ViewBag.logado = verificaLogado();

            acaoCarrinho.deletarCarrinho(cod);

            if (acaoCarrinhoReceita.verificaReceita(cod) == true)
            {
                acaoCarrinhoReceita.deletarCarrinhoReceitaPorProduto(cod);

            }

            return RedirectToAction("Carrinho");
        }

        //------------------------------------------------------------------------
        //FINALIZAR PEDIDO
        [HttpGet]
        public ActionResult FinalizarPedido()
        {
            ViewBag.logado = verificaLogado();

            ClienteModel cliente = UsuarioLogado();

            /* PESQUISAR E RETORNAR ENDEREÇO DO CLIENTE */

            ViewBag.EndCli = cliente.nm_rua;
            ViewBag.NumCli = cliente.no_rua;
            ViewBag.CepCli = cliente.no_cep;
            ViewBag.CompleCli = cliente.complemento;
            ViewBag.BairroCli = cliente.bairro;
            ViewBag.CidadeCli = cliente.cidade;
            ViewBag.Estado = cliente.estado;
            ViewBag.EstadoUF = cliente.sg_uf;


            /* PESQUISAR E RETORNAR CONTEUDO DO CARRINHO */
            return View(acaoCarrinho.pesquisarCarrinho(cliente.cd_cliente));
        }

        [HttpPost]
        public ActionResult FinalizarPedido(FormCollection frm)
        {
            ViewBag.logado = verificaLogado();
            //PESQUISAR O CLIENTE LOGADO E RETORNAR INFORMAÇÕES DELE
            ClienteModel cliente = UsuarioLogado();

            //SALVAR MUDANÇAS DE ENDEREÇO
            cliente.nm_rua = frm["txtEnd"];
            cliente.no_rua = frm["txtNum"];
            cliente.no_cep = frm["txtCEP"];
            cliente.complemento = frm["txtComp"];
            cliente.bairro = frm["txtBairro"];
            cliente.cidade = frm["txtCidade"];
            cliente.estado = frm["txtEstado"];
            cliente.sg_uf = frm["txtUF"];

            acaoCliente.atualizarCliente(cliente);

            //PEGAR ITENS DO CARRINHO
            List<ProdutoModel> listaCarrinho = acaoCarrinho.pesquisarCarrinhoLista(cliente.cd_cliente);

           // CarrinhoReceitaModel dtoCarrinhoReceita = new CarrinhoReceitaModel();

            try
            {
                //SALVAR PEDIDO

                PedidoModel pedido = new PedidoModel();
                pedido.cd_cliente = cliente.cd_cliente;
                pedido.dt_pedido = DateTime.Now.ToString("dd/MM/yyyy");
                if (frm["paga"] == "boleto")
                {
                    pedido.status_pedido = "1";
                }
                else
                {
                    pedido.status_pedido = "2";
                }
                acaoPedido.inserirPedido(pedido);

                //PEGAR CODIGO DO PEDIDO
                string codigoPedido = acaoPedido.getUltimoCadastrado(cliente.cd_cliente);

                //SALVAR ITENS DO PEDIDO

                foreach (ProdutoModel produto in listaCarrinho)
                {
                    PedidoItemModel pedidoItens = new PedidoItemModel();
                    pedidoItens.cd_pedido = codigoPedido;
                    pedidoItens.cd_produto = produto.cd_produto;
                    pedidoItens.qt_item = produto.qt_item;

                    acaoItens.inserirPedidoItens(pedidoItens);

                    //SE ESTE ITEN DO CARRINHO POSSUI RECEITA ATRELADA A ELE ADICIONAR RECEITA A PEDIDO RECEITA
                    if (acaoCarrinhoReceita.verificaReceita(produto.cd_carrinho) == true)
                    {
                        PedidoReceitaModel pedReceita = new PedidoReceitaModel();
                        pedReceita.cd_pedido = codigoPedido;
                        pedReceita.cd_receita = acaoCarrinhoReceita.pesquisarCodCarrinhoReceita(produto.cd_carrinho);
                        pedReceita.cd_item = acaoItens.ultimoCodigo(cliente.cd_cliente);

                        acaoPedidoReceita.inserirPedidoReceita(pedReceita);
                    }
                    //DIMINIUR QUANTIDADE NO ESTOQUE
                    acaoProduto.diminuirestoque(produto.cd_produto, produto.qt_item);
                }

                //SALVAR FORMA DE PAGAMENTO
                PagamentoModel paga = new PagamentoModel();
                paga.cd_pedido = codigoPedido;
                Boleto boleto = new Boleto();

                string valor = frm["valor"];

                if (frm["paga"] == "boleto")
                {
                    paga.tipo_pagamento = boleto.gerarNumeroBoleto(Convert.ToDouble(valor));
                    paga.parcelamento = "Á VISTA";
                }
                else
                {
                    paga.tipo_pagamento = frm["nomecartao"].Replace(' ', '-')
                        + " " + frm["numcartao"]
                        + " " + frm["txtBand"]
                        + " " + frm["codseg"]
                        + " " + frm["mes"] + "/" + frm["ano"];

                    if (frm["parcelamento"] != "1")
                    {
                        paga.parcelamento = frm["parcelamento"];

                     }
                    else
                    {
                      paga.parcelamento = "Á VISTA";
                    }

                }
                acaoPagamento.inserirFormaPagamento(paga);

            }
            catch (Exception e)
            {
               return View();
            }


            //EXCLUIR ITENS DO CARRINHO APOS COLOCA-LOS NA TABELA ITENS PEDIDO
            acaoCarrinho.deletarCarrinho(cliente);
            acaoCarrinhoReceita.deletarCarrinhoReceitaPorCliente(cliente.cd_cliente);

            return RedirectToAction("PedidoConfirmado");
        }

        public ActionResult GerarBoleto()
        {
            ViewBag.logado = verificaLogado();
            //pesquisar atraves do email o cliente e devolver resultado como um no cliente model
            ClienteModel cliente = UsuarioLogado();

            string codigoPedido = acaoPedido.getUltimoCadastrado(cliente.cd_cliente);

            PagamentoModel paga = acaoPagamento.pesquisaFormaPagamento(cliente.cd_cliente, codigoPedido);

            Boleto boleto = new Boleto();
            byte[] abytes = boleto.GeraBoleto(cliente, paga.tipo_pagamento, Convert.ToDouble(paga.vl_total));
            return File(abytes, "application/pdf");
        }


        //-------------------------------------
        //CONFIRMACAO DO PEDIDO
        public ActionResult PedidoConfirmado()
        {
            ViewBag.logado = verificaLogado();
            if (verificaLogado() == true)
            {
                ClienteModel cliente = UsuarioLogado();

                //PESQUISAR ULTIMO PEDIDO E RETORNAR COD DO PEDIDO E TOTAL

                string codigoPedido = acaoPedido.getUltimoCadastrado(cliente.cd_cliente);
                ViewBag.NumPedido = codigoPedido;

                ViewBag.Total = acaoPagamento.consultaFormaPagamento();

                // PESQUISAR E RETORNAR ENDEREÇO DO CLIENTE 
                ViewBag.EndCli = cliente.nm_rua;
                ViewBag.NumCli = cliente.no_rua;
                ViewBag.CepCli = cliente.no_cep;
                ViewBag.CompleCli = cliente.complemento;
                ViewBag.BairroCli = cliente.bairro;
                ViewBag.CidadeCli = cliente.cidade;
                ViewBag.Estado = cliente.estado;
                ViewBag.EstadoUF = cliente.sg_uf;
                ViewBag.email = cliente.email;



                //RECEITA

                if (acaoPedidoReceita.verificaTemReceita(codigoPedido) == true)
                {
                    string codR = acaoPedidoReceita.getCodReceita(codigoPedido);

                    ReceitaModel receita = acaoReceita.pesquisarPorCodigo(codR);

                    ViewBag.dtReceita = receita.dt_receita;
                    ViewBag.dtValidade = receita.dt_validade;
                    ViewBag.nomeOftalmo = receita.nm_oftalmo + ' ' + receita.sobrenome_oftalmo;

                    string[] od = receita.olho_direito.Split(' ');
                    string[] oe = receita.olho_esquerdo.Split(' ');

                    ViewBag.OEesferico = oe[0];
                    ViewBag.OEcili = oe[1];
                    ViewBag.OEeixo = oe[2];
                    ViewBag.OEadi = oe[3];

                    ViewBag.ODesferico = od[0];
                    ViewBag.ODcili = od[1];
                    ViewBag.ODeixo = od[2];
                    ViewBag.ODadi = od[3];

                    ViewBag.dp = receita.distancia_pupilar;
                    ViewBag.Observacao = receita.observacao;
                }

                //PAGAMENTO INFORMACAO

                PagamentoModel pagamento = acaoPagamento.pesquisaFormaPagamento(cliente.cd_cliente, codigoPedido);

                ViewBag.formaPagamento = pagamento.tipo_pagamento;

                string[] pagamentoArray = pagamento.tipo_pagamento.Split(' ');

                ViewBag.numCartao = pagamentoArray[1];
                ViewBag.nomeCartao = pagamentoArray[0];
                ViewBag.bandeira = pagamentoArray[2];
                ViewBag.codSeg = pagamentoArray[3];
                ViewBag.validade = pagamentoArray[4];

                ViewBag.parcelamento = pagamento.parcelamento;
                ViewBag.total = pagamento.vl_total;
                // PESQUISAR PEDIDO  ITENS E PESQUISAR PRODUTOS E RETORNA-LOS COMO IENUMERABLE

                PedidoModel pedido = new PedidoModel();

                try
                {
                    string itens1 = string.Empty;
                    List<ProdutoModel> itens = (List<ProdutoModel>)acaoItens.consultaPedidoItens("0", codigoPedido);
                    foreach (ProdutoModel produto in itens)
                    {
                        itens1 += "<tr>" +
                             "<td>" + produto.nm_marca + produto.cd_produto + "</td>" +
                             "<td>" + produto.vl_preco_unitario + "</td>" +
                             "<td>" + produto.qt_item + "</td>" +
                             "<td>" + produto.vl_subtotal + "</td>" +
                             "</tr>";
                    }

                    WebMail.SmtpServer = "smtp-mail.outlook.com";
                    WebMail.SmtpPort = 587;
                    WebMail.UserName = "goodresults@outlook.com.br";
                    WebMail.Password = "Projetoinfo";
                    WebMail.From = "goodresults@outlook.com.br";
                    WebMail.EnableSsl = true;
                    // Send email

                    WebMail.Send(to: cliente.email,
                        subject: "Detalhes do Pedido.",
                        body: "<div><h2> Olá </h2>" +
                        "<p> Sua compra de número " + codigoPedido + " foi realizado com sucesso. Seu pedido encontra-se em análise, no momento ele está aguardando uma confirmação de pagamento!</p>" +
                       " </div>" +
                       "<div class='col1'>" +
                       "<h4>Informações de entrega:</h4>" +
                       "<ul>" +
                       "<li>" + cliente.nm_rua + " - " + cliente.no_rua + " - " + cliente.complemento + "</li>" +
                       "<li>" + cliente.bairro + "</li>" +
                       "<li>" + cliente.cidade + " - " + cliente.estado + " - " + cliente.sg_uf + "</li>" +
                       "</ul>" +
                       "</div>" +
                       "<div>" +
                       "<h4>Informações de Pagamento:</h4>" +
                       "<p>" + pagamento.tipo_pagamento + "</p>" +
                       "</div>" +
                       "<div>" +
                       "<h4>Informações do Pedido:</h4>" +
                       "<p>Valor Total do Pedido: " + pagamento.vl_total + "</p>" +
                       "</div>" +
                       "<table>" +
                       "<tr>" +
                       "<th>Item</th>" +
                       "<th>Preço Uni.</th>" +
                       "<th>QTDE.</th>" +
                       "<th> Total </th>" +
                       "</tr>" +
                       itens1 + "</table>",
                     isBodyHtml: true, priority: "High"
                                );
                }
                catch { }


                return View(acaoItens.consultaPedidoItens("0", codigoPedido));

            }
            else
            {
                return RedirectToAction("acessoNegado", "Erro");
            }
        }


    }
}