using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoringSWTTeam3;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AirTrafficMonitoringSWTTeam3UnitTest
{
   [TestFixture]
   public class PrintUnitTest
   {
      private LogToScreen uut;

      [SetUp]
      public void SetUp()
      {
         uut = new LogToScreen();
      }

      public void PrintSeparationToFile_Succes()
      {
         string test = "Esmaralda er en giraf og Louise er en søløve";
         uut.Log(test);

         FileStream input = new FileStream("SeparationLog.txt",FileMode.Open,FileAccess.Read);
         StreamReader fileReader = new StreamReader(input);

         string FileString = fileReader.ReadLine();

         Assert.That(FileString, Is.EqualTo(test));
      }
   }
}
