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
using TransponderReceiver;

namespace AirTrafficMonitoringSWTTeam3UnitTest
{
   [TestFixture]
   public class FormattingUnitTest
   {
     
      private Formatting_Tracks uut;
      private IUpdater _fakeUpdater;
      private ILog _fakeLoglog;
     

      [SetUp]
      public void SetUp()
      {
    
         _fakeUpdater = Substitute.For<IUpdater>();
         _fakeLoglog = Substitute.For<ILog>();
         uut = new Formatting_Tracks(_fakeUpdater, _fakeLoglog);

      }

      [Test]
      public void StringToPrintTracksOnScreen()
      {
         List<Aircraft> fakeList = new List<Aircraft>();
         Aircraft fakeAircraft = new Aircraft("SKF", 21, 8, 1996, DateTime.Now);
         fakeAircraft.CompassCourse = 180;
         fakeAircraft.HorizontalVelocity = 2019;
          
         fakeList.Add(fakeAircraft);
        

         uut.StringToPrintTracksOnScreen(this, new UpdatedDataEvent(fakeList));

         _fakeLoglog.Received()
            .Log(("Tag: SKF Current position: X: 21 meters, Y: 8 meters, Current altitude: 1996 meters, Current horizontal velocity: 2019 m/s, Current compass course 180 degress. "));

      }
      
   }
}
