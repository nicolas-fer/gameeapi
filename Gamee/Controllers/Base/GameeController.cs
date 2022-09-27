using Application;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class GameeController : ControllerBase
    {
        public ActionResult GameeResponse<TObject>(Result<TObject> result)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));

            if (!result.Success)
            {
                if(result.StatusCode == HttpStatusCode.NotFound)
                    return NotFound(result);

                return BadRequest(result);
            }

            if (result.Data == null)
            {
                result.Success = false;
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
