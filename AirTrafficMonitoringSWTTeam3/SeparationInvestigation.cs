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
        private IUpdated _updated;
        private List<SeparationWarningData> oldSeparationWarningData = new List<SeparationWarningData>();
        private List<SeparationWarningData> newSeparationWarningData = new List<SeparationWarningData>();
        public event EventHandler<SeparationWarningDataEvent> SeparationWarningDataEvent;

        public SeparationInvestigation(IUpdated updated)
        {
            _updated = updated;
            _updated.UpdatedDataEvent += SeparationController;

        }

       

        //    AddSeparations(e.TransponderData);
        //    SeparationController();

        //    if (newSeparationWarningData.Count != 0)
        //        SeparationWarningDataEvent?.Invoke(this, (new SeparationWarningDataEvent(newSeparationWarningData)));
        //}

        public void AddSeparations(List<Aircraft> newAircrafts)
        {
            foreach (Aircraft aircraft in newAircrafts)
            {
                foreach (Aircraft comparingAircraft in newAircrafts)
                {

                    if (aircraft.Tag != comparingAircraft.Tag)
                    {
                        int verticalSeparation = Math.Abs(aircraft.Altitude - comparingAircraft.Altitude);
                        double xDifference = Math.Abs(aircraft.XCoordinate - comparingAircraft.XCoordinate);
                        double yDifference = Math.Abs(aircraft.YCoordinate - comparingAircraft.YCoordinate);
                        double horizontalSeparation = Math.Sqrt(Math.Pow(xDifference, 2) + Math.Pow(yDifference, 2));

                        if (verticalSeparation < 300 && horizontalSeparation < 5000)
                        {
                            
                            newSeparationWarningData.Add(new SeparationWarningData(aircraft.Tag, comparingAircraft.Tag, aircraft.Timestamp));
                        }
                    }
                }
            }
        }

      public void SeparationController(object sender, UpdatedDataEvent e)
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
        }
    }
}
