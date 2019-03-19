using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace AirTrafficMonitoringSWTTeam3
{
   public class Formatting
   {
      private Print _print;
      private SeparationInvestigation _separationInvestigation;

      public Formatting(SeparationInvestigation separationInvestigation)
      {
         _print = new Print();
         _separationInvestigation = separationInvestigation;
          _separationInvestigation.SeparationWarningDataEvent += StringToPrintSeparationToScreen;
         _separationInvestigation.SeparationWarningDataEvent += StringToPrintSeparationInFile;
      }

      public void StringToPrintTracksOnScreen(List<Aircraft> WithDataAircrafts)
      {
         foreach (Aircraft track in WithDataAircrafts)
         {
           string trackToPrint = "Tag: "+track.Tag + " Current position: X: " + track.XCoordinate + " meters, Y: " + track.YCoordinate + " meters, Current altitude: " + track.Altitude + " meters, Current horizontal velocity: " + track.HorizontalVelocity + " m/s, Current compass course " + track.CompassCourse + " degress. " ;
            _print.PrintOnScreen(trackToPrint);
         }
      }

      public void StringToPrintSeparationInFile(object sender, SeparationWarningDataEvent e)
      {
         foreach (var data in e.TransponderData)
         {
            string separationToFile = "Separation condition between " + data.AircraftTag1 + "and " + data.AircraftTag2 + " at " + data.SeparationTimeStamp;
            _print.PrintSeparationToFile(separationToFile);
         }
      }

      public void StringToPrintSeparationToScreen(object sender, SeparationWarningDataEvent e)
      {
         foreach (var data in e.TransponderData)
         {
            string separationTiScreen = "Separation condition between " + data.AircraftTag1 + "and " + data.AircraftTag2 + " at " + data.SeparationTimeStamp;
         }

      }
   }
}
