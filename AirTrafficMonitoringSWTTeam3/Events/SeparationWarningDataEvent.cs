using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoringSWTTeam3
{
    public class SeparationWarningDataEvent : EventArgs
    {
       public List<SeparationWarningData> TransponderData { get; }
        public SeparationWarningDataEvent(List<SeparationWarningData> transponderData)
        {
           TransponderData = transponderData;
        }
    }
}
