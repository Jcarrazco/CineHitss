using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CineHitssApi
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        User GetUser(string _Username);

        [OperationContract]
        string AddPointsUser (int _points, int _UserId);

        [OperationContract]
        Pelicula GetPelicula(string _peliculaName);

        [OperationContract]
        IEnumerable<Pelicula> GetPeliculasgen(string _genero);

        [OperationContract]
        IEnumerable<Pelicula> GetPeliculasClas(string _clasificacion);

        [OperationContract]
        IEnumerable<Cartelera> GetPeliculasHorario(DateTime _Dia, string _peliculaname);

    }

}
