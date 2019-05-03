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
        public ActionResult Index()
        {
            Filtro filtro = new Filtro();

            return View(filtro);
        }
    }
}