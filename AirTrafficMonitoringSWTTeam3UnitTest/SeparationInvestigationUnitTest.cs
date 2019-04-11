using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AirTrafficMonitoringSWTTeam3;
using AirTrafficMonitoringSWTTeam3.Controler;
using AirTrafficMonitoringSWTTeam3.Events;
using NSubstitute;

namespace AirTrafficMonitoringSWTTeam3UnitTest
{
    [TestFixture]
    class SeparationInvestigationUnitTest
    {
        private SeparationInvestigation _uut;
        private IUpdater fakeUpdater;
      
        private SeparationWarningDataEvent _event;

        [SetUp]
        public void SetUp()
        {
            fakeUpdater = Substitute.For<IUpdater>();
            
            _uut = new SeparationInvestigation(fakeUpdater);
           

            _event = null;
            _uut.SeparationWarningDataEvent += (o, args) => { _event = args; };
        }

        

        [Test]
     
        [TestCase("BAA111", 10000, 51000, 1000, "DAA111", 10000, 50000, 1000, "EAA111", 30000, 40000, 5000, 1)]
        [TestCase("BAA111", 10000, 51000, 1200, "DAA111", 10000, 50000, 1000, "EAA111", 10000, 49000, 800, 2)]
        [TestCase("BAA111", 10000, 50000, 1000, "DAA111", 10000, 50000, 1000, "EAA111", 10000, 50000, 1000, 3)]
        public void AddSeparation_AircraftXAndYAndQIsInSeparation_ZNewSeparationsIsAdded(string t1, int x1, int y1,
            int a1, string t2, int x2, int y2, int a2, string t3, int x3, int y3, int a3, int numberOfSeparations)
        {
            List<Aircraft> testAircraft = new List<Aircraft>();
            testAircraft.Add(new Aircraft(t1, x1, y1, a1, DateTime.Now));
            testAircraft.Add(new Aircraft(t2, x2, y2, a2, DateTime.Now));
            testAircraft.Add(new Aircraft(t3, x3, y3, a3, DateTime.Now));

            fakeUpdater.UpdatedDataEvent += Raise.EventWith(this, new UpdatedDataEvent(testAircraft));

            Assert.That(_event.TransponderData.Count, Is.EqualTo(numberOfSeparations));
        }

        [Test]
        [TestCase("1AA111", 40000, 51000, 2000, "BAA111", 10000, 5000, 1000, "FAA111", 30000, 10000, 5000, 0)]
        
        public void AddSeparation_IsNull(string t1, int x1, int y1,
            int a1, string t2, int x2, int y2, int a2, string t3, int x3, int y3, int a3, int numberOfSeparations)
        {
            List<Aircraft> testAircraft = new List<Aircraft>();
            testAircraft.Add(new Aircraft(t1, x1, y1, a1, DateTime.Now));
            testAircraft.Add(new Aircraft(t2, x2, y2, a2, DateTime.Now));
            testAircraft.Add(new Aircraft(t3, x3, y3, a3, DateTime.Now));

            fakeUpdater.UpdatedDataEvent += Raise.EventWith(this, new UpdatedDataEvent(testAircraft));

            Assert.That(_event, Is.Null);
        }

        [Test]
        [TestCase("1AA111", 40000, 51000, 2000, "BAA111", 10000, 5000, 1000, "FAA111", 30000, 10000, 5000, "BAA111", 10000, 51000, 1000, "DAA111", 10000, 50000, 1000, "EAA111", 30000, 40000, 5000, 1)]
        [TestCase("1AA111", 40000, 51000, 2000, "BAA111", 10000, 5000, 1000, "FAA111", 30000, 10000, 5000, "BAA111", 10000, 51000, 1200, "DAA111", 10000, 50000, 1000, "EAA111", 10000, 49000, 800, 2)]
        [TestCase("BAA111", 10000, 51000, 1200, "DAA111", 10000, 50000, 1000, "EAA111", 10000, 49000, 800,  "BAA111", 10000, 51000, 1000, "DAA111", 10000, 50000, 1000, "EAA111", 30000, 40000, 5000, 0)]
        
        public void AddSeparation_AircraftXAndYAndQIsInSeparation_ZNewSeparationsIsAdded(string t1, int x1, int y1,
            int a1, string t2, int x2, int y2, int a2, string t3, int x3, int y3, int a3, string t4, int x4, int y4,
            int a4, string t5, int x5, int y5, int a5, string t6, int x6, int y6, int a6, int numberOfSeparations)
        {
            List<Aircraft> testAircraft = new List<Aircraft>();
            testAircraft.Add(new Aircraft(t1, x1, y1, a1, DateTime.Now));
            testAircraft.Add(new Aircraft(t2, x2, y2, a2, DateTime.Now));
            testAircraft.Add(new Aircraft(t3, x3, y3, a3, DateTime.Now));

            fakeUpdater.UpdatedDataEvent += Raise.EventWith(this, new UpdatedDataEvent(testAircraft));

            List<Aircraft> testAircraft2 = new List<Aircraft>();
            testAircraft2.Add(new Aircraft(t4, x4, y4, a4, DateTime.Now));
            testAircraft2.Add(new Aircraft(t5, x5, y5, a5, DateTime.Now));
            testAircraft2.Add(new Aircraft(t6, x6, y6, a6, DateTime.Now));

            fakeUpdater.UpdatedDataEvent += Raise.EventWith(this, new UpdatedDataEvent(testAircraft2));

            

            Assert.That(_event.TransponderData.Count, Is.EqualTo(numberOfSeparations));
        }


       

        [Test]
        public void SeparationInvestigation_Calls_FormattingSeparationToFileMethode()
        {
            List<Aircraft> testData = new List<Aircraft>();
            testData.Add(new Aircraft("ATR423", 85045, 12932, 14000, DateTime.ParseExact("20151006213456789",
                "yyyyMMddHHmmssfff",
                System.Globalization.CultureInfo.InvariantCulture)));
            testData.Add(new Aircraft("BCD123", 85045, 12932, 14000, DateTime.ParseExact("20151006213456789",
                "yyyyMMddHHmmssfff",
                System.Globalization.CultureInfo.InvariantCulture)));
            testData.Add(new Aircraft("XYZ98", 85000, 75654, 4000, DateTime.ParseExact("20151006213456789",
                "yyyyMMddHHmmssfff",
                System.Globalization.CultureInfo.InvariantCulture)));
            testData.Add(new Aircraft("XYZ986", 90059, 90654, 4000, DateTime.ParseExact("20151006213456789",
                "yyyyMMddHHmmssfff",
                System.Globalization.CultureInfo.InvariantCulture)));

            // Act: Trigger the fake object to execute event invocation
            fakeUpdater.UpdatedDataEvent += Raise.EventWith(this, new UpdatedDataEvent(testData));

            Assert.That(_event.TransponderData, Is.Not.Null);

            

        }
      

    }
}
