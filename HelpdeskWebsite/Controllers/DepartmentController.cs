using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HelpdeskViewModels;


namespace HelpdeskWebsite.Controllers
{
    public class DepartmentController : ApiController
    {

        [Route("api/departments/{deptId}")]
        public IHttpActionResult Get(string deptId)
        {
            try
            {
                DepartmentViewModel dept = new DepartmentViewModel();
                dept.Id = deptId;
                dept.GetById();
                return Ok(dept);
            }
            catch (Exception ex)
            {
                return BadRequest("ID Retrieve failed - " + ex.Message);
            }
        }


        [Route("api/departments")]
        public IHttpActionResult Get()
        {
            try
            {
                DepartmentViewModel dept = new DepartmentViewModel();
                List<DepartmentViewModel> allDepartments = dept.GetAll();
                return Ok(allDepartments);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }


        [Route("api/departments/")]
        public IHttpActionResult Put(DepartmentViewModel dept)
        {
            try
            {
                string test = dept.DepartmentName;
                int retVal = dept.Update();
                switch (retVal)
                {
                    case 1:
                        return Ok("Department " + dept.DepartmentName+ " updated!");
                    case -1:
                        return Ok("Department " + dept.DepartmentName + " not updated!");
                    case -2:
                        return Ok("Data is stale for " + dept.DepartmentName + ", Department not updated!");
                    default:
                        return Ok("Department " + dept.DepartmentName + " not updated!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Update failed - " + ex.Message);
            }
        }

        [Route("api/departments")]
        public IHttpActionResult Post(DepartmentViewModel dept)
        {

            try
            {

                string msg = dept.DepartmentName + " has been created";
                dept.Create();
                return Ok(msg);
            }
            catch (Exception ex)
            {
                return BadRequest("Create failed - " + ex.Message);
            }
        }

        [Route("api/departments")]
        public IHttpActionResult Delete(DepartmentViewModel dept)
        {
            string msg = dept.DepartmentName + " has been deleted";
            try
            {
                dept.Delete();
                return Ok(msg);
            }
            catch (Exception ex)
            {
                return BadRequest("Delete failed - " + ex.Message);
            }
        }

        [Route("api/departmentname/{name}")]
        public IHttpActionResult GetByDepartmentName(string name)
        {

            try
            {
                DepartmentViewModel dept = new DepartmentViewModel();
                dept.DepartmentName = name;
                dept.GetByDepartmentName();
                return Ok(dept);

            }
            catch (Exception ex)
            {
                return BadRequest("Name Retrieve failed - Contact Tech Support");
            }
        }
    }
}
