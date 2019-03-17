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

      public void PrintOnScreen(List<Aircraft>WithDataAircrafts)
      {
         foreach (Aircraft track in WithDataAircrafts)
         {
            Console.WriteLine("Tag: "+track.Tag + " Current position: X: " + track.XCoordinate + " meters, Y: " + track.YCoordinate + " meters, Current altitude: " + track.Altitude + " meters, Current horizontal velocity: " + track.HorizontalVelocity + " m/s, Current compass course " + track.CompassCourse + " degress. " );
         }
      }

      public void PrintSeparationToFile(List<Aircraft> SeparationList)
      {
         foreach (Aircraft separations in SeparationList)
         {
            using (StreamReader sr =
               new StreamReader(
                  "C:\\Users\\Lenovo\\source\\repos\\EsmaraldaBjerrum\\AirTrafficMonitoringSWTTeam3\\SeparationLog.txt"))
            {
               string content = sr.ReadToEnd();
               if (content.Contains(separations.Tag))
               {

               }
               else
               {
                  using (StreamWriter fileWriter =
                     new FileInfo(
                           "C:\\Users\\Lenovo\\source\\repos\\EsmaraldaBjerrum\\AirTrafficMonitoringSWTTeam3\\SeparationLog.txt")
                        .AppendText())
                  {
                     fileWriter.WriteLine("Separation condition between "+separations.Tag+ "and "+separations.Tag+ " at "+separations.Timestamp);
                  }
               }


            }
         }
      }

      public void PrintSeparationOnScreen(List<Aircraft> SeparationList)
      {
         foreach (Aircraft separations in SeparationList)
         {
            Console.WriteLine("Separation condition between " + separations.Tag + "and " + separations.Tag + " at " + separations.Timestamp);
         }
      }
   }
}
