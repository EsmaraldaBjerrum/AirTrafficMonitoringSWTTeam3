//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using NUnit.Framework;
//using AirTrafficMonitoringSWTTeam3;
//using AirTrafficMonitoringSWTTeam3.Controler;
//using AirTrafficMonitoringSWTTeam3.Events;
//using NSubstitute;

//namespace AirTrafficMonitoringSWTTeam3UnitTest
//{
//    [TestFixture]
//    class SeparationInvestigationUnitTest
//    {
//        private SeparationInvestigation _uut;
//        private IUpdater fakeUpdater;
//        private ILog fakeLogToLog;
//        private ILog fakeLogToScreen;
//        private Formatting_Separation fakeFormattingSeparation;
       
//        [SetUp]
//        public void SetUp()
//        {
//            fakeUpdater = Substitute.For<IUpdater>();
//            fakeLogToLog = Substitute.For<ILog>();
//            fakeLogToScreen = Substitute.For<ILog>();
//            _uut = new SeparationInvestigation(fakeUpdater);
//            fakeFormattingSeparation = Substitute.For<Formatting_Separation>(_uut, fakeLogToLog, fakeLogToScreen);


//        }


//        [Test]
//        [TestCase("AAA111", 10000, 10000, 1000, "CAA111", 50000, 40000, 5000, 0)]
//        [TestCase("BAA111", 10000, 51000, 1000, "DAA111", 10000, 50000, 1000, 1)]
//        public void AddSeparation_AircraftXAndYIsISeparation_ZNewSeparationIsAdded(string t1, int x1, int y1, int a1, string t2, int x2, int y2, int a2, int numberOfSeparations)
//        {
//            List<Aircraft> testAircraft = new List<Aircraft>();
//            testAircraft.Add(new Aircraft(t1, x1, y1, a1, DateTime.Now));
//            testAircraft.Add(new Aircraft(t2, x2, y2, a2, DateTime.Now));

//            _uut.AddSeparations(testAircraft);

//            Assert.That(_uut.newSeparationWarningData.Count.Equals(numberOfSeparations));
//        }

//        [Test]
//        [TestCase("1AA111", 40000, 51000, 2000, "BAA111", 10000, 5000, 1000, "FAA111", 30000, 10000, 5000, 0)]
//        [TestCase("BAA111", 10000, 51000, 1000, "DAA111", 10000, 50000, 1000, "EAA111", 30000, 40000, 5000, 1)]
//        [TestCase("BAA111", 10000, 51000, 1200, "DAA111", 10000, 50000, 1000, "EAA111", 10000, 49000, 800, 2)]
//        [TestCase("BAA111", 10000, 50000, 1000, "DAA111", 10000, 50000, 1000, "EAA111", 10000, 50000, 1000, 3)]
//        public void AddSeparation_AircraftXAndYAndQIsInSeparation_ZNewSeparationsIsAdded(string t1, int x1, int y1,
//            int a1, string t2, int x2, int y2, int a2, string t3, int x3, int y3, int a3, int numberOfSeparations)
//        {
//            List<Aircraft> testAircraft = new List<Aircraft>();
//            testAircraft.Add(new Aircraft(t1, x1, y1, a1, DateTime.Now));
//            testAircraft.Add(new Aircraft(t2, x2, y2, a2, DateTime.Now));
//            testAircraft.Add(new Aircraft(t3, x3, y3, a3, DateTime.Now));

//            _uut.AddSeparations(testAircraft);

//            Assert.That(_uut.newSeparationWarningData.Count, Is.EqualTo(numberOfSeparations));
//        }

//        [Test]
//        [TestCase("AAA111", "BBBB111", "CCC111", "DDD111", "EEE111", "FFF111", "GGG111", "HHH111", 2)]
//        [TestCase("AAA111", "BBBB111", "GGG111", "HHH111", "EEE111", "FFF111", "GGG111", "HHH111", 1)]
//        [TestCase("EEE111", "FFF111", "GGG111", "HHH111", "EEE111", "FFF111", "GGG111", "HHH111", 0)]

//        public void SeparationController_XOldAndYNew_ZNew(string a, string b, string c, string d, string e, string f, string g, string h, int numberOfNewSeparations)
//        {
//            _uut.oldSeparationWarningData.Add(new SeparationWarningData(a,b,DateTime.Now));
//            _uut.oldSeparationWarningData.Add(new SeparationWarningData(c,d, DateTime.Now));

//            _uut.newSeparationWarningData.Add(new SeparationWarningData(e,f,DateTime.Now));
//            _uut.newSeparationWarningData.Add(new SeparationWarningData(g,h,DateTime.Now));

//            _uut.SeparationController();

//            Assert.That(_uut.newSeparationWarningData.Count, Is.EqualTo(numberOfNewSeparations));
//        }

//        [Test]
//        public void SeparationInvestigation_Calls_FormattingSeparationToFileMethode()
//        {
//            List<Aircraft> testData = new List<Aircraft>();
//            testData.Add(new Aircraft("ATR423", 85045, 12932, 14000, DateTime.ParseExact("20151006213456789",
//                "yyyyMMddHHmmssfff",
//                System.Globalization.CultureInfo.InvariantCulture)));
//            testData.Add(new Aircraft("BCD123", 10005, 85001, 12000, DateTime.ParseExact("20151006213456789",
//                "yyyyMMddHHmmssfff",
//                System.Globalization.CultureInfo.InvariantCulture)));
//            testData.Add(new Aircraft("XYZ98", 85000, 75654, 4000, DateTime.ParseExact("20151006213456789",
//                "yyyyMMddHHmmssfff",
//                System.Globalization.CultureInfo.InvariantCulture)));
//            testData.Add(new Aircraft("XYZ986", 90059, 90654, 4000, DateTime.ParseExact("20151006213456789",
//                "yyyyMMddHHmmssfff",
//                System.Globalization.CultureInfo.InvariantCulture)));

//            // Act: Trigger the fake object to execute event invocation
//            fakeUpdater.UpdatedDataEvent += Raise.EventWith(this, new UpdatedDataEvent(testData));
            
//            fakeFormattingSeparation.Received(1).StringToPrintSeparationInFile(this, new SeparationWarningDataEvent(_uut.newSeparationWarningData));

//        }
//        [Test]
//        public void SeparationInvestigation_Calls_FormattingSeparationToScreenMethode()
//        {
//            List<Aircraft> testData = new List<Aircraft>();
//            testData.Add(new Aircraft("ATR423", 85045, 12932, 14000, DateTime.ParseExact("20151006213456789",
//                "yyyyMMddHHmmssfff",
//                System.Globalization.CultureInfo.InvariantCulture)));
//            testData.Add(new Aircraft("BCD123", 10005, 85001, 12000, DateTime.ParseExact("20151006213456789",
//                "yyyyMMddHHmmssfff",
//                System.Globalization.CultureInfo.InvariantCulture)));
//            testData.Add(new Aircraft("XYZ98", 85000, 75654, 4000, DateTime.ParseExact("20151006213456789",
//                "yyyyMMddHHmmssfff",
//                System.Globalization.CultureInfo.InvariantCulture)));
//            testData.Add(new Aircraft("XYZ986", 90059, 90654, 4000, DateTime.ParseExact("20151006213456789",
//                "yyyyMMddHHmmssfff",
//                System.Globalization.CultureInfo.InvariantCulture)));

//            // Act: Trigger the fake object to execute event invocation
//            fakeUpdater.UpdatedDataEvent += Raise.EventWith(this, new UpdatedDataEvent(testData));

//            fakeFormattingSeparation.Received(1).StringToPrintSeparationToScreen(this, new SeparationWarningDataEvent(_uut.newSeparationWarningData));

//        }

//    }
//}
