using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models;

public class RequestWrapper<T> {
    [JsonProperty("userAuth")]
    public RequestUserAuth RequestUserAuth { get; set; }

    [JsonProperty("parameters")]
    public T Parameters { get; set; }
}
