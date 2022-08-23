using Notification.Core.Interface;

namespace Notification.Core.Service
{
    public class NotificationService : INotificationService
    {
        public NotificationService()
        {

        }

        public async Task<string> GetEmailTemplateByTypeAsync(string emailTemplateType)
        {
            string emailTemplate = "";
            switch (emailTemplateType)
            {
                case "welcomeemail":
                    emailTemplate = @"<h1>Welcome to NV </h1>";
                    break;
                case "loginemail":
                    emailTemplate = "<h1>Hi {%Name%}, Thank you for login using {%Email%}</h1>";
                    break;
                default:
                    emailTemplate = "<h1>Welcome to NV </h1>";
                    break;


            }
            return emailTemplate;
        }
    }
}
