using Microsoft.AspNetCore.Mvc;
using Notification.Model;

namespace Notification.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly ILogger<NotificationsController> _logger;

        public NotificationsController(ILogger<NotificationsController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] User user)
        {          
           
            return Ok("Success");
        }
    }
}