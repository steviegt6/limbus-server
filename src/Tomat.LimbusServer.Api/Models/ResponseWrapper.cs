using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models;

public class ResponseWrapper<T> {
    [JsonProperty("serverInfo")]
    public ServerInfo ServerInfo { get; set; }

    [JsonProperty("state")]
    public string State { get; set; }

    [JsonProperty("updated")]
    public Updated Updated { get; }

    [JsonProperty("result")]
    public T Result { get; set; }

    [JsonProperty("synchronized")]
    public Synchronized Synchronized { get; }
}
