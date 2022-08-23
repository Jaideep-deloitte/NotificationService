namespace Notification.Core.Interface
{
    public interface INotificationService
    {
        Task<string> GetEmailTemplateByTypeAsync(string emailTemplateType);
    }
}
