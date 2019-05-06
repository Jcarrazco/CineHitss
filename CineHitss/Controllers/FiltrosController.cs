using CineHitss.CineHitssService;
using CineHitss.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CineHitss.Controllers
{
    public class FiltrosController : Controller
    {
        // GET: Filtros
        public ActionResult Filtros()
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
        
            
    }
}