using Notification.Core.Model;
using System.Net.Mail;

namespace Notification.Core.Helper
{
    public class MailHelper
    {
        private readonly MailSetting _mailSetting;
        private readonly IList<MailAddress> _toAddresses;
        private readonly IList<MailAddress> _ccAddresses;
        private readonly IList<MailAddress> _bccAddresses;
        private string _subject;
        private string _body;

        private IDictionary<string, object> _variables;
        private MailAddress _from;

        public IList<Attachment> Attachments { get; set; }
        public IList<AttachmentFile> AttachmentFiles { get; set; }

        public MailHelper(MailSetting mailSetting)
        {
            _mailSetting = mailSetting;
            _toAddresses = new List<MailAddress>();
            _ccAddresses = new List<MailAddress>();
            _bccAddresses = new List<MailAddress>();
            _variables = new Dictionary<string, object>();
            AttachmentFiles = new List<AttachmentFile>();
        }

        public MailHelper To(string email, string name = null)
        {
            _toAddresses.Add(new MailAddress(email, name));

            return this;
        }

        public MailHelper Cc(string email, string name = null)
        {
            _ccAddresses.Add(new MailAddress(email, name));

            return this;
        }

        public MailHelper Bcc(string email, string name = null)
        {
            _bccAddresses.Add(new MailAddress(email, name));

            return this;
        }

        public MailHelper Subject(string subject)
        {
            _subject = subject;

            return this;
        }

        public MailHelper Body(string body)
        {
            _body = body;

            return this;
        }

        public MailHelper Variables(IDictionary<string, object> bodyValues)
        {
            _variables = bodyValues;

            return this;
        }

        public MailHelper From(string email, string name = null)
        {
            _from = new MailAddress(email, name);

            return this;
        }

        public string PrepareSubjectWithVariables()
        {
            return !_variables.Any() ? _subject : ReplaceWithVariable(_subject);
        }

        public string PrepareBodyWithVariables()
        {
            return !_variables.Any() ? _body : ReplaceWithVariable(_body);
        }

        private string ReplaceWithVariable(string str)
        {
            return _variables.Aggregate(str,
                (current, value) => current.Replace("{%" + value.Key + "%}", Convert.ToString(value.Value)));
        }

        public IList<ToAddress> ToAddresses => _toAddresses.Select(a => new ToAddress { Email = a.Address, Name = a.DisplayName }).ToList();
        public IList<ToAddress> CcAddresses => _ccAddresses.Select(a => new ToAddress { Email = a.Address, Name = a.DisplayName }).ToList();
        public IList<ToAddress> BccAddresses => _bccAddresses.Select(a => new ToAddress { Email = a.Address, Name = a.DisplayName }).ToList();
        public FromAddress GetFromAddress()
        {
            var from = new FromAddress();
            if (_from != null)
            {
                from.Email = _from.Address;
                from.Name = _from.DisplayName;
            }
            else
            {
                from.Email = _mailSetting.FromEmail;
                from.Name = _mailSetting.FromName;
            }

            return from;
        }
        public IList<AttachmentFile> GetAttachments() => AttachmentFiles;
    }
    public class ToAddress
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class FromAddress
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class AttachmentFile
    {
        public byte[] File { get; set; }
        public string FileName { get; set; }
    }

    public class MailMessageModel
    {
        public MailMessageModel()
        {
            Attachments = new List<AttachmentFile>();
        }
        public FromAddress FromEmail { get; set; }
        public IList<ToAddress> ToEmails { get; set; }
        public IList<ToAddress> CcEmails { get; set; }
        public IList<ToAddress> BccEmails { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public IList<AttachmentFile> Attachments { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
