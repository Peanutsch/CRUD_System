using System;
using System.Net;
using System.Net.Mail;

/// <summary>
/// Manages sending emails using SMTP.
/// </summary>
public class EmailManager // Maybe Brevo?
{
    /// <summary>
    /// Sends an email to the specified recipient with the given subject and body.
    /// Allows sending plain text or HTML-formatted emails.
    /// </summary>
    /// <param name="recipientEmail">The recipient's email address.</param>
    /// <param name="subject">The subject line of the email.</param>
    /// <param name="body">The content of the email body.</param>
    /// <param name="isHtml">Determines if the email body is HTML-formatted. Defaults to false.</param>
    public void SendEmail(string recipientEmail, string subject, string body, bool isHtml = false)
    {
        try
        {
            // Create a new MailMessage object to construct the email
            MailMessage mail = new MailMessage();

            // Set the sender's email address
            mail.From = new MailAddress("your-email@example.com");

            // Add the recipient's email address
            mail.To.Add(recipientEmail);

            // Set the email's subject and body
            mail.Subject = subject;
            mail.Body = body;

            // Specify whether the body content is HTML
            mail.IsBodyHtml = isHtml;

            // Configure the SMTP client for sending the email
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587, // SMTP port for Gmail
                Credentials = new NetworkCredential("your-email@example.com", "your-password"), // Authentication credentials
                EnableSsl = true // Enable SSL for a secure connection
            };

            // Send the email
            smtpClient.Send(mail);
            Console.WriteLine("HTML email successfully sent.");
        }
        catch (Exception ex)
        {
            // Catch and log any errors during the email-sending process
            Console.WriteLine($"Error while sending email: {ex.Message}");
        }
    }
}
