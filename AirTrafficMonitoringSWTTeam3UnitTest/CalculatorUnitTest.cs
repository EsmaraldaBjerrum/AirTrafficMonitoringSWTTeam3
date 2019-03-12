using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AirTrafficMonitoringSWTTeam3;
using TransponderReceiver;
using NSubstitute;

namespace AirTrafficMonitoringSWTTeam3UnitTest
{
    [TestFixture]
    public class CalculatorUnitTest
    {

        private ITransponderReceiver _fakeTransponderReceiver;
        private Calculator uut;

        [SetUp]
        public void SetUp()
        {
            _fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();
            uut = new Calculator(_fakeTransponderReceiver);
        }

        [Test]
        public void AirSpaceIsCalled()
        {
            List<string> testData = new List<string>();
            testData.Add("ATR423;39045;12932;14000;20151006213456789");
            testData.Add("BCD123;10005;84000;12000;20151006213456789");
            testData.Add("XYZ987;25059;75654;4000;20151006213456789");

            // Act: Trigger the fake object to execute event invocation
            _fakeTransponderReceiver.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));

            Assert.That(uut.currentAircrafts.Count.Equals(3));
        }

        [Test]
        public void CalculateCompassCourse_()
        {
            _fakeTransponderReceiver.   
        }
    }
}
