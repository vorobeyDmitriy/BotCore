using System;

namespace BotCore.Core.CurrencyBot.DomainModels
{
    public class CurrencyGain
    {
        public string Abbreviation { get; set; }
        public double Gain { get; set; }
        public decimal LatestRate { get; set; }
        public int Scale { get; set; }
        public DateTime Date { get; set; }
    }
}