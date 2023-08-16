using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models;

public class UserUnlockCode {
    [JsonProperty("unlockcode")]
    public int UnlockCode { get; set; }
}
