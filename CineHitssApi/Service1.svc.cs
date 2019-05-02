using System;
using System.Collections.Generic;
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
        public User GetUser(string _Username)
        {
            //READ
            //Creo el objeto que regreso al final
            User _user = new User();
            
            // me permite conectarme al EF solo en lo que dura la funcion
            using (var context = new CineHitssEntities())
            {
               _user = context.Users.First(c => c.Username == _Username);
                
            }

            return _user;
        }
        /*
        public List<objeto> Nombre (string Parametro)
        {
            return List<objeto>;
        }*/
    }
}
