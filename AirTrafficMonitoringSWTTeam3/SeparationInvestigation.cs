using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoringSWTTeam3.Controler;
using AirTrafficMonitoringSWTTeam3.Events;


namespace AirTrafficMonitoringSWTTeam3
{
    public class SeparationInvestigation
    {
        private IUpdater _updater;
        public List<SeparationWarningData> oldSeparationWarningData = new List<SeparationWarningData>();
        public List<SeparationWarningData> newSeparationWarningData = new List<SeparationWarningData>();
        public event EventHandler<SeparationWarningDataEvent> SeparationWarningDataEvent;

        public SeparationInvestigation(IUpdater updater)
        {
            _updater = updater;
            _updater.UpdatedDataEvent += RunSeparationInvestigation;

        }

        public void RunSeparationInvestigation(object sender, UpdatedDataEvent e)
        {
            AddSeparations(e.UpdatedData);
            SeparationController();

            if (newSeparationWarningData.Count != 0)
                SeparationWarningDataEvent?.Invoke(this, (new SeparationWarningDataEvent(newSeparationWarningData)));
        }

        public void AddSeparations(List<Aircraft> newAircrafts)
        {
            int counter = 1;
            foreach (Aircraft aircraft in newAircrafts)
            {
                for (int i = counter; i < newAircrafts.Count; i++)
                {
                    int verticalSeparation = Math.Abs(aircraft.Altitude - newAircrafts[i].Altitude);
                    double xDifference = Math.Abs(aircraft.XCoordinate - newAircrafts[i].XCoordinate);
                    double yDifference = Math.Abs(aircraft.YCoordinate - newAircrafts[i].YCoordinate);
                    double horizontalSeparation = Math.Sqrt(Math.Pow(xDifference, 2) + Math.Pow(yDifference, 2));

                    if (verticalSeparation < 300 && horizontalSeparation < 5000)
                    {
                        newSeparationWarningData.Add(new SeparationWarningData(aircraft.Tag, newAircrafts[i].Tag, aircraft.Timestamp));
                    }
                }
                counter++;
            }
        }

        public void SeparationController()
        {
            foreach (SeparationWarningData newSeparationData in newSeparationWarningData)
            {
                foreach (SeparationWarningData oldSeparationData in oldSeparationWarningData)
                {
                    if (newSeparationData.AircraftTag1 == oldSeparationData.AircraftTag1 &&
                        newSeparationData.AircraftTag2 == oldSeparationData.AircraftTag2 ||
                        newSeparationData.AircraftTag2 == oldSeparationData.AircraftTag1 &&
                         newSeparationData.AircraftTag1 == oldSeparationData.AircraftTag2)
                    {
                        newSeparationWarningData.Remove(newSeparationData);
                    }
                }
            }

            oldSeparationWarningData = newSeparationWarningData;
        }
    }
}
