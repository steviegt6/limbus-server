using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models;

public sealed class MailLog {
    [JsonProperty("maillog_id")]
    public int MailLogId { get; set; }

    [JsonProperty("sent_date")]
    public string SentDate { get; set; } = string.Empty;

    [JsonProperty("content_id")]
    public int ContentId { get; set; }

    [JsonProperty("attachments")]
    public List<object> Attachments { get; set; } = new();

    [JsonProperty("unsealed_date")]
    public string UnsealedDate { get; set; } = string.Empty;

    [JsonProperty("parameters")]
    public List<string> Parameters { get; set; } = new();
}
