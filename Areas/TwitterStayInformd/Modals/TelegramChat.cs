using System.Text.Json.Serialization;

namespace HomeAutomation.Areas.TwitterStayInformd.Modals
{
  public class TelegramChat
  {
    [JsonPropertyName("chat_id")]
    public string ChatId { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; }
  }
}