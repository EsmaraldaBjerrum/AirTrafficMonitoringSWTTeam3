using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoringSWTTeam3;
using AirTrafficMonitoringSWTTeam3.Controler;
using AirTrafficMonitoringSWTTeam3.Events;
using NUnit.Framework;
using NSubstitute;

namespace AirTrafficMonitoringSWTTeam3UnitTest.Controller
{
    [TestFixture]
    class UpdaterUnitTest
    {
        private Updater _uut;
        
        private IFilter _fakeFilter;
        private Formatting_Tracks _fakeFT;
        private ILog _fakeLog;
        private UpdatedDataEvent _event;
        [SetUp]
        public void SetUp()
        {
            _fakeLog = Substitute.For<ILog>();
            _fakeFilter = Substitute.For<IFilter>();
            _uut = new Updater(_fakeFilter);
            _fakeFT = Substitute.For<Formatting_Tracks>(_uut, _fakeLog);
            

            _event = null;
            _uut.UpdatedDataEvent += (o, args) => { _event = args; };
        }

        [Test]
        [TestCase("AAA111", 10000, 10000, 1000, "AAA111", 10000, 40000, 1000, 0)]
        [TestCase("BAA111", 10000, 60000, 1000, "BAA111", 10000, 40000, 1000, 180)]
        [TestCase("CAA111", 10000, 10000, 1000, "CAA111", 20000, 10000, 1000, 90)]
        [TestCase("DAA111", 60000, 10000, 1000, "DAA111", 50000, 10000, 1000, 270)]
        public void CalculateCompassCourse_AircraftTravelingNorthSouthEastWest(string t1, int x1, int y1, int a1, string t2, int x2, int y2, int a2, int course1)
        {
            List<Aircraft> testData = new List<Aircraft>();
            testData.Add( new Aircraft(t1, x1, y1, a1, DateTime.Now));

            _fakeFilter.FilterDataEvent
                += Raise.EventWith(this, new FilterDataEvent(testData));

            List<Aircraft> testData2 = new List<Aircraft>();
            testData2.Add(new Aircraft(t2, x2, y2, a2, DateTime.Now));

            _fakeFilter.FilterDataEvent
                += Raise.EventWith(this, new FilterDataEvent(testData2));

            Assert.That(_event.UpdatedData[0].CompassCourse, Is.EqualTo(course1));
        }


        [Test]
        [TestCase("AAA111", 10000, 5000, 1000, "AAA111", 10001, 85000, 1000, 0)]
        [TestCase("BAA111", 10000, 60000, 1000, "BAA111", 10100, 70000, 1000, 1)]
        [TestCase("CAA111", 10000, 10000, 1000, "CAA111", 10100, 10100, 1000, 45)]
        [TestCase("DAA111", 60000, 10000, 1000, "DAA111", 70000, 10100, 1000, 89)]
        [TestCase("EAA111", 5000, 10000, 1000, "EAA111", 85000, 10001, 1000, 90)]
        [TestCase("FAA111", 15000, 10000, 1000, "FAA111", 19000, 20010, 1000, 22)]
        public void CalculateCompassCourse_AircraftTravelingBetweenNorthAndEast(string t1, int x1, int y1, int a1, string t2, int x2, int y2, int a2, int course1)
        {
            List<Aircraft> testData = new List<Aircraft>();
            testData.Add(new Aircraft(t1, x1, y1, a1, DateTime.Now));

            _fakeFilter.FilterDataEvent
                += Raise.EventWith(this, new FilterDataEvent(testData));

            List<Aircraft> testData2 = new List<Aircraft>();
            testData2.Add(new Aircraft(t2, x2, y2, a2, DateTime.Now));

            _fakeFilter.FilterDataEvent
                += Raise.EventWith(this, new FilterDataEvent(testData2));

            Assert.That(_event.UpdatedData[0].CompassCourse, Is.EqualTo(course1));
        }

        [Test]
        [TestCase("AAA111", 10000, 85000, 1000, "AAA111", 10001, 5000, 1000, 180)]
        [TestCase("BAA111", 10000, 60000, 1000, "BAA111", 10100, 50000, 1000, 179)]
        [TestCase("CAA111", 10000, 10000, 1000, "CAA111", 10100, 9900, 1000, 135)]
        [TestCase("DAA111", 60000, 10000, 1000, "DAA111", 70000, 9900, 1000, 91)]
        [TestCase("EAA111", 5000, 10000, 1000, "EAA111", 85000, 10001, 1000, 90)]
        [TestCase("FAA111", 5000, 14000, 1000, "FAA111", 15010, 10000, 1000, 112)]
        public void CalculateCompassCourse_AircraftTravelingBetweenEastAndSouth(string t1, int x1, int y1, int a1, string t2, int x2, int y2, int a2, int course1)
        {
            List<Aircraft> testData = new List<Aircraft>();
            testData.Add(new Aircraft(t1, x1, y1, a1, DateTime.Now));

            _fakeFilter.FilterDataEvent
                += Raise.EventWith(this, new FilterDataEvent(testData));

            List<Aircraft> testData2 = new List<Aircraft>();
            testData2.Add(new Aircraft(t2, x2, y2, a2, DateTime.Now));

            _fakeFilter.FilterDataEvent
                += Raise.EventWith(this, new FilterDataEvent(testData2));

            Assert.That(_event.UpdatedData[0].CompassCourse, Is.EqualTo(course1));
        }

        [Test]
        [TestCase("AAA111", 10000, 85000, 1000, "AAA111", 9999, 5000, 1000, 180)]
        [TestCase("BAA111", 10100, 60000, 1000, "BAA111", 10000, 50000, 1000, 181)]
        [TestCase("CAA111", 10000, 10000, 1000, "CAA111", 9900, 9900, 1000, 225)]
        [TestCase("DAA111", 60000, 10000, 1000, "DAA111", 50000, 9900, 1000, 269)]
        [TestCase("EAA111", 85000, 10001, 1000, "EAA111", 5000, 10000, 1000, 270)]
        [TestCase("FAA111", 14000, 15010, 1000, "FAA111", 10000, 5000, 1000, 202)]
        public void CalculateCompassCourse_AircraftTravelingBetweenSouthAndWest(string t1, int x1, int y1, int a1, string t2, int x2, int y2, int a2, int course1)
        {
            List<Aircraft> testData = new List<Aircraft>();
            testData.Add(new Aircraft(t1, x1, y1, a1, DateTime.Now));

            _fakeFilter.FilterDataEvent
                += Raise.EventWith(this, new FilterDataEvent(testData));

            List<Aircraft> testData2 = new List<Aircraft>();
            testData2.Add(new Aircraft(t2, x2, y2, a2, DateTime.Now));

            _fakeFilter.FilterDataEvent
                += Raise.EventWith(this, new FilterDataEvent(testData2));

            Assert.That(_event.UpdatedData[0].CompassCourse, Is.EqualTo(course1));
        }

        [Test]
        [TestCase("AAA111", 10000, 5000, 1000, "AAA111", 10001, 85000, 1000, 0)]
        [TestCase("BAA111", 10000, 60000, 1000, "BAA111", 9900, 70000, 1000, 359)]
        [TestCase("CAA111", 10000, 10000, 1000, "CAA111", 9900, 10100, 1000, 315)]
        [TestCase("DAA111", 60000, 10000, 1000, "DAA111", 50000, 10100, 1000, 271)]
        [TestCase("EAA111", 85000, 10000, 1000, "EAA111", 5000, 10001, 1000, 270)]
        [TestCase("FAA111", 15010, 10000, 1000, "FAA111", 5000, 14000, 1000, 292)]
        public void CalculateCompassCourse_AircraftTravelingBetweenWestAndNorth(string t1, int x1, int y1, int a1, string t2, int x2, int y2, int a2, int course1)
        {
            List<Aircraft> testData = new List<Aircraft>();
            testData.Add(new Aircraft(t1, x1, y1, a1, DateTime.Now));

            _fakeFilter.FilterDataEvent
                += Raise.EventWith(this, new FilterDataEvent(testData));

            List<Aircraft> testData2 = new List<Aircraft>();
            testData2.Add(new Aircraft(t2, x2, y2, a2, DateTime.Now));

            _fakeFilter.FilterDataEvent
                += Raise.EventWith(this, new FilterDataEvent(testData2));

            Assert.That(_event.UpdatedData[0].CompassCourse, Is.EqualTo(course1));
        }


        [Test]
        [TestCase("ATR423", 39045, 12932, 14000, "20151006213456789", "ATR423", 45000, 15940, 16000, "20151006214356895", 12.352)]
        [TestCase("SKF218", 82000, 21000, 16300, "20191203213426980", "SKF218", 83000, 19960, 37000, "20191203214456990", 2.2901)]
        [TestCase("EDB239", 10000, 60000, 1000, "20191203214726980", "EDB239", 20000, 80000, 1000, "20191203215356990", 57.334)]
        public void HorizontalVelocity(string t1, int x1, int y1, int a1, string ts1, string t2, int x2, int y2, int a2, string ts2, double velocity1)
        {
            List<Aircraft> testData = new List<Aircraft>();
            testData.Add(new Aircraft(t1, x1, y1, a1, (DateTime.ParseExact(ts1, "yyyyMMddHHmmssfff",
                System.Globalization.CultureInfo.InvariantCulture))));
           
            _fakeFilter.FilterDataEvent
                += Raise.EventWith(this, new FilterDataEvent(testData));

            List<Aircraft> testData2 = new List<Aircraft>();
            

            testData2.Add(new Aircraft(t2, x2, y2, a2, (DateTime.ParseExact(ts2, "yyyyMMddHHmmssfff",
                System.Globalization.CultureInfo.InvariantCulture))));

            _fakeFilter.FilterDataEvent
                += Raise.EventWith(this, new FilterDataEvent(testData2));

            Assert.That(_event.UpdatedData[0].HorizontalVelocity, Is.EqualTo(velocity1).Within(00.01));
        }


        [Test]
        public void UpdaterCallsFormatting_Tracks()
        {
            List<Aircraft> testData = new List<Aircraft>();
            testData.Add(new Aircraft("ATR423", 85045, 12932, 14000, DateTime.ParseExact("20151006213456789",
                "yyyyMMddHHmmssfff",
                System.Globalization.CultureInfo.InvariantCulture)));
            testData.Add(new Aircraft("BCD123", 10005, 85001, 12000, DateTime.ParseExact("20151006213456789",
                "yyyyMMddHHmmssfff",
                System.Globalization.CultureInfo.InvariantCulture)));
            testData.Add(new Aircraft("XYZ98", 85000, 75654, 4000, DateTime.ParseExact("20151006213456789",
                "yyyyMMddHHmmssfff",
                System.Globalization.CultureInfo.InvariantCulture)));
            testData.Add(new Aircraft("XYZ986", 90059, 90654, 4000, DateTime.ParseExact("20151006213456789",
                "yyyyMMddHHmmssfff",
                System.Globalization.CultureInfo.InvariantCulture)));

            // Act: Trigger the fake object to execute event invocation
            _fakeFilter.FilterDataEvent
                += Raise.EventWith(this, new FilterDataEvent(testData));

            Assert.That(_event, Is.Not.Null);

        }

        [Test]
        public void UpdaterCallsSeparationInvestigation()
        {
            List<Aircraft> testData = new List<Aircraft>();
            testData.Add(new Aircraft("ATR423", 85045, 12932, 14000, DateTime.ParseExact("20151006213456789",
                "yyyyMMddHHmmssfff",
                System.Globalization.CultureInfo.InvariantCulture)));
            testData.Add(new Aircraft("BCD123", 10005, 85001, 12000, DateTime.ParseExact("20151006213456789",
                "yyyyMMddHHmmssfff",
                System.Globalization.CultureInfo.InvariantCulture)));
            testData.Add(new Aircraft("XYZ98", 85000, 75654, 4000, DateTime.ParseExact("20151006213456789",
                "yyyyMMddHHmmssfff",
                System.Globalization.CultureInfo.InvariantCulture)));
            testData.Add(new Aircraft("XYZ986", 90059, 90654, 4000, DateTime.ParseExact("20151006213456789",
                "yyyyMMddHHmmssfff",
                System.Globalization.CultureInfo.InvariantCulture)));

            // Act: Trigger the fake object to execute event invocation
            _fakeFilter.FilterDataEvent
                += Raise.EventWith(this, new FilterDataEvent(testData));

            Assert.That(_event, Is.Not.Null);

        }




    }
}
