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
            
            foreach (var data in e.TransponderData)
            {
                string[] aircraftdata = new string[5];
                aircraftdata = data.Split(';');
                int xCoordinate = Convert.ToInt32(aircraftdata[2]);
                int yCoordinate = Convert.ToInt32(aircraftdata[3]);

                if (xCoordinate <= 85000 && yCoordinate <= 85000)
                {
                    Aircraft aircraft = new Aircraft();
                    aircraft.Tag = aircraftdata[0];
                    aircraft.XCoordinate = xCoordinate;
                    aircraft.YCoordinate = yCoordinate;
                    aircraft.Altitude = Convert.ToInt32(aircraftdata[3]);
                    aircraft.Timestamp = Convert.ToDateTime(aircraftdata[4]);
                    currentAircrafts.Add(aircraft);
                }
            }
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
