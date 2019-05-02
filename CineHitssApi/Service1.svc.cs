using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CineHitssApi
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        /// <summary>
        /// Funcion para obtener un User con el solo username
        /// </summary>
        /// <param Nombre de usuario="_Username"></param>
        /// <returns>Se regresa un objeto User</returns>
        public User GetUser(string _Username)
        {
            //READ
            //Creo el objeto que regreso al final
            User _user = new User();
            
            // me permite conectarme al EF solo en lo que dura la funcion
            using (var context = new CineHitssEntities())
            {
                //Deshabilite la funcion para que no cargue datos sin relacion
                context.Configuration.LazyLoadingEnabled = false; 
                //Deshabilite la funcion para que me permita conectarme al servicio
                context.Configuration.ProxyCreationEnabled = false;
               _user = context.Users.First(c => c.Username == _Username);
                
            }

            return _user;
        }

        /// <summary>
        /// Funcion para actualizar los puntos actuales
        /// </summary>
        /// <param Puntos actualizados="_points"></param>
        /// <param Id del usuario="_UserId"></param>
        /// <returns>string de confiarmacion</returns>
        public string AddPointsUser(int _points, int _UserId)
        {
            User _user = new User();
            using (var context = new CineHitssEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                _user = context.Users.Find(_UserId);
                _user.Points = _points;
                context.SaveChanges();
            }
            return "Puntos Actualizados" + _points;
        }

        /// <summary>
        /// Metodo para obtener pelicula por nombre
        /// </summary>
        /// <param Nombre de la pelicula="_peliculaName"></param>
        /// <returns>objeto de pelicula completo</returns>
        public Pelicula GetPelicula(string _peliculaName)
        {
            Pelicula _pelicula = new Pelicula();

            using (var context = new CineHitssEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;

                _pelicula = context.Peliculas.First(c=>c.Nombre == _peliculaName);
            }
            return _pelicula;
        }

        /// <summary>
        /// Metodo para obtener peliculas por genero
        /// </summary>
        /// <param Nombre de genero a buscar ="_genero"></param>
        /// <returns>Lista de peliculas por genero</returns>
        public IEnumerable<Pelicula> GetPeliculasgen(string _genero)
        {
            Genero Gen = new Genero();
            IEnumerable<Pelicula> _peliculas = new List<Pelicula>();

            using (var context = new CineHitssEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;

                Gen = context.Generos.First(x => x.Nombre == _genero);

                _peliculas = context.Peliculas.Where(c => c.GeneroID == Gen.id).ToList();
            }

            return _peliculas;
        }

        /// <summary>
        /// Metodo de Filtro por Clasificacion
        /// </summary>
        /// <param Nombre de la clasificacion="_clasificacion"></param>
        /// <returns>regresa lista de peliculas que coinciden</returns>
        public IEnumerable<Pelicula> GetPeliculasClas(string _clasificacion)
        {
            IEnumerable<Pelicula> _peliculas = new List<Pelicula>();
            using (var context = new CineHitssEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;

                _peliculas = context.Peliculas.Where(c => c.Clasification == _clasificacion).ToList();
            }


            return _peliculas;
        }

        /// <summary>
        /// Metodo de Filtro por Horario
        /// </summary>
        /// <param Nombre de la clasificacion="_clasificacion"></param>
        /// <returns>regresa lista de peliculas que coinciden</returns>
        public IEnumerable<Cartelera> GetPeliculasHorario(DateTime _Dia, string _peliculaname)
        {
            Pelicula _pelicula = new Pelicula();
            IEnumerable<Cartelera> _Carteleras = new List<Cartelera>();
            List<Cartelera> _Cartelerasresult = new List<Cartelera>();

            using (var context = new CineHitssEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;

                _pelicula = context.Peliculas.First(c => c.Nombre == _peliculaname);
                _Carteleras = context.Carteleras.Where(c => c.PeliculaID == _pelicula.id).ToList();
                foreach (Cartelera cartelera in _Carteleras )
                {
                    //Mientras este dentro del dia muestre los horarios que hay(Solo por dia)
                    if (cartelera.Horario > _Dia && cartelera.Horario < _Dia.AddDays(1))
                    {
                        _Cartelerasresult.Add(cartelera);
                    }
                }
            }


            return _Cartelerasresult;
        }
    }
}
