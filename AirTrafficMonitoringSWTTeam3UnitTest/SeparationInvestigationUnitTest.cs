using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AirTrafficMonitoringSWTTeam3;
using NSubstitute;

namespace AirTrafficMonitoringSWTTeam3UnitTest
{
    [TestFixture]
    class SeparationInvestigationUnitTest
    {
        private SeparationInvestigation _uut;
        private Calculator _calculator;
       
        public void SetUp()
        {
            _calculator = Substitute.For<Calculator>();
            _uut = new SeparationInvestigation(_calculator);

        }

        [Test]
        public void IsSeparationInvestigationCalled()
        {
            

        }

        [Test]
        [TestCase("AAA111", 10000, 10000, 1000, "AAA111", 50000, 40000, 5000, 0)]
        [TestCase("BAA111", 10000, 60000, 1000, "BAA111", 10000, 40000, 1000, 1)]
        public void AddSeparation_AircraftXAndYIsISeparation_ZNewSeparationIsAdded(string t1, int x1, int y1, int a1, string t2, int x2, int y2, int a2, int numberOfSeparations)
        {
            List<Aircraft> testAircraft = new List<Aircraft>();
            testAircraft.Add(new Aircraft(t1, x1, y1, a1, DateTime.Now));
            testAircraft.Add(new Aircraft(t2, x2, y2, a2, DateTime.Now));

            _uut.AddSeparations(testAircraft);

            Assert.That(_uut.newSeparationWarningData.Count, Is.EqualTo(numberOfSeparations));
        }
    }
}
