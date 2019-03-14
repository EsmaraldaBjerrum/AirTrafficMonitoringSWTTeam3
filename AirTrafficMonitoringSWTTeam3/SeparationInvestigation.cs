using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AirTrafficMonitoringSWTTeam3
{
    class SeparationInvestigation
    {
        private Calculator _calculator;

        public SeparationInvestigation(Calculator calculator)
        {
            _calculator = calculator;
            _calculator.AirspaceDataEvent += Separation;
        }

        public void Separation(object sender, AirspaceDataEventArgs e)
        {
            foreach (Aircraft aircarft in e.TransponderData)
            {
                for (int i = 0; i < e.TransponderData.Count; i++)
                {
                   
                }
            }

            
        }
    }
}
