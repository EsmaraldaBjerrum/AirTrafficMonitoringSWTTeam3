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
        [TestCase(5,7)]
        public void CalculateCompassCourse_ZCurrentAircraft_QNewAircraft_ResultsInQCurrentAircrafts(int Z, int Q)
        {
            for (int i = 0; i < Z; i++)
            {
                
            }
        }



        [Test]
        [TestCase("AAA111", 10000, 10000, 1000, "AAA111", 10000, 40000, 1000, 0)]
        [TestCase("BAA111", 10000, 60000, 1000, "BAA111", 10000, 40000, 1000, 180)]
        [TestCase("CAA111", 10000, 10000, 1000, "CAA111", 20000, 10000, 1000, 90)]
        [TestCase("DAA111", 60000, 10000, 1000, "DAA111", 50000, 10000, 1000, 270)]
        public void CalculateCompassCourse_AircraftTravelingNorthSouthEastWest(string t1, int x1, int y1, int a1, string t2, int x2, int y2, int a2, int course1)
        {
            uut.currentAircrafts.Add(new Aircraft(t1, x1, y1, a1, DateTime.Now));

            List<Aircraft> newAircraft = new List<Aircraft>();
            newAircraft.Add(new Aircraft(t2, x2, y2, a2, DateTime.Now));

            uut.CalculateCompassCourse(newAircraft);

            Assert.That(uut.currentAircrafts[0].CompassCourse, Is.EqualTo(course1));
        }
    }
}
