﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoringSWTTeam3
{
   class Formatting_Separation
   {
      private SeparationInvestigation _separationInvestigation;
      private ILog _logFileLog;
      private ILog _logScreenLog;
      public Formatting_Separation(SeparationInvestigation separationInvestigation)
      {
         _separationInvestigation = separationInvestigation;
         _separationInvestigation.SeparationWarningDataEvent += StringToPrintSeparationToScreen;
         _separationInvestigation.SeparationWarningDataEvent += StringToPrintSeparationInFile;
         _logFileLog = new LogToLog();
         _logScreenLog = new LogToScreen();
      }

      public void StringToPrintSeparationInFile(object sender, SeparationWarningDataEvent e)
      {
         foreach (var data in e.TransponderData)
         {
            string separationToFile = "Separation condition between " + data.AircraftTag1 + "and " + data.AircraftTag2 + " at " + data.SeparationTimeStamp;
            _logFileLog.Log(separationToFile);
         }
      }

      public void StringToPrintSeparationToScreen(object sender, SeparationWarningDataEvent e)
      {
         foreach (var data in e.TransponderData)
         {
            string separationTiScreen = "Separation condition between " + data.AircraftTag1 + "and " + data.AircraftTag2 + " at " + data.SeparationTimeStamp;
            _logScreenLog.Log(separationTiScreen);
         }

      }
   }
}
