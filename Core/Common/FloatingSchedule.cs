using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Common;

namespace Core.Common
{
    public class FloatingSchedule : Schedule
    {
        //Date when the rates are fixed
        public Date[] FixingDates { get; set; }

        //Number of business days in the leg
        public int NumberOfBusDays { get; set; }

        //true if leg is from endDate false if leg from start Date
        public bool Arrears { get; set; }


        public FloatingSchedule(Date startDate, Date endDate, string payFreq, Rule scheduleRule, BusinessDayAdjustment rollConv, BusinessDayAdjustment payConv, string paymentLag, bool isArreaFixing, int busDayCount)
            : base(startDate, endDate, payFreq, scheduleRule, rollConv, payConv, paymentLag)
        {
            this.Arrears = isArreaFixing;
            this.NumberOfBusDays = busDayCount;


        }

        // private Method used in constructor
        private void BuildFloatingSchedule()
        {
            if (Arrears)
            {
                this.FixingDates = Date.GetBusinessDayShifted(this.ToDates, this.NumberOfBusDays);
            }
            else
            { // in advance
                this.FixingDates = Date.GetBusinessDayShifted(this.FromDates, this.NumberOfBusDays);
            }
        }

        // Method to visualise schedule on console
        public void PrintSchedule()
        {
            int n = FromDates.GetLength(0); // number of element
            Console.WriteLine("\nFixing \t\t  From  \t\t  To \t\t  PayDate");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("{0:ddd dd-MMM-yyyy}___{1:ddd dd-MMM-yyyy}___{2:ddd dd-MMM-yyyy}___{3:ddd dd-MMM-yyyy}",
                    FixingDates[i].DateValue, FromDates[i].DateValue, ToDates[i].DateValue, PayDates[i].DateValue);
            }
        }
    }
}
