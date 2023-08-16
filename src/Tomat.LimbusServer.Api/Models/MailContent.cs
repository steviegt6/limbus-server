using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models;

public class MailContent {
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("version")]
    public int Version { get; set; }

    [JsonProperty("senderSprName")]
    public string SenderSprName { get; set; }

    [JsonProperty("content_KR")]
    public string ContentKr { get; set; }

    [JsonProperty("sender_KR")]
    public string SenderKr { get; set; }

    [JsonProperty("content_EN")]
    public string ContentEn { get; set; }

    [JsonProperty("sender_EN")]
    public string SenderEn { get; set; }

    [JsonProperty("content_JP")]
    public string ContentJp { get; set; }

    [JsonProperty("sender_JP")]
    public string SenderJp { get; set; }
}
