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

            IConverter convert = new Converter(receiver);


        

            while (true)
            {
                Thread.Sleep(100);

            }
        }
    }
}
