using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models;

public class Announcer {
    [JsonProperty("announcer_ids")]
    public List<int> AnnouncerIds { get; set; }

    [JsonProperty("cur_announcer_ids")]
    public List<int> CurAnnouncerIds { get; set; }
}
