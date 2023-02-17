using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice.DB
{
    public static class DataAccess
    {
        public static List<Client> GetClients()
        {
            return db.connection.Client.ToList();
        }
        public static List<Gender> GetGenders()
        {
            return db.connection.Gender.ToList();
        }
        public static List<Service> GetServices()
        {
            return db.connection.Service.ToList();
        }
        public static List<ClientService> GetPerformServices()
        {
            return db.connection.ClientService.ToList();
        }
        public static void AddClient(Client client)
        {
            db.connection.Client.Add(client);
            db.connection.SaveChanges();
        }
        public static void AddService(Service service)
        {
            db.connection.Service.Add(service);
            db.connection.SaveChanges();
        }
        public static void AddPerformService(ClientService performService)
        {
            db.connection.ClientService.Add(performService);
            db.connection.SaveChanges();
        }
    }
}
