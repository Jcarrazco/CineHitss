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
            ViewBag.MId = Movieid;
            return View();
        }
    }
}