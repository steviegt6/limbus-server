using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models;

public class LobbyCg {
    [JsonProperty("characterId")]
    public int CharacterId { get; set; }

    [JsonProperty("lobbycgdetails")]
    public List<LobbyCgDetail> LobbyCgDetails { get; set; }
}
