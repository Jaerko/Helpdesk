using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HelpdeskViewModels;


namespace HelpdeskWebsite.Controllers
{
    public class EmployeeController : ApiController
    {

        [Route("api/employees/{empId}")]
        public IHttpActionResult GetById(string empId)
        {
            try
            {
                EmployeeViewModel emp = new EmployeeViewModel();
                emp.Id = empId;
                emp.GetById();
                return Ok(emp);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve by Id failed - " + ex.Message);
            }
        }


        [Route("api/employees")]
        public IHttpActionResult Get()
        {
            try
            {
                EmployeeViewModel emp = new EmployeeViewModel();
                List<EmployeeViewModel> allEmployees = emp.GetAll();
                return Ok(allEmployees);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve All failed - " + ex.Message);
            }
        }




        [Route("api/employees/")]
        public IHttpActionResult Put(EmployeeViewModel emp)
        {
            try
            {
                int retVal = emp.Update();
                switch (retVal)
                {
                    case 1:
                        return Ok("Employee " + emp.Lastname + " updated!");
                    case -1:
                        return Ok("Employee " + emp.Lastname + " not updated!");
                    case -2:
                        return Ok("Data is stale for " + emp.Lastname + ", Employee not updated!");
                    default:
                        return Ok("Employee " + emp.Lastname + " not updated!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Update failed - " + ex.Message);
            }
        }

        [Route("api/employees")]
        public IHttpActionResult Post(EmployeeViewModel emp)
        {
            try
            {
                emp.Create();
                string msg = emp.Lastname + " has been created";
                return Ok(msg);
            }
            catch (Exception ex)
            {
                return BadRequest("Create failed - " + ex.Message);
            }
        }

        [Route("api/employees")]
        public IHttpActionResult Delete(EmployeeViewModel emp)
        {
            string msg = emp.Lastname + " has been deleted";
            try
            {
                emp.Delete();
                return Ok(msg);
            }
            catch (Exception ex)
            {
                return BadRequest("Delete failed - " + ex.Message);
            }
        }


        [Route("api/employeename/{name}")]
        public IHttpActionResult Get(string name)
        {
            try
            {
                EmployeeViewModel emp = new EmployeeViewModel();
                emp.Lastname = name;
                emp.GetByLastname();
                return Ok(emp);
            }
            catch(Exception ex)
            {
                return BadRequest("Retrieve failed - Contact Tech Support");
            }
        }

    }
}
