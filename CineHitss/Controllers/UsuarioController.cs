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
            xuser.Username = "Juan";
            xuser.Email = "juan@globalhitss.com";
            xuser.Points = 2345;
           

            Session["Login"] = xuser;
            User user2 = (User)Session["Login"];

            ViewBag.login = xuser;
            
            return View();
        }

 
    }
}