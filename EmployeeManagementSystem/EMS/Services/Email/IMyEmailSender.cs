﻿namespace EMS.Services.Email;

public interface IMyEmailSender
{
  Task SendEmailAsync(string toEmail, string subject, string message);
}
