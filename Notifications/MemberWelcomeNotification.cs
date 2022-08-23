using Microsoft.Extensions.Options;
using Notification.Core.Helper;
using Notification.Core.Interface;
using Notification.Core.Model;
using Notification.Core.Notifications;
using Notification.Model;

namespace Notification.Notifications
{
    public class MemberWelcomeNotification : EmailNotification
    {
        private readonly INotificationService _notificationService;
        private readonly IOptions<MailSetting> _mailSetting;
        public MemberWelcomeNotification(INotificationService notificationService,
            IOptions<MailSetting> mailSetting) : base(notificationService, mailSetting)
        {
            _notificationService = notificationService;
            _mailSetting = mailSetting; 
        }

        public async Task<MemberWelcomeNotification> PrepareAsync(User user)
        {
            emailTemaplte = await _notificationService.GetEmailTemplateByTypeAsync(EmailTemplateType.Welcome);
            //_mailHelper.From("Jaideep.Tanwar@outlook.com", user.Name);
            //Multiple mail send for TO
            _mailHelper.To(user.ToEmail, user.Name);
            _mailHelper.To("jaideep.newvision@gmail.com", user.Name);

            Dictionary<string,string> bcc = new Dictionary<string,string>();
            bcc.Add("Raj@gmail.com", "Raj");
            bcc.Add("Nik@gmail.com", "Nik");

            Dictionary<string, string> cc = new Dictionary<string, string>();
            cc.Add("CC@gmail.com", "CC");
            cc.Add("CC1@gmail.com", "CC1");
            foreach (var _bcc in bcc)
            {
                _mailHelper.Bcc(_bcc.Key, _bcc.Value);
            }
            foreach (var _cc in cc)
            {
                _mailHelper.Cc(_cc.Key, _cc.Value);
            }
            _mailHelper.Subject(user.Subject);
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
