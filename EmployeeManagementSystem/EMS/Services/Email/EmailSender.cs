using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using SendGrid;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace EMS.Services.Email;

public class EmailSender : IEmailSender
{
	public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
	{
		Options = optionsAccessor.Value;
	}

	//Set with Secret Manager
	public AuthMessageSenderOptions Options { get; }

	public async Task SendEmailAsync(string toEmail, string subject, string message)
	{
		if (string.IsNullOrEmpty(Options.SendGridKey))
		{
			throw new Exception("Null SendGridKey");
		}
		await Execute(Options.SendGridKey, subject, message, toEmail);
	}

	public async Task Execute(string apiKey, string subject, string message, string toEmail)
	{
		var client = new SendGridClient(apiKey);
		var msg = new SendGridMessage()
		{
			From = new EmailAddress("coding.email7125@gmail.com", "Gabe"),
			Subject = subject,
			PlainTextContent = message,
			HtmlContent = message
		};
		msg.AddTo(new EmailAddress(toEmail));

		msg.SetClickTracking(false, false);
		await client.SendEmailAsync(msg);
	}
}
