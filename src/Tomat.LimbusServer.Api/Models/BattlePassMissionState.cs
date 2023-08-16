using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models;

public class BattlePassMissionState {
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("count")]
    public int Count { get; set; }

    [JsonProperty("state")]
    public int State { get; set; }
}
