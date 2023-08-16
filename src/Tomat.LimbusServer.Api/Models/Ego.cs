using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models;

public class Ego {
    [JsonProperty("ego_id")]
    public int EgoId { get; set; }

    [JsonProperty("gacksung")]
    public int Gacksung { get; set; }

    [JsonProperty("acquire_time")]
    public string AcquireTime { get; set; }
}
