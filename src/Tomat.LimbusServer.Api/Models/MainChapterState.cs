using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models;

public class MainChapterState {
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("subcss")]
    public List<SubChapterState> SubCss { get; set; }
}
