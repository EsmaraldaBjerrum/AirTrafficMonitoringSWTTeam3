using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace AirTrafficMonitoringSWTTeam3
{
   public class Formatting_Tracks
   {
      private ILog _log;
     

      public Formatting_Tracks()
      {
         _log = new LogToScreen();
    
      }

      public void StringToPrintTracksOnScreen(List<Aircraft> WithDataAircrafts)
      {
         foreach (Aircraft track in WithDataAircrafts)
         {
           string trackToPrint = "Tag: "+track.Tag + " Current position: X: " + track.XCoordinate + " meters, Y: " + track.YCoordinate + " meters, Current altitude: " + track.Altitude + " meters, Current horizontal velocity: " + track.HorizontalVelocity + " m/s, Current compass course " + track.CompassCourse + " degress. " ;
            _log.Log(trackToPrint);
         }
      }

     
   }
}
