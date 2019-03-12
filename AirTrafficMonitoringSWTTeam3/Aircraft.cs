using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoringSWTTeam3
{
    public class Aircraft
    {
        public string Tag { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public int Altitude { get; set; }
        public double HorizontalVelocity { get; set; }
        public int CompassCourse { get; set; }
        public DateTime Timestamp { get; set; }

        public Aircraft(string tag, int xCoordinate, int yCoordinate, int altitude, DateTime timestamp)
        {
            Tag = tag;
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            Altitude = altitude;
            Timestamp = timestamp;
            HorizontalVelocity = 0;
            CompassCourse = 0;
        }

       

    }
}
