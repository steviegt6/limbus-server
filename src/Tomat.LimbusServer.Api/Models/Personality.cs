using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models;

public class Personality {
    [JsonProperty("personality_id")]
    public int PersonalityId { get; set; }

    [JsonProperty("level")]
    public int Level { get; set; }

    [JsonProperty("exp")]
    public int Exp { get; set; }

    // uptie
    [JsonProperty("gacksung")]
    public int Gacksung { get; set; }

    [JsonProperty("order_id")]
    public int OrderId { get; set; }

    [JsonProperty("hacksung_illust_type")]
    public int GacksungIllustType { get; set; }

    [JsonProperty("acquire_time")]
    public string AcquireTime { get; set; }
}
