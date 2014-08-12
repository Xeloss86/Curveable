using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Common
{
    public class Date
    {
        #region Constructors

        public DateTime DateValue{get;set;}
        
        public Date() { }

        public Date(int year, int month, int day)
        {
            DateValue = new DateTime(year, month, day);
        }

        public Date(DateTime dateValue)
        {
            DateValue = dateValue;
        }

        public Date(Date date) : this(date.DateValue) { }

        // Constructor using Excel serial number i.e for 15 Sep 10  use Date(40436)
        public Date(double excelSerialDate)
        {
            DateTime excelStartingDate = new DateTime(1899, 12, 30);
            DateValue = excelStartingDate.AddDays(excelSerialDate);
        }

        #endregion

        public double SerialValue
        {
            get 
            {
                DateTime starting = new DateTime(1899, 12, 30);
                TimeSpan timeSpan = DateValue.Subtract(starting);
                return (double)timeSpan.Days;
            }

            set 
            {
                DateTime starting = new DateTime(1899, 12, 30);
                DateValue = starting.AddDays(value);
            }
        }

       
        //Add Period to this date ("nd" will add "n" days, "nm" will add "n" months, "ny" will add "n" years)
        //modified following can be considered or not
        public Date AddPeriod(string periodStr, bool modFoll)
        {
            Period period = new Period(periodStr);
            TenorType tenorType = period.TenorType;
            DateTime date = this.DateValue;
            Date output = new Date();
            switch (tenorType)
            { 
                case TenorType.D:
                    output = new Date(this.DateValue.AddDays(period.Tenor));
                    break;
                case TenorType.W:
                    output = new Date(this.DateValue.AddDays(period.Tenor * 7));
                    break;
                case TenorType.M:
                    output = new Date(this.DateValue.AddMonths(period.Tenor));
                    break;
                case TenorType.Y:
                    output = new Date(this.DateValue.AddYears(period.Tenor));
                    break;
            }

            if (modFoll) output = output.GetModifiedFollowing();
            return output;
        }

        public Date SubPeriod(string period, bool modFoll)
        {
            Period p = new Period(period);
            TenorType tenorType = p.TenorType;
            DateTime dateTime = CloneDateValue();
            Date output = new Date();

            switch (tenorType)
            { 
                case TenorType.D:
                    output = new Date(this.DateValue.AddDays(-p.Tenor));
                    break;
                case TenorType.W:
                    output = new Date(this.DateValue.AddDays(-p.Tenor * 7));
                    break;
                case TenorType.M:
                    output = new Date(this.DateValue.AddMonths(-p.Tenor));
                    break;
                case TenorType.Y:
                    output = new Date(this.DateValue.AddYears(-p.Tenor));
                    break;
                default:
                    break;
            }
            if (modFoll) output = output.GetModifiedFollowing();
            return output;
        }


        public Date AddBusDays(int days)
        {
            DateTime outDate = CloneDateValue();
            int iterations = Math.Abs(days);
            int increment = Math.Sign(days);
            for (int i = 0; i < iterations; i++)
            {
                outDate = outDate.AddDays(increment);
                if (IsDateFallsOnWeekends(outDate)) i -= 1;
            }
            Date result = new Date(outDate);
            return result;
        }

        public Date AddMonths(int months)
        {
            Date output = new Date(DateValue.AddMonths(months));
            return output;
        }

        public Date AddWorkingDays(int days)
        {
            DateTime output = new DateTime();
            output = DateValue;
            int iterations = Math.Abs(days);
            int increment = Math.Sign(days);
            for (int i = 0; i < iterations; i++)
            {
                output = output.AddDays(increment);
                if(IsDateFallsOnWeekends(output))i-=1;
            }
            return new Date(output);
        }

        public Date AddDays(int days)
        {
            Date output = new Date(DateValue.AddDays(days));
            return output;
        }

        public Date AddYears(int years)
        {
            Date output = new Date(DateValue.AddYears(years));
            return output;
        }

        public bool IsDateFallsOnWeekends(DateTime date)
        {
            return (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday);
        }

        public DateTime CloneDateValue()
        {
            return new DateTime(DateValue.Year, DateValue.Month, DateValue.Day);
        }

        public Date Clone()
        {
            return new Date(this.SerialValue);
        }

        // Method Member: adjust the date according to modified following convection:
        // the date is rolled to the next business day, unless doing so you will find a date in the next calendar month,
        // in which case the date is rolled on the previous business day. (see http: // en.wikipedia.org/wiki/Accrued_interest
        public Date GetModifiedFollowing()
        {
            Date output = new Date();
            DayOfWeek dayOfWeek = DateValue.DayOfWeek;

            if (dayOfWeek == DayOfWeek.Sunday)
            {
                if (DateValue.Month == DateValue.AddDays(1).Month)
                {
                    output.DateValue = DateValue.AddDays(1);
                    return output;
                }
                else
                {
                    output.DateValue = DateValue.AddDays(-2);
                    return output;
                }
            }

            if (dayOfWeek == DayOfWeek.Saturday)
            {
                if (DateValue.Month == DateValue.AddDays(2).Month)
                {
                    output.DateValue = DateValue.AddDays(2);
                    return output;
                }
                else 
                {
                    output.DateValue = DateValue.AddDays(-1);
                    return output;
                }
            }

            return this;
        }

        public static Date[] GetShiftedDates(IList<Date> datesToBeShifted, string periodString)
        {
            if (datesToBeShifted == null || datesToBeShifted.Count == 0)
            {
                throw new ArgumentNullException("Dates cannot be null or size of 0!");
            }

            if (string.IsNullOrEmpty(periodString))
            {
                throw new ArgumentNullException("PeriodString is not given!");
            }

            int count = datesToBeShifted.Count;
            Date[] shiftedArray = new Date[count];
            for (int i = 0; i < count; i++)
            {
                shiftedArray[i] = datesToBeShifted[i].AddPeriod(periodString, false);
            }
            return shiftedArray;
        }

        public static Date[] GetBusinessDayShifted(IList<Date> datesToBeShifted, int busDays)
        {
            Date[] output = new Date[datesToBeShifted.Count];
            for (int i = 0; i < datesToBeShifted.Count; i++)
            {
                output[i] = datesToBeShifted[i].AddWorkingDays(busDays);
            }
            return output;
        }


        public static Date[] GetBusDayAdjustedDates(IList<Date> datesToBeAdjusted, BusinessDayAdjustment convention)
        {
            if (datesToBeAdjusted == null || datesToBeAdjusted.Count == 0)
            {
                throw new ArgumentNullException("Dates cannot be null or size of 0!");
            }

            int count = datesToBeAdjusted.Count;
            Date[] output = new Date[count];
            for (int i = 0; i < count; i++)
            {
                output[i] = datesToBeAdjusted[i].GetBusinessDayAdjustment(convention);
            }

            return output;
        }

        public Date GetBusinessDayAdjustment(BusinessDayAdjustment conv)
        {
            Date result = new Date(this);
            switch (conv)
            { 
                case BusinessDayAdjustment.Following:
                    result = GetFollowing();
                    break;
                case BusinessDayAdjustment.ModifiedFollowing:
                    result = GetModifiedFollowing();
                    break;
                case BusinessDayAdjustment.Preceding:
                    result =  GetPreceding();
                    break;
                case BusinessDayAdjustment.Unadjusted:
                    result =  GetUnadjusted();
                    break;
                default:
                    break;
            }

            return result;
        }

        public Date GetFollowing()
        {
            Date output = new Date(this);
            DayOfWeek dow = DateValue.DayOfWeek;

            if (dow == DayOfWeek.Sunday)
            {
                output.DateValue = DateValue.AddDays(1);
                return output;
            }

            if (dow == DayOfWeek.Saturday)
            {
                output.DateValue = DateValue.AddDays(2);
                return output;
            }

            return output;
        }


        public Date GetPreceding()
        {
            Date output = new Date(this);
            DayOfWeek dow = DateValue.DayOfWeek;
            if (dow == DayOfWeek.Sunday)
            {
                output.DateValue = this.DateValue.AddDays(-2);
                return output;
            }

            if (dow == DayOfWeek.Saturday)
            {
                output.DateValue = this.DateValue.AddDays(-1);
                return output;
            }

            return output;
        }

        public Date GetUnadjusted()
        {
            return Clone();
        }

        public Date IMMDate()
        {
            return GenerateImmDate(this.DateValue.Year, this.DateValue.Month);
        }

        public double CalcYearFraction(Date date, Dc dayCount)
        {
            double output = 0.00;
            switch (dayCount)
            { 
                case Dc._30_360:
                    output = CalcYearFraction_30_360(date);
                    break;
                case Dc._ACT_360:
                    output = CalcYearFraction_MM(date);
                    break;
                case Dc._ACT_365:
                    output = CalcYearFraction_365(date);
                    break;
                default:
                    throw new NotImplementedException(string.Format("Unable to Calculate the Year Fraction for {0} Day Count Convention Yet!", dayCount.ToString()));
            }

            return output;
        }

        //Calculate YearFraction between two dates using Act/365 convection
        public double CalcYearFraction_365(Date date)
        {
            return (date.SerialValue - this.SerialValue) / 365;
        }

        //Calculate YearFraction between two dates using Act/360 convection
        public double CalcYearFraction_MM(Date date)
        {
            return (date.SerialValue - this.SerialValue) / 360.00;
        }

        // 30-360 Bond Basis, see http: // www.isda.org/c_and_a/docs/30-360-2006ISDADefs.xls
        public double CalcYearFraction_30_360(Date date)
        {
            DateTime dateValue = date.DateValue;
            int d1 = this.DateValue.Day;
            int d2 = dateValue.Day;
            int m1 = this.DateValue.Month;
            int m2 = dateValue.Month;
            int y1 = this.DateValue.Year;
            int y2 = dateValue.Year;


            //Date adjustment for 30E/360
            if (d1 == 31) d1 = 30;
            if (d2 == 31 && d1 > 29) d2 = 30;

            return (double)(360.0 * (y2 - y1) + 30 * (m2 - m1) + (d2-d1))/360;
        }

        #region Operators

        public static Date operator +(Date date, int nDays)
        {
            DateTime outDate = new DateTime();
            outDate = date.DateValue.AddDays(nDays);
            Date result = new Date(outDate);
            return result;
        }

        public static Date operator -(Date date, int nDays)
        {
            DateTime outDate = new DateTime();
            outDate = date.DateValue.AddDays(-nDays);
            Date result = new Date(outDate);
            return result;
        }

        public static bool operator >(Date date1, Date date2)
        {
            return date1.SerialValue > date2.SerialValue;
        }

        public static bool operator <(Date date1, Date date2)
        {
            return date1.SerialValue < date2.SerialValue;
        }

        public static bool operator !=(Date date1, Date date2)
        {
            return !(date1 == date2);
        }

        public static bool operator ==(Date date1, Date date2)
        {
            if (Object.Equals(date1, null))
            {
                if (Object.Equals(date2, null))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return date1.Equals(date2);
            }
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Date date2 = (Date)obj;
            if (this.DateValue == date2.DateValue) return true;
            return false;
        }

        #region Static Utility Functions

        public static double[] GenerateYearFractionArray(IList<Date> dates1, IList<Date> dates2, Dc dayCount)
        { 
            //d1, d2 should have same dimension
            if (dates1.Count != dates2.Count)
            {
                throw new ArgumentException("Both Dates Array should have the same number of dates!");
            }
            double[] output = new double[dates2.Count];
            for (int i = 0; i < dates1.Count; i++)
            {
                output[i] = dates1[i].CalcYearFraction(dates2[i], dayCount);
            }

            return output;
        }


        public static Date GetFirstDayOfMonth(Date date)
        {
            return new Date(date.DateValue.Year, date.DateValue.Month, 1);
        }
        
        public static Date GetLastDayOfMonth(Date date)
        {
            Date firstDay = GetFirstDayOfMonth(date);
            return firstDay.AddMonths(1).AddDays(-1);
        }

        public static double GetEffectiveDaysInMonth(Date date)
        {
            return GetLastDayOfMonth(date).SerialValue - GetFirstDayOfMonth(date).SerialValue + 1;
        }

        public static Date GenerateImmDate(int year, int month)
        {
            DateTime output = new DateTime(year, month, 1).AddDays(-1);
            int wedCounter = 0;

            //loop to find the third wednesday
            while (wedCounter < 3)
            {
                output = output.AddDays(1);
                if (output.DayOfWeek == DayOfWeek.Wednesday)
                {
                    wedCounter++;
                }
            }
            return new Date(output);
        }

        public static void PrintDateVector(IList<Date> dates)
        {
            foreach (Date d in dates)
            {
                Console.WriteLine("{0:D}", d.DateValue);
            }
        }

        #endregion


        public override int GetHashCode()
        {
            string hashStr = DateValue.ToString();
            return hashStr.GetHashCode();
        }

       
    }
}
