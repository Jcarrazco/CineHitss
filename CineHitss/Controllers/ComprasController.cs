using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CineHitss.Controllers
{
    public class ComprasController : Controller
    {
        // GET: Compras
        public ActionResult Compras(string Movieid)
        {
            if (Session["Login"] != null)
            {
                CineHitssService.User User = (CineHitssService.User)Session["Login"];
                ViewBag.User = User.Username;
            }

            int ID = Convert.ToInt32(Movieid);
            DataBaseCineHitssEntities context = new DataBaseCineHitssEntities();
            context.Configuration.LazyLoadingEnabled = false;
            context.Configuration.ProxyCreationEnabled = false;
            Pelicula pelicula = context.Peliculas.First(p => p.id == ID);
            Genero genero = context.Generos.First(g => g.id == pelicula.GeneroID);

            ViewBag.Gen = genero.Nombre;
            ViewBag.MId = pelicula.id;
            return View(pelicula);
        }
    }
}