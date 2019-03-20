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
      private Print uut;

      [SetUp]
      public void SetUp()
      {
         uut = new Print();
      }

      public void PrintSeparationToFile_Succes()
      {
         string test = "Esmaralda er en giraf og Louise er en søløve";
         uut.PrintSeparationToFile(test);

         FileStream input = new FileStream("C:\\Users\\Lenovo\\source\\repos\\EsmaraldaBjerrum\\AirTrafficMonitoringSWTTeam3\\SeparationLog.txt",FileMode.Open,FileAccess.Read);
         StreamReader fileReader = new StreamReader(input);

         string FileString = fileReader.ReadLine();

         Assert.That(FileString, Is.EqualTo(test));
      }
   }
}
