using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models.Packets; 

public sealed class GetMailLogAll {
    [JsonProperty("mailLogs")]
    public List<MailLog> MailLogs { get; set; } = new();
}
