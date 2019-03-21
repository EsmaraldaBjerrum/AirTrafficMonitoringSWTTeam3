using System;
using System.Collections.Generic;
using AirTrafficMonitoringSWTTeam3.Events;

namespace AirTrafficMonitoringSWTTeam3.Controler
{
    class Filter : IFilter
    {
        public event EventHandler<FilterDataEvent> FilterDataEvent;
        private List<Aircraft> filterList;
        private IConverter _converter;

        public Filter(IConverter converter)
        {
            _converter = converter;
            _converter.ConvertDataEvent += FilterMethod;
        }

        public void FilterMethod(object sender, ConvertDataEvent e)
        {
            filterList = new List<Aircraft>();

            foreach (Aircraft data in e.ConvertData)
            {
                if (data.XCoordinate <= 85000 && data.YCoordinate <= 85000)
                     filterList.Add(data);
            }

            if (filterList.Count != 0)
            FilterDataEvent?.Invoke(this, new FilterDataEvent(filterList));
        }
    }
}