using Microsoft.Extensions.Options;
using Notification.Core.Helper;
using Notification.Core.Interface;
using Notification.Core.Model;
using Notification.Core.Notifications;
using Notification.Model;

namespace Notification.Notifications
{
    public class MemberLoginNotification : EmailNotification
    {
        private readonly INotificationService _notificationService;
        private readonly IOptions<MailSetting> _mailSetting;
        public MemberLoginNotification(INotificationService notificationService,
            IOptions<MailSetting> mailSetting) : base(notificationService, mailSetting)
        {
            _notificationService = notificationService;
            _mailSetting = mailSetting; 
        }

        public async Task<MemberLoginNotification> PrepareAsync(User user)
        {
            emailTemaplte = await _notificationService.GetEmailTemplateByTypeAsync(EmailTemplateType.Login);
            //_mailHelper.From("Jaideep.Tanwar@outlook.com",user.Name);
            _mailHelper.To(user.ToEmail, user.Name);
            //_mailHelper.To("jaideep.newvision@gmail.com", user.Name);
            _mailHelper.Subject(user.Subject);

            Variables.Add("Name", user.Name);
            Variables.Add("Email", user.ToEmail);


            //MailHelper.To
            return this;
        }
        public async Task<string> SendWithPrepareAsync(User user)
        {
            try
            {
                var result = await PrepareAsync(user);
                await result.SendAsync();
                return "Succes";
            }
            catch (Exception ex)
            {
                return "Error";
            }
        }

    }
}
