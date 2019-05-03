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
            User xuser = new User();

            var user2 = Session["Login"];

            if (user2 == null)
            ViewBag.login = xuser;
            
            else
            ViewBag.login = user2;
            
            return View();
        }

 
    }
}