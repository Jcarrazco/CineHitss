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
            DataBaseCineHitssEntities context = new DataBaseCineHitssEntities();
            context.Configuration.LazyLoadingEnabled = false;
            context.Configuration.ProxyCreationEnabled = false;
            var peliculas = context.Peliculas.ToList();

            return View();
        }

        public ActionResult Login(string usuario, string contrasenia)
        {
            using (CineHitssService.Service1Client client = new CineHitssService.Service1Client())
            {
                try
                {
                    var _user = client.LoginUser(usuario, contrasenia);
                    Session["Login"] = _user;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            return RedirectToAction("Index", "Filtros", null);
        }
         

    }
}