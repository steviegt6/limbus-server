using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models;

public class Gacha {
    [JsonProperty("gachaId")]
    public int GachaId { get; set; }

    [JsonProperty("pityPoint")]
    public int PityPoint { get; set; }
}
