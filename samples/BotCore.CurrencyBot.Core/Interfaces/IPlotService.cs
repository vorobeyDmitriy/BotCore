using System;
using System.Collections.Generic;

namespace BotCore.Core.CurrencyBot.Interfaces
{
    public interface IPlotService
    {
        string SavePlot<TX, TY>(IEnumerable<TX> dataX, IEnumerable<TY> dataY)
            where TX : struct, IComparable
            where TY : struct, IComparable;

        public void DeletePlot(string path);
    }
}