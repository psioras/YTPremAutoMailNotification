using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Logging;

public class EmailService
{
  private readonly EmailSettings _settings;
  private readonly ILogger<EmailService> _logger;

  public EmailService(EmailSettings settings, ILogger<EmailService> logger)
  {
    _settings = settings;
    _logger = logger;
  }

  public async Task SendEmailAsync(string subject, string htmlBody, CancellationToken stoppingToken = default)
  {
    var message = new MimeMessage();
    message.From.Add(new MailboxAddress("psioras", _settings.User));
    message.Subject = subject;

    foreach (var recipient in _settings.Recipients)
      message.To.Add(new MailboxAddress("", recipient));

    message.Body = new TextPart("html") { Text = htmlBody };

    using var client = new SmtpClient();
    try
    {
      stoppingToken.ThrowIfCancellationRequested();

      _logger.LogDebug("Connecting to {Host}:{Port}...", _settings.Host, _settings.Port);
      await client.ConnectAsync(_settings.Host, _settings.Port, MailKit.Security.SecureSocketOptions.StartTls, stoppingToken);

      await client.AuthenticateAsync(_settings.User, _settings.Pass, stoppingToken);
      _logger.LogInformation("Authenticated successfully as {User}.", _settings.User);

      await client.SendAsync(message, stoppingToken);
      _logger.LogInformation("Email sent successfully to all recipients.");
    }
    catch (OperationCanceledException)
    {
      _logger.LogWarning("Email sending was cancelled.");
      throw;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Failed to send email.");
      throw;
    }
    finally
    {
      await client.DisconnectAsync(true, stoppingToken);
    }
  }
}
