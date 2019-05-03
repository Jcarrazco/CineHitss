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
        public ActionResult Index()
        {
            var _user = Session["Login"];
                return View("Filtros");
        }
    }
}