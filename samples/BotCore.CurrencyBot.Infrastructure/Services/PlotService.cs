using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using BotCore.Core.CurrencyBot.Interfaces;

namespace BotCore.CurrencyBot.Infrastructure.Services
{
    public class PlotService : IPlotService
    {
        private const string PlotExtension = ".png";
        public string SavePlot<TX, TY>(IEnumerable<TX> dataX, IEnumerable<TY> dataY)  
            where TX : struct, IComparable
            where TY : struct, IComparable
        {
            var path = DateTime.UtcNow.Ticks.ToString() + RandomNumberGenerator.GetInt32(int.MaxValue) +
                       PlotExtension;
            var plt = new ScottPlot.Plot(600, 600);
            plt.PlotSignalXYConst(dataX.ToArray(), dataY.ToArray());
            plt.Ticks(dateTimeX: true);
            plt.SaveFig(path);

            return path;
        }
        
        public void DeletePlot(string path)
        {
            var file = new FileInfo(path);
            file.Delete();
        }
    }
}