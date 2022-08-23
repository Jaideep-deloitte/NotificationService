using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Notification.Core.Model;
using Notification.Model;
using Notification.Notifications;

namespace Notification.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly ILogger<NotificationsController> _logger;
        private readonly IOptions<MailSetting> _config;
        private readonly IMemberService _memberService;

        public NotificationsController(ILogger<NotificationsController> logger,
            IOptions<MailSetting> config, IMemberService memberService)
        {
            _logger = logger;
            _config = config;
            _memberService = memberService;
        }

        [HttpPost("welcome")]
        public async Task<IActionResult> Welcome([FromForm] User user)
        {
            var d =_config.Value.UserName;
            _memberService.Create(user);
            //_memberNotification.SendWithPrepareAsync(user);
            return Ok("Success");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] User user)
        {
            var d = _config.Value.UserName;
            _memberService.Login(user);
            //_memberNotification.SendWithPrepareAsync(user);
            return Ok("Success");
        }
    }
}