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

        public bool IsDateFallsOnWeekends(DateTime date)
        {
            return (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday);
        }

        public DateTime CloneDateValue()
        {
            return new DateTime(DateValue.Year, DateValue.Month, DateValue.Day);
        }

        // Method Member: adjust the date according to modified following convection:
        // the date is rolled to the next business day, unless doing so you will find a date in the next calendar month,
        // in which case the date is rolled on the previous business day. (see http: // en.wikipedia.org/wiki/Accrued_interest
        public Date ModifiedFollowing()
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
    }
}
