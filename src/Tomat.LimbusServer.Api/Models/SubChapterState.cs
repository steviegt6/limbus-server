using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models; 

public class SubChapterState {
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("nss")]
    public List<NodeState> Nss { get; set; }

    [JsonProperty("rss")]
    public List<int> Rss { get; set; }
}
