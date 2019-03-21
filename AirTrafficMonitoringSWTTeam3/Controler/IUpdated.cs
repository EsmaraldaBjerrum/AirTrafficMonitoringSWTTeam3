﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoringSWTTeam3.Events;

namespace AirTrafficMonitoringSWTTeam3.Controler
{
    public interface IUpdated
    {
        event EventHandler<UpdatedDataEvent> UpdatedDataEvent;
    }
}
