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
        public void CalculateCompassCourse_()
        {
            _fakeTransponderReceiver.   
        }

       [Test]
       public void HorizontalVelocity()
       {
          List<Aircraft> testAircraftsList = new List<Aircraft>();
         
          
       }
    }

}
