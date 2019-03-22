using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoringSWTTeam3.Controler;
using NUnit.Framework;
using NSubstitute;

namespace AirTrafficMonitoringSWTTeam3UnitTest.Controller
{
    [TestFixture]
    class UpdaterUnitTest
    {
        private Updater _uut;
        
        private IFilter _fakeFilter;
        [SetUp]
        public void SetUp()
        {
            _fakeFilter = Substitute.For<IFilter>();
            _uut = new Updater(_fakeFilter);
            
        }


    }
}
