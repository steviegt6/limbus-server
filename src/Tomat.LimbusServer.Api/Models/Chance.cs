using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models;

public class Chance {
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("value")]
    public int Value { get; set; }
}
