using ParaTakip.Business.Interfaces;
using ParaTakip.Core;
using ParaTakip.Entities;
using System.Net;
using System.Net.Mail;

namespace ParaTakip.Business.Helpers
{
    public class EmailSender
    {
        protected string From { get; set; }
        protected string To { get; set; }
        protected string Subject { get; set; }
        protected string Body { get; set; }
        protected string Username { get; set; }
        protected string Password { get; set; }
        protected string Host { get; set; }
        protected int Port { get; set; }

        protected Attachment? Attachment { get; set; }

        protected SmtpClient SmtpClient;
        protected MailMessage MailMessage;
        private Dictionary<string, string> Values { get; set; }
        private string? TemplateName { get; set; }

        public EmailSender(Dictionary<string, string> values, string? templateName)
        {

            Values = values;
            TemplateName = templateName;
        }

        public void Initialize()
        {
            ValidateParameters();

            SmtpClient = new SmtpClient(Host, Port)
            {
                Credentials = new NetworkCredential(Username, Password),
                EnableSsl = true,
            };
        }

        public void Send()
        {
            EmailTemplate template = GetTemplate();
            string emailContent = GetTemplateContent(template);

            MailMessage = new MailMessage
            {
                Subject = Subject,
                From = new MailAddress(From),
                Body = emailContent,
                IsBodyHtml = true                
            };

            if (Attachment != null)
            {
                MailMessage.Attachments.Add(Attachment);
            }

            MailMessage.To.Add(new MailAddress(To));

            SmtpClient.Send(MailMessage);
        }


        private EmailTemplate GetTemplate()
        {
            var template = AppServiceProvider.Instance.Get<IEmailTemplateService>().FilterBy(x => x.Name == TemplateName).FirstOrDefault();
            if (template == null)
            {
                throw new AppException(ReturnMessages.EMAIL_TEMLATE_NOT_FOUND, TemplateName ?? "");
            }

            return template;
        }

        private string GetTemplateContent(EmailTemplate template)
        {
            string result = template.Content;
            foreach (var item in Values)
            {
                result = result.Replace(item.Key, item.Value);
            }

            return result;
        }

        private void ValidateParameters()
        {
            if (string.IsNullOrWhiteSpace(Host))
                throw new AppException(ReturnMessages.EMAIL_PROVIDER_HOST_NOT_FOUND);

            if (string.IsNullOrWhiteSpace(Username))
                throw new AppException(ReturnMessages.EMAIL_PROVIDER_USERNAME_NOT_FOUND);

            if (string.IsNullOrWhiteSpace(Password))
                throw new AppException(ReturnMessages.EMAIL_PROVIDER_PASSWORD_NOT_FOUND);

            if (Port==0)
                throw new AppException(ReturnMessages.EMAIL_PROVIDER_PORT_NOT_FOUND);

            if (string.IsNullOrWhiteSpace(Subject))
                throw new AppException(ReturnMessages.EMAIL_PROVIDER_SUBJECT_NOT_FOUND);

            if (string.IsNullOrWhiteSpace(From))
                throw new AppException(ReturnMessages.EMAIL_PROVIDER_FROM_NOT_FOUND);

            if (string.IsNullOrWhiteSpace(To))
                throw new AppException(ReturnMessages.EMAIL_PROVIDER_TO_NOT_FOUND);
        }
    }
}
