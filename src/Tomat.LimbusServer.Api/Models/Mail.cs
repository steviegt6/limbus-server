using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models;

public class Mail {
    [JsonProperty("mail_id")]
    public int MailId { get; set; }

    [JsonProperty("sent_date")]
    public string SentDate { get; set; }

    [JsonProperty("expiry_date")]
    public string ExpiryDate { get; set; }

    [JsonProperty("content_id")]
    public int ContentId { get; set; }

    [JsonProperty("attachments")]
    public List<Element> Attachments { get; set; }

    [JsonProperty("parameters")]
    public List<string> Parameters { get; set; }
}
