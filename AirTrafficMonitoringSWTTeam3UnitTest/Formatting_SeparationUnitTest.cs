using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoringSWTTeam3;
using AirTrafficMonitoringSWTTeam3.Controler;
using AirTrafficMonitoringSWTTeam3.Events;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoringSWTTeam3UnitTest
{
   [TestFixture]
  public class FormattingSeparationUnitTest
   {

      private LogToScreen _fakePrint;
      private Formatting_Separation uut;
      private IUpdater _fakeUpdater;
      private ILog _fakeLoglog;
      //private ILog _fakeLogFile;
      private SeparationInvestigation _fakeSeparationInvestigation;


      [SetUp]
      public void SetUp()
      {
         _fakePrint = Substitute.For<LogToScreen>();
        
         _fakeUpdater = Substitute.For<IUpdater>();
         _fakeLoglog = Substitute.For<LogToLog>();
         _fakeSeparationInvestigation = new SeparationInvestigation(_fakeUpdater);
         uut = new Formatting_Separation(_fakeSeparationInvestigation,_fakePrint,_fakeLoglog);
      }
      [Test]
      public void SeparationOccur_Eventfired_StringReadyToScreen()
      {
         List<SeparationWarningData> fakeList = new List<SeparationWarningData>();
         SeparationWarningData fakeSeparationWarningData = new SeparationWarningData("SKF", "LBS", (DateTime.ParseExact("20191203213426980", "yyyyMMddHHmmssfff",
             System.Globalization.CultureInfo.InvariantCulture)));
         
         fakeList.Add(fakeSeparationWarningData);

         _fakeSeparationInvestigation.SeparationWarningDataEvent += Raise.EventWith(this, new SeparationWarningDataEvent(fakeList));

       _fakePrint.Received().Log(Arg.Is<string>("WARNING! Separation condition between SKF and LBS at 03-12-2019 21:34:26"));

      }

      [Test]
      public void SeparationOccur_Eventfired_StringReadyToFile()
      {
         List<SeparationWarningData> fakeList = new List<SeparationWarningData>();

         SeparationWarningData fakeSeparationWarningData = new SeparationWarningData("SKF", "LBS", (DateTime.ParseExact("20191203213426980", "yyyyMMddHHmmssfff",
            System.Globalization.CultureInfo.InvariantCulture)));
        
         fakeList.Add(fakeSeparationWarningData);

         _fakeSeparationInvestigation.SeparationWarningDataEvent +=
            Raise.EventWith(this,new SeparationWarningDataEvent(fakeList));

         _fakeLoglog.Received().Log(Arg.Is<string>("WARNING! Separation condition between SKF and LBS at 03-12-2019 21:34:26"));
         
      }
   }
}
