using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Instrument;
namespace Core.Common
{
    public class Schedule
    {
        //Array of starting date of each period
        public Date[] FromDates { get; set; }
        //Array of end date for each period
        public Date[] ToDates { get; set; }
        //Array of payment date for each period
        public Date[] PayDates { get; set; }

        //Starting date of scehdule
        public Date StartDate { get; set; }

        //End Date of schedule
        public Date EndDate { get; set; }

        //Schedule Frequency (3m,6m,1y...)
        public string StringTenor { get; set; }

        //Rule for generating the period
        public Rule ScheduleGenerationRule { get; set; }

        //Business Day Adjustment Rule for roll of end date of each period
        public BusinessDayAdjustment RollConvention { get; set; }

        //Business Day Adjustment Rule for payment date of each period
        public BusinessDayAdjustment PayConvention { get; set; }

        //The leg between the end date of each period and the payment date as string (for example, 0d, 1d...)
        public string PayLag { get; set; }

        public Schedule(Date startDate, Date endDate, string payFreq, Rule scheduleRule, BusinessDayAdjustment rollConv,
            BusinessDayAdjustment payConv, string paymentLag)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.StringTenor = payFreq;
            this.ScheduleGenerationRule = scheduleRule;
            this.RollConvention = rollConv;
            this.PayConvention = payConv;
            this.PayLag = paymentLag;
        }

        public Schedule(Date startDate, Date endDate, BusinessDayAdjustment rollConv, BusinessDayAdjustment payConv, string paymentLag)
        {
            this.StartDate = startDate;
            this.EndDate = EndDate;
            this.StringTenor = "ONCE";
            this.ScheduleGenerationRule = Rule.Backward;
            this.RollConvention = rollConv;
            this.PayConvention = payConv;
            this.PayLag = paymentLag;

            this.FromDates = new Date[] { startDate };
            this.ToDates = new Date[] { endDate };
            this.PayDates = Date.GetShiftedDates(this.ToDates, this.PayLag);
            this.PayDates = Date.GetBusDayAdjustedDates(this.PayDates, this.PayConvention);
        }

        private void BuildSchedule()
        {
            var dates = new List<Date>();

            //Forward and backward handling
            if (this.ScheduleGenerationRule == Rule.Forward)
            {
                dates.Add(StartDate);
                while (EndDate.DateValue > dates.Last().DateValue) // loop until it hits the end dates
                {
                    dates.Add(dates.Last().AddPeriod(this.StringTenor, false)); //Add dates according to the string tenor;
                }

                if (dates.Last().DateValue > EndDate.DateValue)
                {
                    dates.Remove(dates.Last());
                    dates.Add(EndDate);
                }
            }
            else if (this.ScheduleGenerationRule == Rule.Backward)
            {
                dates.Add(EndDate); //Start from the End Date
                Period p = new Period(this.StringTenor);
                int i = 1;
                while (StartDate.DateValue < dates.Last().DateValue) //After start Date
                {
                    Period pp = new Period(p.Tenor * i, p.TenorType);
                    dates.Add(EndDate.SubPeriod(pp.GetPeriodStringFormat(), false));
                    i++;
                }

                if (dates.Last().DateValue < StartDate.DateValue)
                {
                    dates.Remove(dates.Last());
                    dates.Add(StartDate);
                }

                dates.Reverse();  //Reverse it to sort ascending
            }

            FillDataMember(dates);
        }

        private void FillDataMember(IList<Date> dates)
        { 
            int length = dates.Count;

            //Get in a Date Vector
            Date[] source = Date.GetBusDayAdjustedDates(dates, this.RollConvention);
            Date[] fromDates = new Date[length-1];
            Date[] toDates = new Date[length-1];

            Array.Copy(source, 0, fromDates, 0, length-1);
            Array.Copy(source, 1, toDates, 0, length-1);

            this.FromDates = fromDates;
            this.ToDates = toDates;
        }

        public double[] GetYearFractions(Dc dayCount)
        {
            return Date.GenerateYearFractionArray(this.FromDates, this.ToDates, dayCount);
        }

        public void PrintScheduleToConsole()
        {
            for (int i = 0; i < FromDates.Length; i++)
            {
                string msg = string.Format("{0:ddd dd-MMM-yyyy} ____ {1:ddd dd-MMM-yyyy} ____ {2:ddd dd-MMM-yyyy}", FromDates[i].DateValue, ToDates[i].DateValue, PayDates[i].DateValue);
            }
        }
    }
}
