using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoringSWTTeam3.Controler;
using AirTrafficMonitoringSWTTeam3.Events;
using TransponderReceiver;

namespace AirTrafficMonitoringSWTTeam3
{
   public class Formatting_Tracks
   {
      private ILog _log;
       private IUpdater _updater;
     

      public Formatting_Tracks(IUpdater updater, ILog log)
      {
          _log = log;
          _updater = updater;
          _updater.UpdatedDataEvent += StringToPrintTracksOnScreen;

      }

      public void StringToPrintTracksOnScreen(object sender, UpdatedDataEvent e)
      {
         foreach (Aircraft track in e.UpdatedData)
         {
           string trackToPrint = "Tag: "+track.Tag + " Current position: X: " + track.XCoordinate + " meters, Y: " + track.YCoordinate + " meters, Current altitude: " + track.Altitude + " meters, Current horizontal velocity: " + track.HorizontalVelocity + " m/s, Current compass course " + track.CompassCourse + " degress. " ;
            _log.Log(trackToPrint);
         }
      }

     
   }
}
