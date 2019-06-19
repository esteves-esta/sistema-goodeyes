using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using tccGoodEyes.Repositorio;
using tccGoodEyes.Models;
using tccGoodEyes.Repositorio.Acoes;

namespace tccGoodEyes.Controllers
{
    public class ErroController : Controller
    {
        // GET: Erro

        public ClienteModel EmailDoUsuarioLogado()
        {
            string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
            HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //Get the cookie by it's name
            if (authCookie != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
                string email = ticket.Name;
                Cliente acaocliente = new Cliente();
                ClienteModel cliente = acaocliente.pesquisarClienteporEmail(email);
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

        public ActionResult erro404()
        {
            ViewBag.logado = verificaLogado();
            return View(ViewBag.logado);
        }

        public ActionResult erroInterno()
        {
            ViewBag.logado = verificaLogado();
            return View();
        }

        public ActionResult acessoNegado()
        {
            ViewBag.logado = verificaLogado();
            return View(ViewBag.logado);
        }

    }
}

