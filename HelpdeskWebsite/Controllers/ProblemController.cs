using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HelpdeskViewModels;


namespace HelpdeskWebsite.Controllers
{
    public class ProblemController : ApiController
    {

        [Route("api/problems/{probId}")]
        public IHttpActionResult Get(string probId)
        {
            try
            {
                ProblemViewModel prob = new ProblemViewModel();
                prob.Id = probId;
                prob.GetById();
                return Ok(prob);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }


        [Route("api/problems")]
        public IHttpActionResult Get()
        {
            try
            {
                ProblemViewModel prob = new ProblemViewModel();
                List<ProblemViewModel> allProblems = prob.GetAll();
                return Ok(allProblems);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }


        [Route("api/problems/")]
        public IHttpActionResult Put(ProblemViewModel prob)
        {
            try
            {
                string test = prob.Description;
                int retVal = prob.Update();
                switch (retVal)
                {
                    case 1:
                        return Ok("Problem " + prob.Description + " updated!");
                    case -1:
                        return Ok("Problem " + prob.Description + " not updated!");
                    case -2:
                        return Ok("Data is stale for " + prob.Description + ", Problem not updated!");
                    default:
                        return Ok("Problem " + prob.Description + " not updated!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Update failed - " + ex.Message);
            }
        }

        [Route("api/problems")]
        public IHttpActionResult Post(ProblemViewModel prob)
        {
            try
            {
                prob.Create();
                string msg = prob.Description + " has been created.";
                return Ok(msg);
            }
            catch (Exception ex)
            {
                return BadRequest("Create failed - " + ex.Message);
            }
        }

        [Route("api/problems")]
        public IHttpActionResult Delete(ProblemViewModel prob)
        {
            string msg = prob.Description + " has been deleted";
            try
            {
                prob.Delete();
                return Ok(msg);
            }
            catch (Exception ex)
            {
                return BadRequest("Delete failed - " + ex.Message);
            }
        }
    }
}
