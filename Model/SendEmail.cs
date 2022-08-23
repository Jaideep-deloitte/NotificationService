//using Notification.Core;

//namespace Notification.Model
//{
//    public static class SendEmail
//    {
//        public static void SendEmail(string smtpServer)
//        {
//            //Send teh High priority Email  
//            EmailManager mailMan = new EmailManager(smtpServer);

//            EmailSendConfigure myConfig = new EmailSendConfigure();
//            // replace with your email userName  
//            myConfig.ClientCredentialUserName = "abc@outlook.com";
//            // replace with your email account password
//            myConfig.ClientCredentialPassword = "password!";
//            myConfig.TOs = new string[] { "user1@outlook.com" };
//            myConfig.CCs = new string[] { };
//            myConfig.From = "<YOUR_ACCOUNT>@outlook.com";
//            myConfig.FromDisplayName = "<YOUR_NAME>";
//            myConfig.Priority = System.Net.Mail.MailPriority.Normal;
//            myConfig.Subject = "WebSite was down - please investigate";

//            EmailContent myContent = new EmailContent();
//            myContent.Content = "The following URLs were down - 1. Foo, 2. bar";

//            mailMan.SendMail(myConfig, myContent);
//        }

//    }
//}
