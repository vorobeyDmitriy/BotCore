using System;
using Newtonsoft.Json;

namespace BotCore.Telegram.Test.DomainModels
{
    public class Currency
    {
        [JsonProperty("Cur_ID")]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [JsonProperty("Cur_Abbreviation")]
        public string Abbreviation { get; set; }
        [JsonProperty("Cur_Scale")]
        public int Scale { get; set; }
        [JsonProperty("Cur_Name")]
        public string Name { get; set; }
        [JsonProperty("Cur_OfficialRate")]
        public double OfficialRate { get; set; }

        public override string ToString()
        {
            return $"{Scale}  {Abbreviation} :  {OfficialRate}  BYN";
        }
    }
}