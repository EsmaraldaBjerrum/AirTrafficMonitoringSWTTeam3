using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace AirTrafficMonitoringSWTTeam3
{
    class Program
    {
        static void Main(string[] args)
        {
         ITransponderReceiver receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();

         var system = new Calculator(receiver);

         while (true)
         {
            foreach (Aircraft track in system.currentAircrafts)
            {
               Console.WriteLine(track.Tag, track.XCoordinate, track.YCoordinate);
             }

         }

         Console.ReadKey();
        }
    }
}
