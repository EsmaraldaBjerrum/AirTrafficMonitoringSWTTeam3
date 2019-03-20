using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoringSWTTeam3;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace AirTrafficMonitoringSWTTeam3UnitTest
{
   [TestFixture]
  public class FormattingUnitTest
   {
      private Print _fakePrint;
      private SeparationInvestigation _fakeSeparationInvestigation;
      private Calculator _fakeCalculator;
      private ITransponderReceiver _fakeTransponderReceiver;
      private Formatting uut;
     

      [SetUp]
      public void SetUp()
      {
         _fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();
         _fakeCalculator = new Calculator(_fakeTransponderReceiver);
         _fakePrint = Substitute.For<Print>();
         _fakeSeparationInvestigation = Substitute.For<SeparationInvestigation>(_fakeCalculator);
        uut = new Formatting(_fakeSeparationInvestigation);
        
      }

      [Test]
      public void StringToPrintTracksOnScreen()
      {
         List<Aircraft> fakeList = new List<Aircraft>();
         Aircraft fakeAircraft = new Aircraft("SKF", 21, 8, 1996, DateTime.Now);
         fakeAircraft.CompassCourse = 180;
         fakeAircraft.HorizontalVelocity = 2019;
         fakeList.Add(fakeAircraft);
         
       uut.StringToPrintTracksOnScreen(fakeList);

         _fakePrint.Received().PrintOnScreen("Tag: SKF Current position: X: 21 meters, Y: 8 meters, Current altitude: 1996 meters, Current horizontal velocity: 2019 m/s, Current compass course 180 degress");
       
      }

      [Test]
      public void SeparationOccur_Eventfired_StringReadyToFile()
      {
         List<SeparationWarningData> fakeList = new List<SeparationWarningData>();
         SeparationWarningData fakeSeparationWarningData = new SeparationWarningData();
         fakeSeparationWarningData.AircraftTag1 = "SKF";
         fakeSeparationWarningData.AircraftTag2 = "LBS";
         fakeSeparationWarningData.SeparationTimeStamp = (DateTime.ParseExact("20191203213426980", "yyyyMMddHHmmssfff",
            System.Globalization.CultureInfo.InvariantCulture));

         fakeList.Add(fakeSeparationWarningData);

         _fakeSeparationInvestigation.SeparationWarningDataEvent +=
            Raise.EventWith(new SeparationWarningDataEvent(fakeList));

         _fakePrint.Received().PrintSeparationToFile("Separation condition between SKF and LBS at 03-12-2019 21:34:26");

      }

    [Test]
      public void SeparationOccur_Eventfired_StringReadyToScreen()
      {
         List<SeparationWarningData> fakeList = new List<SeparationWarningData>();
         SeparationWarningData fakeSeparationWarningData = new SeparationWarningData();
         fakeSeparationWarningData.AircraftTag1 = "SKF";
         fakeSeparationWarningData.AircraftTag2 = "LBS";
         fakeSeparationWarningData.SeparationTimeStamp = (DateTime.ParseExact("20191203213426980", "yyyyMMddHHmmssfff",
            System.Globalization.CultureInfo.InvariantCulture));

         fakeList.Add(fakeSeparationWarningData);

         _fakeSeparationInvestigation.SeparationWarningDataEvent +=
            Raise.EventWith(new SeparationWarningDataEvent(fakeList));

         _fakePrint.Received().PrintSeparationOnScreen("Separation condition between SKF and LBS at 03-12-2019 21:34:26");

      }

   }
}
