using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Common;

namespace Core.Common
{
    public class FloatingSchedule:Schedule
    {

        public DateTime[] FixingDates { get; set; }

        public int NumberOfBusDays { get; set; }

        //true if leg is from endDate false if leg from start Date
        public bool Arrears { get; set; }

    }
}
