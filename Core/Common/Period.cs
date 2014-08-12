using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Common
{
    public struct Period
    {
        //Data member
        public int Tenor; //tenor as int( i.e. 1,3,6....)
        public TenorType TenorType; //tenor type

        public Period(int tenor, TenorType tenorType)
        {
            Tenor = tenor;
            TenorType = tenorType;
        }

        public Period(string period)
        {
            char maturity = period[period.Length - 1];
            int nPeriods = int.Parse(period.Remove(period.Length - 1, 1));
            Tenor = nPeriods;
            TenorType = (TenorType)Enum.Parse(typeof(TenorType), Convert.ToString(maturity).ToUpper());
        }

        //Method get string format
        public string GetPeriodStringFormat()
        {
            return Tenor + (TenorType.ToString()).ToLower();
        }

        //Interval in time 1y=1, 6m = 0.5 ... 18 = 1.5
        public double TimeInterval()
        {
            return ((double)this.Tenor / (double)this.TenorType);
        }

    }
}
