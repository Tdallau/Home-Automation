using System;
using System.Text.Json.Serialization;

namespace HomeAutomation.Areas.TwitterStayInformd.Modals
{
  public class TweetResponse
  {
    [JsonPropertyName("id_str")]
    public string Id { get; set; }
    [JsonPropertyName("created_at")]
    public string CreatedAt { get; set; }
    [JsonPropertyName("text")]
    public string Text { get; set; }
  }
}