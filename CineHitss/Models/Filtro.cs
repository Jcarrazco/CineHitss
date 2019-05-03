using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CineHitss.Models
{
    public class Filtro
    {
        public List<Ciudade> Ciudades { get; set; }
        public List<Cine> Cines { get; set; }

        public List<Cine> ObtenerCines(int id)
        {
            return Cines.Where(c => c.CiudadID == id).ToList();
        }
    }
}