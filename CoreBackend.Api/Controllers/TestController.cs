using CoreBackend.Api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CoreBackend.Api.Controllers
{
    [Route("api/[controller]")]
    public class TestController:Controller
    {
        private MyContext _context;
        public TestController(MyContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("GetTest")]
        public IActionResult Get()
        {
            return Ok("数据");
        }
    }
}
