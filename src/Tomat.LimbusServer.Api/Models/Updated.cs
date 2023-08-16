using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models;

public class Updated {
    [JsonProperty("isInitialized")]
    public bool IsInitialized { get; set; }

    [JsonProperty("userInfo")]
    public UserInfo UserInfo { get; set; }

    [JsonProperty("personalityList")]
    public List<Personality> PersonalityList { get; set; }

    [JsonProperty("egoList")]
    public List<Ego> EgoList { get; set; }

    [JsonProperty("formationList")]
    public List<Formation> FormationList { get; set; }

    [JsonProperty("lobbyCG")]
    public LobbyCg LobbyCg { get; set; }

    [JsonProperty("chanceList")]
    public List<Chance> ChanceList { get; set; }

    [JsonProperty("battlePass")]
    public BattlePass BattlePass { get; set; }

    [JsonProperty("mainChapterStateList")]
    public List<MainChapterState> MainChapterStateList { get; set; }

    [JsonProperty("mailList")]
    public List<Mail> MailList { get; set; }

    [JsonProperty("announcer")]
    public Announcer Announcer { get; set; }

    [JsonProperty("membershipList")]
    public List<Membership> MembershipList { get; set; }

    [JsonProperty("gachaList")]
    public List<Gacha> GachaList { get; set; }

    [JsonProperty("userUnlockCodeList")]
    public List<UserUnlockCode> UserUnlockCodeList { get; set; }

    [JsonProperty("eventRewardStateList")]
    public List<EventRewardState> EventRewardStateList { get; set; }

    [JsonProperty("isUpdateUserBanner")]
    public bool IsUpdatedUserBanner { get; set; }

    [JsonProperty("isResetMirrorDungeon")]
    public bool IsResetMirrorDungeon { get; set; }
}
