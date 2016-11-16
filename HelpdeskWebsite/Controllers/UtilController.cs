using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HelpdeskViewModels;

namespace HelpdeskWebsite.Controllers
{
    public class UtilController : ApiController
    {

        [Route("api/collections")]
        public IHttpActionResult Get()
        {
            try
            {
                ViewModelUtils utils = new ViewModelUtils();
                if(utils.LoadCollections())
                {
                    return Ok("Collections created!");
                }
                else
                {
                    return Ok("Collections not created!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Collections creation failed.");
            }
        }

    }
}
