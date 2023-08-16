using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models;

public class ServerInfo {
    [JsonProperty("version")]
    public string Version { get; set; }
}
