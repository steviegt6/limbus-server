using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models; 

/// <summary>
///     Information about the server.
/// </summary>
public sealed class ServerInfo {
    /// <summary>
    ///     The version of the server.
    /// </summary>
    [JsonProperty("version")]
    public string Version { get; set; }
}
