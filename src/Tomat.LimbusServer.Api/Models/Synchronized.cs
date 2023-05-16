using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models;

public sealed class Synchronized {
    [JsonProperty("version")]
    public int Version { get; set; }

    [JsonProperty("noticeList")]
    public List<object> NoticeList { get; set; } = new(); // TODO

    [JsonProperty("mailContentList")]
    public List<object> MailContentList { get; set; } = new(); // TODO

    [JsonProperty("priceTierList")]
    public List<object> PriceTierList { get; set; } = new(); // TODO
}
