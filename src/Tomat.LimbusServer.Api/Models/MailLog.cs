﻿using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models;

public class MailLog {
    [JsonProperty("maillog_id")]
    public int MailLodId { get; set; }

    [JsonProperty("sent_date")]
    public string SentDate { get; set; }

    [JsonProperty("content_id")]
    public int ContentId { get; set; }

    [JsonProperty("attachments")]
    public List<Element> Attachments { get; set; }

    [JsonProperty("unsealed_date")]
    public string UnsealedDate { get; set; }

    [JsonProperty("parameters")]
    public List<string> Parameters { get; set; }
}
