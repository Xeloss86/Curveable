using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Common;
namespace Core.InterestRateCurve.BuildingBlock
{
    public abstract class OnePaymentStyle:BuildingBlock
    {
        public BusinessDayAdjustment BusDayAdjPay { get; set; }

        //No Parameter Constructor
        protected OnePaymentStyle() : base() { }

        //Constructor
        public OnePaymentStyle(DateTime refDate, double rateValue, string tenor):base(refDate, rateValue, tenor)
        { 
        
        }

    }
}
