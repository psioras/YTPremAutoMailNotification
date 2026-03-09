// Program.cs
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

DotNetEnv.Env.Load();

var services = new ServiceCollection()
    .AddSingleton<EmailSettings>()
    .AddTransient<EmailService>()
    .AddLogging(logging =>
    {
      logging.ClearProviders();
      logging.AddNLog();
      logging.SetMinimumLevel(LogLevel.Debug);
    })
    .BuildServiceProvider();

var emailService = services.GetRequiredService<EmailService>();

await emailService.SendEmailAsync(
    "YouTube Premium Payment Reminder",
    """
    <p>Reminder for the monthly Youtube Premium Payment!</p>
    <p>I accept IRIS &amp; Revolut :)</p>
    <p>-psioras</p>
    """
    );
