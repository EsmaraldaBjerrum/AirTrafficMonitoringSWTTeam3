using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoringSWTTeam3
{
   public class Print
   {
      public event EventHandler<SeparationWarningDataEvent> SeparationWarningDataEvent;

      public void PrintOnScreen(List<Aircraft>WithDataAircrafts)
      {
         foreach (Aircraft track in WithDataAircrafts)
         {
            Console.WriteLine("Tag: "+track.Tag + " Current position: X: " + track.XCoordinate + " meters, Y: " + track.YCoordinate + " meters, Current altitude: " + track.Altitude + " meters, Current horizontal velocity: " + track.HorizontalVelocity + " m/s, Current compass course " + track.CompassCourse + " degress. " );
         }
      }

      public void PrintSeparationToFile(object sender, SeparationWarningDataEvent e)
      {
         foreach (var data in e.TransponderData )
         {
            using (StreamWriter fileWriter =
               new FileInfo(
                     "C:\\Users\\Lenovo\\source\\repos\\EsmaraldaBjerrum\\AirTrafficMonitoringSWTTeam3\\SeparationLog.txt")
                  .AppendText())
            {
               fileWriter.WriteLine("Separation condition between " + data.AircraftTag1 + "and " + data.AircraftTag2 + " at " + data.SeparationTimeStamp);
            }
         }
      }

     
      public void PrintSeparationOnScreen(object sender, SeparationWarningDataEvent e)
      {
         foreach (var data in e.TransponderData)
         {
            
               Console.WriteLine("Separation condition between " + data.AircraftTag1 + "and " + data.AircraftTag2 + " at " + data.SeparationTimeStamp);
            
         }
      }
   }
}
