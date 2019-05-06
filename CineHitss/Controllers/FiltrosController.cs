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
            return View();
        }

        /// <summary>
        /// Metodo para obtener las peliculas con filtro 
        /// </summary>
        /// <param name="Cine"></param>
        /// <param name="Genero"></param>
        /// <returns>regresa la vista con las peliculas actualizadas</returns>
        [HttpPost]
        public ActionResult Filtros(string Cine, string Genero)
        {
            if (Session["Login"] != null)
            {
                CineHitssService.User User = (CineHitssService.User)Session["Login"];
                ViewBag.User = User.Username;
            }

            DataBaseCineHitssEntities context = new DataBaseCineHitssEntities();
            context.Configuration.LazyLoadingEnabled = false;
            context.Configuration.ProxyCreationEnabled = false;

            List<Pelicula> pelics = new List<Pelicula>();

            //Filtra por cine las peliculas que existan
            if (!String.IsNullOrEmpty(Cine))
            {
                Cine _cine = context.Cines.First(cc => cc.Location == Cine);
                List<Cartelera> cartelera = context.Carteleras.Where(c => c.CineID == _cine.id).ToList();
                foreach(Cartelera car in cartelera)
                {
                    pelics.Add(context.Peliculas.First(p => p.id == car.PeliculaID));
                }
            }

            //Filtra por genero las peliculas que esten en el cine si es que se aplico el filtro
            if (!String.IsNullOrEmpty(Genero) && Genero != "-1")
            {
                List<Pelicula> GenPeliculas = new List<Pelicula>();
                Genero gener = context.Generos.First(g => g.Nombre == Genero);
                foreach (Pelicula pel in pelics)
                {
                    if (pel.GeneroID == gener.id)
                    {
                        GenPeliculas.Add(pel);
                    }
                }
                return View(GenPeliculas);
            }

            return View(pelics);
        }

        /// <summary>
        /// Metodo para llamada de ajax que retorna una lista de estados
        /// </summary>
        /// <returns>Lista de strings que contienen la lista de estados diferentes</returns>
        [HttpPost]
        public JsonResult GetEstados()
        {
            DataBaseCineHitssEntities context = new DataBaseCineHitssEntities();
            context.Configuration.LazyLoadingEnabled = false;
            context.Configuration.ProxyCreationEnabled = false;
            List<Ciudade> ciudades = new List<Ciudade>();
            ciudades = context.Ciudades.ToList();
            List<string> Estados = new List<string>();

            foreach (Ciudade c in ciudades)
            {
                if (!Estados.Contains(c.Estado))
                {
                    Estados.Add(c.Estado);
                }
            }

            return Json(Estados, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Meotdo que regresa las ciudades del estado en una lista
        /// </summary>
        /// <param Nombre del estado seleccionado="Estado"></param>
        /// <returns>lista de ciudades en lista de string</returns>
        [HttpPost]
        public JsonResult GetCiudades(string Estado)
        {
            DataBaseCineHitssEntities context = new DataBaseCineHitssEntities();
            context.Configuration.LazyLoadingEnabled = false;
            context.Configuration.ProxyCreationEnabled = false;
            List<Ciudade> ciudades = new List<Ciudade>();
            ciudades = context.Ciudades.ToList();
            List<string> _Ciudades = new List<string>();

            foreach (Ciudade c in ciudades)
            {
                if (!_Ciudades.Contains(c.Nombre) && c.Estado == Estado)
                {
                    _Ciudades.Add(c.Nombre);
                }
            }

            return Json(_Ciudades, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Metodo para obtener los cines existentes en una ciudad con ajax call 
        /// </summary>
        /// <param nombre de la ciudad a buscar="_Ciudad"></param>
        /// <returns>lista de strings de cines locations</returns>
        [HttpPost]
        public JsonResult GetCine(string _Ciudad)
        {
            DataBaseCineHitssEntities context = new DataBaseCineHitssEntities();
            context.Configuration.LazyLoadingEnabled = false;
            context.Configuration.ProxyCreationEnabled = false;

            List<Cine> cines = context.Cines.ToList();

            Ciudade ciudad = context.Ciudades.First(c => c.Nombre == _Ciudad);

            List<string> _cines = new List<string>();

            foreach (Cine c in cines)
            {
                if (!_cines.Contains(c.Location) && c.CiudadID == ciudad.id)
                {
                    _cines.Add(c.Location);
                }
            }

            return Json(_cines, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Meotodo para llamada de Ajax para obtener generos
        /// </summary>
        /// <param Nombre de la locacion del cine a buscar="_cine"></param>
        /// <returns>lista de strings con generos</returns>
        [HttpPost]
        public ActionResult GetGeneros(string _cine)
        {
            DataBaseCineHitssEntities context = new DataBaseCineHitssEntities();
            context.Configuration.LazyLoadingEnabled = false;
            context.Configuration.ProxyCreationEnabled = false;

            Cine cine = context.Cines.First(c => c.Location == _cine);
            List<Cartelera> cartelera = context.Carteleras.Where(ca => ca.CineID == cine.id).ToList();

            List<Pelicula> peliculas = new List<Pelicula>();

            List<string> _generos = new List<string>();

            foreach (Cartelera c in cartelera)
            {
                peliculas.Add(context.Peliculas.First(p => p.id == c.PeliculaID));
            }

            foreach (Pelicula p in peliculas)
            {
                Genero gen = context.Generos.First(g => g.id == p.GeneroID);
                if (!_generos.Contains(gen.Nombre))
                {
                    _generos.Add(gen.Nombre);
                }
            }


            return Json(_generos, JsonRequestBehavior.AllowGet);
        }
    }
}