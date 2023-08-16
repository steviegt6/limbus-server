using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models;

public class FormationDetail {
    [JsonProperty("personalityId")]
    public int PersonalityId { get; set; }

    [JsonProperty("egos")]
    public List<int> Egos { get; set; }

    [JsonProperty("isParticipated")]
    public bool IsParticipated { get; set; }

    [JsonProperty("participationOrder")]
    public int ParticipationOrder { get; set; }
}
