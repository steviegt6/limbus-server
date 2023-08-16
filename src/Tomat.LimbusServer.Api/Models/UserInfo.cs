using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models;

public class UserInfo {
    [JsonProperty("uid")]
    public long Uid { get; set; }

    [JsonProperty("level")]
    public int Level { get; set; }

    [JsonProperty("exp")]
    public int Exp { get; set; }

    [JsonProperty("stamina")]
    public int Stamina { get; set; }

    [JsonProperty("last_stamina_recover")]
    public string LastStaminaRecover { get; set; }
}
