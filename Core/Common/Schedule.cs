using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Common

{
    public class Schedule
    {
        public DateTime[] FromDates { get; set; }
        public DateTime[] ToDates { get; set; }
        public DateTime[] PayDates { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string StringTenor { get; set; }

    }
}
