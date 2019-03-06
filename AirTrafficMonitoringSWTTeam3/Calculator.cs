using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace AirTrafficMonitoringSWTTeam3
{
    public class Calculator
    {
        private ITransponderReceiver _transponderReceiver;
        public Calculator(ITransponderReceiver transponderReceiver)
        {
            _transponderReceiver = transponderReceiver;

            _transponderReceiver.TransponderDataReady += AirSpace;
        }

        private void AirSpace(object sender, RawTransponderDataEventArgs e)
        {

        }

        public void CalculateCompassCourse()
        {

        }
    }
}
