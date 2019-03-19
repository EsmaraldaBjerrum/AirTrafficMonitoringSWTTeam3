using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace AirTrafficMonitoringSWTTeam3
{
    public partial class Calculator
    {
        private ITransponderReceiver _transponderReceiver;
       private Print _print;

        public List<Aircraft> WithoutDataAircrafts = new List<Aircraft>();
        public List<Aircraft> WithDataAircrafts = new List<Aircraft>();
        private double velocity;
        public event EventHandler<AirspaceDataEventArgs> AirspaceDataEvent;
        private bool _newData = false;
       


        public Calculator(ITransponderReceiver transponderReceiver)
        {
            _print = new Print();
            
            _transponderReceiver = transponderReceiver;

            _transponderReceiver.TransponderDataReady += Control;
        }

        public void Control(object sender, RawTransponderDataEventArgs e)
        {
            AirSpace(e.TransponderData);

            if (WithoutDataAircrafts.Count != 0)
            {
                CalculateCompassCourse(WithoutDataAircrafts);
                HorizontalVelocity(WithoutDataAircrafts);
                WithDataAircrafts = new List<Aircraft>(WithoutDataAircrafts);

                _print.PrintOnScreen(WithDataAircrafts);

                AirspaceDataEvent?.Invoke(this, (new AirspaceDataEventArgs(WithDataAircrafts)));

                WithoutDataAircrafts.Clear();
            }
        }

        public void AirSpace(List<string> newAircrafts)
        {
            foreach (var data in newAircrafts)
            {
                string[] aircraftdata = new string[5];
                aircraftdata = data.Split(';');
                int xCoordinate = Convert.ToInt32(aircraftdata[1]);
                int yCoordinate = Convert.ToInt32(aircraftdata[2]);

                if (xCoordinate <= 85000 && yCoordinate <= 85000)
                {
                    Aircraft aircraft = new Aircraft(aircraftdata[0], xCoordinate, yCoordinate,
                        Convert.ToInt32(aircraftdata[3]), DateTime.ParseExact(aircraftdata[4], "yyyyMMddHHmmssfff",
                            System.Globalization.CultureInfo.InvariantCulture));

                    WithoutDataAircrafts.Add(aircraft);
                }
            }
        }


        public void CalculateCompassCourse(List<Aircraft> WithoutDataaircrafts)
        {
            foreach (Aircraft WithoutDataaircraft in WithoutDataaircrafts)
            {
                foreach (Aircraft WithDataAircraft in WithDataAircrafts)
                {
                    if (WithoutDataaircraft.Tag == WithDataAircraft.Tag)
                    {
                        double xDifference = WithoutDataaircraft.XCoordinate - WithDataAircraft.XCoordinate;
                        double yDifference = WithoutDataaircraft.YCoordinate - WithDataAircraft.YCoordinate;

                        if (xDifference == 0)
                        {
                            if (yDifference > 0)
                            {
                                WithoutDataaircraft.CompassCourse = 0;
                            }
                            else
                            {
                                WithoutDataaircraft.CompassCourse = 180;
                            }
                        }

                        else if (yDifference == 0)
                        {
                            if (xDifference > 0)
                            {
                                WithoutDataaircraft.CompassCourse = 90;
                            }
                            else
                            {
                                WithoutDataaircraft.CompassCourse = 270;
                            }
                        }

                        else
                        {
                            if(xDifference > 0 && yDifference > 0)
                            { 
                            WithoutDataaircraft.CompassCourse =
                               Convert.ToInt32(Math.Round(Math.Atan(Math.Abs(xDifference / yDifference))/Math.PI*180));
                            }

                            if (xDifference > 0 && yDifference < 0)
                            {
                                WithoutDataaircraft.CompassCourse =
                                    Convert.ToInt32(Math.Round(Math.Atan(Math.Abs(yDifference / xDifference)) / Math.PI * 180));
                                WithoutDataaircraft.CompassCourse += 90;
                            }

                            if (xDifference < 0 && yDifference < 0)
                            {
                                WithoutDataaircraft.CompassCourse =
                                    Convert.ToInt32(Math.Round(Math.Atan(Math.Abs(xDifference / yDifference)) / Math.PI * 180));
                                WithoutDataaircraft.CompassCourse += 180;
                            }

                            if (xDifference < 0 && yDifference > 0)
                            {
                                WithoutDataaircraft.CompassCourse =
                                    Convert.ToInt32(Math.Round(Math.Atan(Math.Abs(yDifference / xDifference)) / Math.PI * 180));
                                WithoutDataaircraft.CompassCourse += 270;
                            }
                        }
                    } 
                }
            }
        }

        public void HorizontalVelocity(List<Aircraft> WithoutDataaircrafts)
        {
            foreach (Aircraft WithoutDataaircraft in WithoutDataaircrafts)
            {
                foreach (Aircraft WithDataAircraft in WithDataAircrafts)
                {
                    if (WithDataAircraft.Tag == WithoutDataaircraft.Tag)
                    {
                        DateTime oldDateTime = WithDataAircraft.Timestamp;
                        DateTime newDateTime = WithoutDataaircraft.Timestamp;
                        double interval = (newDateTime - oldDateTime).TotalSeconds;

                        double distance =
                            Math.Sqrt(Math.Pow(WithoutDataaircraft.XCoordinate - WithDataAircraft.XCoordinate, 2) +
                                      (Math.Pow(WithoutDataaircraft.YCoordinate - WithDataAircraft.YCoordinate, 2)));

                        velocity = distance / interval;
                    }
                    WithoutDataaircraft.HorizontalVelocity = Math.Round(velocity,2);
                }
            }
        }
    }
}
