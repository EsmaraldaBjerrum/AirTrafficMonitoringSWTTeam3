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
    
      public void PrintOnScreen(string s)
      {
         Console.WriteLine(s);
      }

      public void PrintSeparationToFile(string s)
      {
         
            using (StreamWriter fileWriter =
               new FileInfo(
                     "SeparationLog.txt")
                  .AppendText())
            {
               fileWriter.WriteLine(s);
            }
         
      }

     
      public void PrintSeparationOnScreen(string s)
      {
        Console.WriteLine(s);
      }
   }
}
