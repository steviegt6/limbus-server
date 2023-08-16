using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models;

public class LobbyCgDetail {
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("g")]
    public int G { get; set; }
}
