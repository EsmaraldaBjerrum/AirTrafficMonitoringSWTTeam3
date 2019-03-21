using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AirTrafficMonitoringSWTTeam3.Controler;
using TransponderReceiver;

namespace AirTrafficMonitoringSWTTeam3
{
    class Program
    {
        static void Main(string[] args)
        {
            ITransponderReceiver receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();

            IConverter converter = new Converter(receiver);
            IFilter filter = new Filter(converter);
            IUpdater updater = new Updater(filter);
            SeparationInvestigation separationInvestigation = new SeparationInvestigation(updater);
            
            ILog logToScreen = new LogToScreen();
            ILog logToLog = new LogToLog();
            Formatting_Separation fs = new Formatting_Separation(separationInvestigation, logToScreen, logToLog);
            Formatting_Tracks ft = new Formatting_Tracks(updater, logToScreen);


        

            while (true)
            {
                

            }
        }
    }
}
