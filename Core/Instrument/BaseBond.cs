using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Common;

namespace Core.Instrument
{
    public abstract class BaseBond
    {
        protected DateTime QuoteDate { get; set; }
        protected DateTime SettlementDate { get; set; }
        protected int SettlementDaysLag { get; set; }
        protected Schedule Schedule { get; set; }
        protected DateTime StartDate { get; set; }
        protected DateTime EndDate { get; set; }
        protected double FaceValue { get; set; }
        protected DayCount DayCount { get; set; }
        protected int NextCouponIndex { get; set; }//index of next coupon date
        protected double CurrentCoupon { get; set; } //coupon rate 
        protected double[] CashFlows { get; set; } //Coupon cash flows amount, no face value at the end
        protected DateTime[] CashFlowDates { get; set; }
        protected DateTime LastCouponDate { get; set; }
        protected DateTime NextCouponDate { get; set; }
        protected double AccruedInterest { get; set; }

    }


}
