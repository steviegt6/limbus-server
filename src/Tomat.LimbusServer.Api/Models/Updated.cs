using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models;

public sealed class Updated {
    [JsonProperty("isInitialized")]
    public bool IsInitialized { get; set; }

    [JsonProperty("userInfo")]
    public object UserInfo { get; set; } = new(); // TODO

    [JsonProperty("personalityList")]
    public List<object> PersonalityList { get; set; } = new(); // TODO

    [JsonProperty("egoList")]
    public List<object> EgoList { get; set; } = new(); // TODO

    [JsonProperty("formationList")]
    public List<object> FormationList { get; set; } = new(); // TODO

    [JsonProperty("lobbyCG")]
    public object LobbyCg { get; set; } = new(); // TODO

    [JsonProperty("itemList")]
    public List<object> ItemList { get; set; } = new(); // TODO

    [JsonProperty("chanceList")]
    public List<object> ChanceList { get; set; } = new(); // TODO

    [JsonProperty("battlePass")]
    public object BattlePass { get; set; } = new(); // TODO

    [JsonProperty("mainChapterStateList")]
    public List<object> MainChapterStateList { get; set; } = new(); // TODO

    [JsonProperty("mailList")]
    public List<object> MailList { get; set; } = new(); // TODO

    [JsonProperty("announcer")]
    public object Announcer { get; set; } = new(); // TODO

    [JsonProperty("membershipList")]
    public List<object> MembershipList { get; set; } = new(); // TODO

    [JsonProperty("isUpdateUserBanner")]
    public bool IsUpdateUserBanner { get; set; }

    [JsonProperty("isResetMirrorDungeon")]
    public bool IsResetMirrorDungeon { get; set; }
}
