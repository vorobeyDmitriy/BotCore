using Newtonsoft.Json;

namespace BotCore.Viber.DomainModels
{
    public class WebhookModel
    {
        [JsonProperty("url")] public string Url { get; set; }

        [JsonProperty("send_name")] public bool SendName { get; set; }

        [JsonProperty("send_photo")] public bool SendPhoto { get; set; }
    }
}