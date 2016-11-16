using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HelpdeskDAL
{
    public class EmployeeDAO
    {
        private IRepository repo;

        public EmployeeDAO()
        {
            repo = new HelpdeskRepository();
        }

        //GetByLastname
        public Employee GetByLastname (string name)
        {
            Employee emp = null;
            var builder = Builders<Employee>.Filter;
            var filter = builder.Eq("Lastname", name);
            
            try
            {
                emp = repo.GetOne<Employee>(filter);
            }
            catch(Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "EmployeeDAO", "GetByLastname");
            }
            return emp;
        }
        // End GetByLastname


        //Update with Repo
        public UpdateStatus Update(Employee emp)
        {
            UpdateStatus status = UpdateStatus.Failed;
            HelpdeskRepository repo = new HelpdeskRepository(new DbContext());
            try
            {
                
                var builder = Builders<Employee>.Filter;
                var filter = Builders<Employee>.Filter.Eq("Id", emp.Id) & Builders<Employee>.Filter.Eq("Version", emp.Version);
                var update = Builders<Employee>.Update
                    .Set("DepartmentId", emp.DepartmentId)
                    .Set("Email", emp.Email)
                    .Set("Firstname", emp.Firstname)
                    .Set("Lastname", emp.Lastname)
                    .Set("Phoneno", emp.Phoneno)
                    .Set("Title", emp.Title)
                    .Inc("Version", 1);
                
                status = repo.Update(emp.Id.ToString(), filter, update);

                
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "EmployeeDAO", "UpdateWithRepo");

            }
            return status;
        }
        //End UpdateWithRepo



        //Create - add an Employee document to the employees collection                public Employee Create(Employee emp)
        {
            HelpdeskRepository repo = new HelpdeskRepository(new DbContext());
            try
            {
                emp = repo.Create(emp);
            }
            catch(Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "EmployeeDAO", "Create");
            }
            return emp;
        }

        //Delete - returns a long (1 or 0) depending on if the delete was successful
         public long Delete(string Id)
        {
            long deleteFlag = 0;
            try
            {
                deleteFlag = repo.Delete<Employee>(Id);
                
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "EmployeeDAO", "Delete");
            }

            return deleteFlag;
        }


        //GetAll - This one is just going to return a straight List of Employee instances

        public List<Employee> GetAll()
        {


            List<Employee> empList = new List<Employee>();
            

            try
            {
                empList.AddRange(repo.GetAll<Employee>().ToList());
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "EmployeeDAO", "GetAll");
            }
            return empList;
        }


        //GetByID - Similar to GetByLastname, see above.
        public Employee GetById(string Id)
        {
            Employee emp = null;
            var builder = Builders<Employee>.Filter;

            try
            {
                emp = repo.GetById<Employee>(Id);
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "EmployeeDAO", "GetById");
            }
            return emp;
        }


    }    //End class
}
//End namespace use

