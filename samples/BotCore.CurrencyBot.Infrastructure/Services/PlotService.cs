using System.IO;
using BotCore.Core.CurrencyBot.Interfaces;

namespace BotCore.CurrencyBot.Infrastructure.Services
{
    public class PlotService : IPlotService
    {
        public string SavePlot()
        {
            var path = "quickstart.png";
            var dataX = new double[] {1, 2, 3, 4, 5};
            var dataY = new double[] {1, 4, 9, 16, 25};
            var plt = new ScottPlot.Plot(400, 300);
            plt.PlotScatter(dataX, dataY);
            plt.SaveFig("quickstart.png");

            return path;
        }
        
        public void DeletePlot(string path)
        {
            var file = new FileInfo(path);
            file.Delete();
        }
    }
}