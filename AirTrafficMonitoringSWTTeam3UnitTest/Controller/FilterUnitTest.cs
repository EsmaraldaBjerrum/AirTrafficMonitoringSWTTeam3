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

namespace AirTrafficMonitoringSWTTeam3UnitTest.Controller
{
    class FilterUnitTest
    {
        private Filter _uut;
        private IConverter _fakeConverter;
        private Updater _fakeUpdater;
        [SetUp]
        public void SetUp()
        {
            _fakeConverter = Substitute.For<IConverter>();
            _uut = new Filter(_fakeConverter);
            _fakeUpdater = Substitute.For<Updater>(_uut);

        }

        [Test]
        public void FilterOnlyFlightsWithinTheSpace()
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
            _fakeConverter.ConvertDataEvent
                += Raise.EventWith(this, new ConvertDataEvent (testData));

            Assert.That(_uut.filterList.Count.Equals(0));
        }


        [Test]
        public void FilterCallsUpdater()
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
            _fakeConverter.ConvertDataEvent
                += Raise.EventWith(this, new ConvertDataEvent(testData));

            _fakeUpdater.Received(1).UpdatedMethod(this, new FilterDataEvent(_uut.filterList));

        }
    }
}
