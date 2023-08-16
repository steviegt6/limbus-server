using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models;

public class RequestUserAuth {
    [JsonProperty("uid")]
    public long Uid { get; set; }

    [JsonProperty("dbid")]
    public int DbId { get; set; }

    [JsonProperty("authCode")]
    public string AuthCode { get; set; }

    [JsonProperty("version")]
    public string Version { get; set; }

    [JsonProperty("synchronousDataVersion")]
    public int SynchronousDataVersion { get; set; }
}
