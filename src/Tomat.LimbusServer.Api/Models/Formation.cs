using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models;

public class Formation {
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("formationDetails")]
    public List<FormationDetail> FormationDetails { get; set; }
}
