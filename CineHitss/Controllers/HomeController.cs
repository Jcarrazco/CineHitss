using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CineHitss.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult Login(string usuario, string contrasenia)
        {
            CineHitssService.Service1Client client = new CineHitssService.Service1Client();
            try
            {
                var _user = client.LoginUser(usuario, contrasenia);
                Session["Login"] = _user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return RedirectToAction("Index", "Filtros", null);
        }
         

    }
}