using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models;

/// <summary>
///     Wraps around a response from the server. All responses are wrapped in
///     this class, as it contains additional information about the response.
/// </summary>
/// <typeparam name="T">The type of the response data.</typeparam>
public sealed class ResponseWrapper<T> {
    /// <summary>
    ///     Information about the server.
    /// </summary>
    [JsonProperty("serverInfo")]
    public ServerInfo ServerInfo { get; set; } = null!; // TODO: y/n: expect not null?

    /// <summary>
    ///     The response state; either "ok" or a number (as a string) error
    ///     code.
    /// </summary>
    [JsonProperty("state")]
    public string State { get; set; } = null!; // TODO: y/n: expect not null?

    /// <summary>
    ///     Contains information that was updated during the request.
    /// </summary>
    [JsonProperty("updated")]
    public Updated? Updated { get; set; }

    /// <summary>
    ///     The response data.
    /// </summary>
    [JsonProperty("result")]
    public T? Result { get; set; }

    [JsonProperty("synchronized")]
    public Synchronized? Synchronized { get; set; }
}
