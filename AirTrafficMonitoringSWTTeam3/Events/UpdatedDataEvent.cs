using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoringSWTTeam3.Events
{
    public class UpdatedDataEvent : EventArgs
    {
        public UpdatedDataEvent(List<Aircraft> updatedData)
        {
            UpdatedData = updatedData;
        }

        public List<Aircraft> UpdatedData { get; }
    }
}
