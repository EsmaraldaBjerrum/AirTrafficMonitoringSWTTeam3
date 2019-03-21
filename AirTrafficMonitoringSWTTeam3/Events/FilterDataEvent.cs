using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoringSWTTeam3.Events
{
    public class FilterDataEvent : EventArgs
    {
        public FilterDataEvent(List<Aircraft> filterData)
        {
            FilterData = filterData;
        }

        public List<Aircraft> FilterData { get; }
    }
}
