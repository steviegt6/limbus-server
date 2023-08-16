using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models;

public class Membership {
    [JsonProperty("iap_id")]
    public int IapId { get; set; }

    [JsonProperty("expiry_date")]
    public string ExpiryDate { get; set; }
}
