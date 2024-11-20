using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.Controllers
{
    [Route("time2")]
    public class TimeController : ControllerBase
    {
        [HttpGet]
        public object GetTime()
        {
            //return Content(content: DateTime.UtcNow.ToString("o"), contentType: "text/plain");

            //as JSON
            return new { Time = DateTime.UtcNow };
        }
    }
}
