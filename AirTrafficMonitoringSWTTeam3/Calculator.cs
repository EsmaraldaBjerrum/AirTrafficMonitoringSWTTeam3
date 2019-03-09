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

        private List<Aircraft> currentAircrafts;

        public Calculator(ITransponderReceiver transponderReceiver)
        {
            currentAircrafts = new List<Aircraft>();

            _transponderReceiver = transponderReceiver;

            _transponderReceiver.TransponderDataReady += AirSpace;
        }

        private void AirSpace(object sender, RawTransponderDataEventArgs e)
        {

        }

        public void CalculateCompassCourse(List<Aircraft> aircrafts)
        {
            foreach (Aircraft aircraft in aircrafts)
            {
                foreach (Aircraft currentAircraft in currentAircrafts)
                {
                    if (aircraft.Tag == currentAircraft.Tag)
                    {

                        // Kode til at udregne kompasretning
                    }
                }
            }

            currentAircrafts = aircrafts;
        }
    }
}
