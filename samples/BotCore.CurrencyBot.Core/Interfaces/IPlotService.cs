namespace BotCore.Core.CurrencyBot.Interfaces
{
    public interface IPlotService
    {
        string SavePlot();

        public void DeletePlot(string path);
    }
}