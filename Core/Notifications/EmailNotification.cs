using Microsoft.Extensions.Options;
using Notification.Core.Helper;
using Notification.Core.Interface;
using Notification.Core.Model;
using Notification.Model;
using System.Net;
using System.Net.Mail;

namespace Notification.Core.Notifications
{
    public class EmailNotification
    {
        private readonly INotificationService _notificationService;
        protected MailHelper _mailHelper;
        protected string emailTemaplte;
        protected Dictionary<string, object> Variables = new Dictionary<string, object>();
        private readonly IOptions<MailSetting> _mailSetting;
        public EmailNotification(INotificationService notificationService, IOptions<MailSetting> mailSetting)
        {
            _notificationService = notificationService;
            _mailHelper = new MailHelper(mailSetting.Value);
            _mailSetting = mailSetting;
        }

        public async Task SendAsync()
        {
            await PrepareMailHelperForSendAsync();
            var mailModel = GetMailMessageModel();
            SendMail(mailModel);
        }

        public async Task SendMail(MailMessageModel model)
        {
            try
            {
                MailMessage message = new MailMessage();

                message.From = new MailAddress(model.FromEmail.Email);
                foreach (var to in model.ToEmails)
                {
                    message.To.Add(new MailAddress(to.Email, to.Name));
                }
                foreach (var bcc in model.BccEmails)
                {
                    message.Bcc.Add(new MailAddress(bcc.Email, bcc.Name));
                }
                foreach (var cc in model.CcEmails)
                {
                    message.CC.Add(new MailAddress(cc.Email, cc.Name));
                }
                
                message.Subject = model.Subject;
                message.Body = model.Body;
                message.IsBodyHtml = true;

                var smtpClient = new SmtpClient(_mailSetting.Value.Host, _mailSetting.Value.Port)
                {
                    EnableSsl = _mailSetting.Value.EnableSsl
                };

                if (_mailSetting.Value.IsAuthentication)
                {
                    //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(_mailSetting.Value.UserName, _mailSetting.Value.Password);
                }

                smtpClient.Send(message);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private MailMessageModel GetMailMessageModel()
        {
            MailMessageModel model = new MailMessageModel
            {
                Subject = _mailHelper.PrepareSubjectWithVariables(),
                Body = _mailHelper.PrepareBodyWithVariables(),  
                ToEmails = _mailHelper.ToAddresses,
                FromEmail =_mailHelper.GetFromAddress(),
                CcEmails = _mailHelper.CcAddresses,
                BccEmails = _mailHelper.BccAddresses,
                Host = _mailSetting.Value.Host,
                Port = _mailSetting.Value.Port,
                Username = _mailSetting.Value.UserName,
                Password = _mailSetting.Value.Password
            };

            return model;            
        }

        private async Task PrepareMailHelperForSendAsync()
        {
            //Write a code for Layout i.e. admin or user layout
            //var template = await _notificationService.GetEmailTemplateTypeAsync(EmailTemplateType.Welcome);

            //_mailHelper.Subject("Subjected");
            _mailHelper.Body(emailTemaplte);
            _mailHelper.Variables(Variables);
        }
    }
}
