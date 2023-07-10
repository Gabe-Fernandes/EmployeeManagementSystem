using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace EMS.Services.Email;

public class MyEmailSender : IMyEmailSender
{
  public MyEmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
  {
    Options = optionsAccessor.Value;
  }

  //Set with Secret Manager
  public AuthMessageSenderOptions Options { get; }

  public Task SendEmailAsync(string toEmail, string subject, string message)
  {
		var client = new SmtpClient("smtp-mail.outlook.com", 587)
		{
			EnableSsl = true,
      UseDefaultCredentials = false,
			Credentials = new NetworkCredential(Str.AppEmail, Options.AppEmailPassword)
		};

		return client.SendMailAsync(new MailMessage(Str.AppEmail, to: toEmail, subject, message));
  }
}
