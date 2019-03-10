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
                        double xDifference = currentAircraft.XCoordinate - aircraft.XCoordinate;
                        double yDifference = currentAircraft.YCoordinate - aircraft.YCoordinate;

                        if (xDifference == 0)
                        {
                            if (yDifference > 0)
                            {
                                aircraft.CompassCourse = 0;
                            }
                            else
                            {
                                aircraft.CompassCourse = 180;
                            }
                        }

                        else if (yDifference == 0)
                        {
                            if (xDifference > 0)
                            {
                                aircraft.CompassCourse = 90;
                            }
                            else
                            {
                                aircraft.CompassCourse = 270;
                            }
                        }

                        else
                        {
                            aircraft.CompassCourse = Convert.ToInt32(Math.Round(Math.Atan(Math.Abs(xDifference / yDifference))));

                            if (xDifference > 0 && yDifference < 0)
                            {
                                aircraft.CompassCourse += 90;
                            }
                            if (xDifference < 0 && yDifference < 0)
                            {
                                aircraft.CompassCourse += 180;
                            }
                            if (xDifference < 0 && yDifference > 0)
                            {
                                aircraft.CompassCourse += 270;
                            }
                        }
                    }
                }
            }

            currentAircrafts = aircrafts;
        }
    }
}
