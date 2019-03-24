using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoringSWTTeam3.Events
{
    public class SeparationWarningDataEvent : EventArgs
    {
        public List<SeparationWarningData> TransponderData;
        //Havde get-metode tidligere, ser om den kan undværes, da den ikke testes
        public SeparationWarningDataEvent(List<SeparationWarningData> transponderData)
        {
           TransponderData = transponderData;
        }
    }
}
