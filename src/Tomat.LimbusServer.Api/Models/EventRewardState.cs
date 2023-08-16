using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models;

public class EventRewardState {
    [JsonProperty("event_id")]
    public int EventId { get; set; }

    [JsonProperty("reward_id")]
    public int RewardId { get; set; }

    [JsonProperty("count")]
    public int Count { get; set; }
}
