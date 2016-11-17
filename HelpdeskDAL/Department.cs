using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace HelpdeskDAL
{
    //Department Entity Class
    public class Department : HelpdeskEntity
    {
        public string DepartmentName { get; set; }

        public ObjectId ManagerId { get; set; }
    }
}
