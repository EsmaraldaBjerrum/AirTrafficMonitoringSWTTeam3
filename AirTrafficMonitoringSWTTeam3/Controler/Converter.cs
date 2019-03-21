using System;
using System.Collections.Generic;
using AirTrafficMonitoringSWTTeam3.Events;
using TransponderReceiver;

namespace AirTrafficMonitoringSWTTeam3.Controler
{
    class Converter : IConverter
    {
        public event EventHandler<ConvertDataEvent> ConvertDataEvent;
        private ITransponderReceiver _transponderReceiver;
        private List<Aircraft> convertedDataList;

        public Converter(ITransponderReceiver transponderReceiver)
        {
            _transponderReceiver = transponderReceiver;
            _transponderReceiver.TransponderDataReady += ConvertMethod;

        }

        public void ConvertMethod(object sender, RawTransponderDataEventArgs e)
        {
            convertedDataList = new List<Aircraft>();

            foreach (var data in e.TransponderData)
            {
                string[] aircraftdata = new string[5];
                aircraftdata = data.Split(';');


                Aircraft aircraft = new Aircraft(aircraftdata[0], Convert.ToInt32(aircraftdata[1]),
                    Convert.ToInt32(aircraftdata[2]),
                    Convert.ToInt32(aircraftdata[3]), DateTime.ParseExact(aircraftdata[4], "yyyyMMddHHmmssfff",
                        System.Globalization.CultureInfo.InvariantCulture));
                convertedDataList.Add(aircraft);
            }

            ConvertDataEvent?.Invoke(this, new ConvertDataEvent(convertedDataList));
        }
    }
}
