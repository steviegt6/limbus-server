using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models.Packets;

public class GetMailLogAllResultPacket {
    [JsonProperty("mailLogs")]
    public List<MailLog> MailLogs { get; set; }
}
