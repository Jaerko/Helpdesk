using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace HelpdeskDAL
{
    //DbContext - abstraction of connection, database and entity collections
    public class DbContext
    {
        public IMongoDatabase Db;

        public DbContext()
        {
            MongoClient client = new MongoClient(); //localhost
            Db = client.GetDatabase("HelpdeskDB"); 
        }

        public IMongoCollection<HelpdeskEntity> GetCollection<HelpdeskEntity>()
        {
            return Db.GetCollection<HelpdeskEntity>(typeof(HelpdeskEntity).Name.ToLower() + "s");
        }

        public IMongoCollection<Employee> Employees
        {
            get
            {
                return this.Db.GetCollection<Employee>("employees");
            }

        }

    }
}
