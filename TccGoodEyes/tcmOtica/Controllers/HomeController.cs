using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using tccGoodEyes.Repositorio.Acoes;
using tccGoodEyes.Models;

namespace tccGoodEyes.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {

        public ClienteModel EmailDoUsuarioLogado()
        {
            string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
            HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //Get the cookie by it's name
            if (authCookie != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
                string email = ticket.Name;
                Cliente acaoCliente = new Cliente();
                ClienteModel cliente = acaoCliente.pesquisarClienteporEmail(email);
                return cliente;
            }
            else
            {
                ClienteModel cliente = new ClienteModel();
                cliente.cd_cliente = "0";
                return cliente;
            }

        }

        public bool verificaLogado()
        {
            if (EmailDoUsuarioLogado().cd_cliente != "0")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ActionResult Index()
        {
            ViewBag.logado = verificaLogado();

            Produto acaoProduto = new Produto();
            ViewBag.listaProd = acaoProduto.pesquisarTopPromo();

            return View(ViewBag.logado);
        }

        public ActionResult Sobre()
        {
            ViewBag.logado = verificaLogado();

            return View(ViewBag.logado);
        }

        public ActionResult Atendimento()
        {
            ViewBag.logado = verificaLogado();

            return View(ViewBag.logado);
        }



    }


}