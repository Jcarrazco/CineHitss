using CineHitss.CineHitssService;
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
            if (Session["Login"] != null)
            {
                CineHitssService.User User = (CineHitssService.User)Session["Login"];
                ViewBag.User = User.Username;
            }

            DataBaseCineHitssEntities context = new DataBaseCineHitssEntities();
            context.Configuration.LazyLoadingEnabled = false;
            context.Configuration.ProxyCreationEnabled = false;
            List<Pelicula> Peliculas = context.Peliculas.ToList();
            ViewBag.Existep = true;
            return View(Peliculas);
        }

        public ActionResult Login(string usuario, string contrasenia)
        {
            using (CineHitssService.Service1Client client = new CineHitssService.Service1Client())
            {
                try
                {
                    var _user = client.LoginUser(usuario, contrasenia);
                    Session["Login"] = _user;
                    ViewBag.User = _user.Username;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            return RedirectToAction("Index", "Usuario", null);
        }
         

    }
}