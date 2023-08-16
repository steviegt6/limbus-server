using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models;

public class NodeState {
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("ct")]
    public int Ct { get; set; }

    [JsonProperty("cn")]
    public int Cn { get; set; }

    [JsonProperty("dn")]
    public int Dn { get; set; }
}
