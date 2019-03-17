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
    }
}
