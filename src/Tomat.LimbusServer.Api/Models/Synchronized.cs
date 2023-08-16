using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models;

public class Synchronized {
    [JsonProperty("version")]
    public int Version { get; set; }

    [JsonProperty("noticeList")]
    public List<Notice> NoticeList { get; set; }

    [JsonProperty("mailContentList")]
    public List<MailContent> MailContentList { get; set; }
}
