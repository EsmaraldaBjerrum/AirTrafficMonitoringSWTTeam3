using System;
using System.Collections.Generic;
using AirTrafficMonitoringSWTTeam3.Events;

namespace AirTrafficMonitoringSWTTeam3.Controler
{
    public class Updater : IUpdater
    {
        public event EventHandler<UpdatedDataEvent> UpdatedDataEvent;
        private IFilter _filter;
        public List<Aircraft> WithDataAircrafts;
        public List<Aircraft> WithoutDataAircrafts;

        public Updater(IFilter filter)
        {
            _filter = filter;
            _filter.FilterDataEvent += UpdatedMethod;
            WithDataAircrafts = new List<Aircraft>();
            WithoutDataAircrafts = new List<Aircraft>();
        }

        public void UpdatedMethod(object sender, FilterDataEvent e)
        {
            WithoutDataAircrafts = e.FilterData;
            CalculateCompassCourse(WithoutDataAircrafts);
            HorizontalVelocity(WithoutDataAircrafts);
            WithDataAircrafts = new List<Aircraft>(WithoutDataAircrafts);

            UpdatedDataEvent?.Invoke(this, new UpdatedDataEvent(WithDataAircrafts));

            WithoutDataAircrafts.Clear();

        }

        public void CalculateCompassCourse(List<Aircraft> WithoutDataAircrafts)
        {
            foreach (Aircraft WithoutDataaircraft in WithoutDataAircrafts)
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
                            if (xDifference > 0 && yDifference > 0)
                            {
                                WithoutDataaircraft.CompassCourse =
                                   Convert.ToInt32(Math.Round(Math.Atan(Math.Abs(xDifference / yDifference)) / Math.PI * 180));
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
        private double velocity;

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
                    WithoutDataaircraft.HorizontalVelocity = Math.Round(velocity, 2);
                }
            }
        }
    }
}