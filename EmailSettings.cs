// EmailSettings.cs
public class EmailSettings
{
  public string Host { get; init; }
  public int Port { get; init; }
  public string User { get; init; }
  public string Pass { get; init; }
  public List<string> Recipients { get; init; }

  public EmailSettings()
  {
    Host = Environment.GetEnvironmentVariable("SMTP_SERVER") ?? "smtp.gmail.com";
    Port = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT") ?? "587");
    User = Environment.GetEnvironmentVariable("SMTP_USER") ?? "";
    Pass = Environment.GetEnvironmentVariable("SMTP_PASS") ?? "";

    var rawRecipients = Environment.GetEnvironmentVariable("SMTP_RECIPIENTS") ?? "";
    Recipients = rawRecipients.Split(';', StringSplitOptions.RemoveEmptyEntries)
                              .Select(r => r.Trim())
                              .ToList();
  }
}
