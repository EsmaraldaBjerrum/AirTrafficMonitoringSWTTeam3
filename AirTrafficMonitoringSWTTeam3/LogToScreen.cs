using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoringSWTTeam3
{
   public class LogToScreen : ILog
   {
    
      public void Log(string s)
      {
         Console.WriteLine(s);
      }
   }
}
