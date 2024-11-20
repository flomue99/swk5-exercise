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
            return DateTime.UtcNow.ToString("o");
        }
    }
}
