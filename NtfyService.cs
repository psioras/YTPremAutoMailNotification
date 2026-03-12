using System.Text;

public class NtfyService
{
  public static async Task SendNotificationAsync(string message, string topic)
  {
    string url = $"https://ntfy.sh/{topic}";

    var httpClient = new HttpClient();

    httpClient.DefaultRequestHeaders.Add("Title", "YT_Premium_Mail_Notifier");
    httpClient.DefaultRequestHeaders.Add("Priority", "high");
    httpClient.DefaultRequestHeaders.Add("Tags", "email,notification");

    var content = new StringContent(message, Encoding.UTF8, "text/plain");
    var response = await httpClient.PostAsync(url, content);
    response.EnsureSuccessStatusCode();
  }
}

