using System;
using System.Collections.Generic;

namespace AirTrafficMonitoringSWTTeam3
{
    public partial class Calculator
    {
        public class AirspaceDataEventArgs : EventArgs
        {
            public AirspaceDataEventArgs(List<Aircraft> transponderData)
            {
                TransponderData = transponderData;
            }
            public List<Aircraft> TransponderData { get; }
        }
    }
}
