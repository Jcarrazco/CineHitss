using CineHitss.CineHitssService;
using CineHitss.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CineHitss.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {

            var context = new DataBaseCineHitssEntities();
            IEnumerable<Historial> hist = new List<Historial>();
            User xuser = new User();
            Pelicula peli = new Pelicula();
            List<lala> list = new List<lala>();
          
            CineHitssService.User user2 = (CineHitssService.User)Session["Login"];

            if (user2 == null)
            {
                ViewBag.login = xuser;
               
            }
            else
            { 
                ViewBag.login = user2;
                hist = context.Historials.Where(c => c.UserID == user2.id).ToList();
                foreach (var item in hist)
                {
                    Cartelera cart = context.Carteleras.First(ca => ca.id == item.CarteleraID);
                    peli = context.Peliculas.First(p => p.id == cart.PeliculaID);
                    list.Add(new lala { nombre = peli.Nombre, horario = cart.Horario});
                }
            }
             
            ViewBag.historia = list;
            return View();
        }

 
    }
}