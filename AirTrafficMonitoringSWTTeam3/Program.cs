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
            IConverter _converter = new Converter(receiver);
            IFilter _filter = new Filter(_converter);
            IUpdated _updater = new Updated(_filter);

            SeparationInvestigation _separationInvestigation = new SeparationInvestigation(_updater);
            

            while (true)
            {
                Thread.Sleep(100);

            }
        }
    }
}
