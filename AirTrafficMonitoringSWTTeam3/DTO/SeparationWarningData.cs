using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoringSWTTeam3
{
    public class SeparationWarningData
    {
        public string AircraftTag1 { get; set; }
        public string AircraftTag2 { get; set; }
        public DateTime SeparationTimeStamp { get; set; }

        public SeparationWarningData(string aircraftTag1, string aircraftTag2, DateTime separationTimeStamp)
        {
            AircraftTag1 = aircraftTag1;
            AircraftTag2 = aircraftTag2;
            SeparationTimeStamp = separationTimeStamp;
        }
    }
}
