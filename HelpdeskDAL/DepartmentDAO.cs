using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpdeskDAL
{
    public class DepartmentDAO
    {



        private IRepository repo;

        public DepartmentDAO()
        {
            repo = new HelpdeskRepository();
        }

        //GetByLastname
        public Department GetByDepartmentName(string deptName)
        {
            Department dept = null;
            var builder = Builders<Department>.Filter;
            var filter = builder.Eq("DepartmentName", deptName);

            try
            {
                dept = repo.GetOne<Department>(filter);
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "DepartmentDAO", "GetByLastname");
            }
            return dept;
        }
        // End GetByLastname


        //Update with Repo
        public UpdateStatus Update(Department dept)
        {
            UpdateStatus status = UpdateStatus.Failed;
            try
            {

                var builder = Builders<Department>.Filter;
                var filter = Builders<Department>.Filter.Eq("Id", dept.Id) & Builders<Department>.Filter.Eq("Version", dept.Version);
                var update = Builders<Department>.Update
                    .Set("DepartmentName", dept.DepartmentName)
                    .Inc("Version", 1);
                    

                status = repo.Update(dept.Id.ToString(), filter, update);


            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "DepartmentDAO", "Update");

            }
            return status;
        }
        //End UpdateWithRepo



        //Create - add an Department document to the Departments collection

        public Department Create(Department dept)
        {
            HelpdeskRepository repo = new HelpdeskRepository(new DbContext());
            try
            {
                dept = repo.Create(dept);
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "DepartmentDAO", "Create");
            }
            return dept;
        }

        //Delete - returns a long (1 or 0) depending on if the delete was successful
        public long Delete(string Id)
        {
            long deleteFlag = 0;
            try
            {
                deleteFlag = repo.Delete<Department>(Id);

            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "DepartmentDAO", "Delete");
            }

            return deleteFlag;
        }


        //GetAll - This one is just going to return a straight List of Department instances

        public List<Department> GetAll()
        {


            List<Department> deptList = new List<Department>();


            try
            {
                deptList.AddRange(repo.GetAll<Department>().ToList());
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "DepartmentDAO", "GetAll");
            }
            return deptList;
        }


        //GetByID - Similar to GetByLastname, see above.
        public Department GetById(string Id)
        {
            Department dept = null;
            var builder = Builders<Department>.Filter;

            try
            {
                dept = repo.GetById<Department>(Id);
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "DepartmentDAO", "GetById");
            }
            return dept;
        }




    }
}
