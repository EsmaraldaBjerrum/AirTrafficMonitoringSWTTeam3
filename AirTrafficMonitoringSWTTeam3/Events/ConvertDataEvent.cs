using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoringSWTTeam3.Events
{
    class ConvertDataEvent : EventArgs
    {
        public ConvertDataEvent(List<Aircraft> convertData)
        {
            ConvertData = convertData;
        }

        public List<Aircraft> ConvertData { get; }
        
        
    }
}
