using Application;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        public ActionResult ApiResponse<TObject>(Result<TObject> result)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));

            if (!result.Success)
            {
                if(result.StatusCode == HttpStatusCode.NotFound)
                    return NotFound(result);

                return BadRequest(result);
            }

            if (result.Data != null)
                return Ok(result);
            
            result.Success = false;
            return NotFound(result);
        }
    }
}
