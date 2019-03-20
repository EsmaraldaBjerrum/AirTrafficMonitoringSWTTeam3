using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoringSWTTeam3
{
   public class LogToLog : ILog
   {
      public void Log(string s)
      {
         using (StreamWriter fileWriter =
               new FileInfo(
                     "SeparationLog.txt")
                  .AppendText())
            {
               fileWriter.WriteLine(s);
            }
         
      }
   }
}
