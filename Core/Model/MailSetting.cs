namespace Notification.Core.Model
{
    public class MailSetting
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public bool IsAuthentication { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
