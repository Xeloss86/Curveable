using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Common
{
    public struct Period
    {
        //Data member
        public int tenor; //tenor as int( i.e. 1,3,6....)
        public TenorType tenorType; //tenor type

        public Period(int tenor, TenorType tenorType)
        {
            this.tenor = tenor;
            this.tenorType = tenorType;
        }

        public Period(string period)
        {
            char maturity = period[period.Length - 1];
            int nPeriods = int.Parse(period.Remove(period.Length - 1, 1));
            tenor = nPeriods;
            tenorType = (TenorType)Enum.Parse(typeof(TenorType), Convert.ToString(maturity).ToUpper());
        }

        //Method get string format
        public string GetPeriodStringFormat()
        {
            return tenor + (tenorType.ToString()).ToLower();
        }

        //Interval in time 1y=1, 6m = 0.5 ... 18 = 1.5
        public double TimeInterval()
        {
            return ((double)this.tenor / (double)this.tenorType);
        }

    }
}
