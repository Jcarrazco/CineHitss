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
    //: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
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
        /// Metodo de login de usuario
        /// </summary>
        /// <param Username del usuario="_Username"></param>
        /// <param Password del usuario="_Password"></param>
        /// <returns>si existe regresa el usuario completo si no solo un nombre no valido</returns>
        public User LoginUser(string _Username, string _Password)
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
                if (_user.Password != _Password)
                {
                    return new User() { Username = "Usuario No Valido" };
                }

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

        public IEnumerable<Cine> GetCinesxCiudad(string _ciudad)
        {
            Ciudade City = new Ciudade();
            IEnumerable<Cine> _cine = new List<Cine>();

            using (var context = new CineHitssEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;

                City = context.Ciudades.First(x => x.Nombre == _ciudad);

                _cine = context.Cines.Where(c => c.CiudadID == City.id).ToList();
            }

            return _cine;
        }

        public IEnumerable<Pelicula> GetPeliculasxCine(string _location)
        {
            Cine cine = new Cine();
            List<Cartelera> cartelera = new List<Cartelera>();

            IEnumerable<Pelicula> _pelicula = new List<Pelicula>();
            List<Pelicula> _pelicula2 = new List<Pelicula>();

            using (var context = new CineHitssEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;

                cine = context.Cines.First(x => x.Location == _location);
                cartelera = context.Carteleras.Where(y => y.CineID == cine.id).ToList();
                _pelicula = context.Peliculas.ToList();

                foreach (Pelicula itemp in _pelicula)
                {
                    foreach (Cartelera item in cartelera)
                    {
                        if (itemp.id == item.PeliculaID)
                        {
                            _pelicula2.Add(itemp);
                        }
                    }
                }

            }

            return _pelicula2;
        }

        public IEnumerable<Cartelera> HorariosPeliculas(string _peliculaname)
        {
            Pelicula _pelicula = new Pelicula();
            IEnumerable<Cartelera> _cartelera = new List<Cartelera>();

            using (var context = new CineHitssEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;

                _pelicula = context.Peliculas.First(x => x.Nombre == _peliculaname);

                _cartelera = context.Carteleras.Where(c => c.PeliculaID == _pelicula.id).ToList();
            }

            return _cartelera;
        }
    }
}
