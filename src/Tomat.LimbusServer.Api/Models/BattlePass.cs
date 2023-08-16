using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models;

public class BattlePass {
    [JsonProperty("is_limbus")]
    public bool IsLimbus { get; set; }

    [JsonProperty("level")]
    public int Level { get; set; }

    [JsonProperty("exp")]
    public int Exp { get; set; }

    [JsonProperty("today_rand_value")]
    public int TodayRandValue { get; set; }

    [JsonProperty("ex_reward_level")]
    public int ExRewardLevel { get; set; }

    [JsonProperty("limbus_apply_level")]
    public int LimbusApplyLevel { get; set; }

    [JsonProperty("rewards_state")]
    public List<int> RewardsState { get; set; }

    [JsonProperty("missions_state")]
    public List<BattlePassMissionState> MissionsState { get; set; }
}
