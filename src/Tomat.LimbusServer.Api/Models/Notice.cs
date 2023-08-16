using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models;

public class Notice {
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("version")]
    public int Version { get; set; }

    [JsonProperty("type")]
    public int Type { get; set; }

    [JsonProperty("startDate")]
    public string StartDate { get; set; }

    [JsonProperty("endDate")]
    public string EndDate { get; set; }

    [JsonProperty("sprNameList")]
    public List<string> SprNameList { get; set; }

    [JsonProperty("title_KR")]
    public string TitleKr { get; set; }

    [JsonProperty("content_KR")]
    public string ContentKr { get; set; }

    [JsonProperty("title_EN")]
    public string TitleEn { get; set; }

    [JsonProperty("content_EN")]
    public string ContentEn { get; set; }

    [JsonProperty("title_JP")]
    public string TitleJp { get; set; }

    [JsonProperty("content_JP")]
    public string ContentJp { get; set; }
}
