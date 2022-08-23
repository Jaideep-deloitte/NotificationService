using Notification.Model;

namespace Notification.Notifications
{

    public interface IMemberService
    {
        Task<string> Create(User user);
        Task<string> Login(User user);
    }
    public class MemberService: IMemberService
    {
        private readonly MemberWelcomeNotification _memberNotification;
        private readonly MemberLoginNotification _memberLoginNotification;
        public MemberService(MemberWelcomeNotification memberNotification, MemberLoginNotification memberLoginNotification)
        {
            _memberNotification = memberNotification;
            _memberLoginNotification = memberLoginNotification; 
        }

        public async Task<string> Create(User user)
        {
            try
            {
                _memberNotification.SendWithPrepareAsync(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "Success";
        }

        public async Task<string> Login(User user)
        {
            try
            {
                _memberLoginNotification.SendWithPrepareAsync(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "Success";
        }
    }
}
