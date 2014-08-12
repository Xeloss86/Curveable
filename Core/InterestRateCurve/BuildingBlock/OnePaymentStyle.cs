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
        public OnePaymentStyle(Date refDate, double rateValue, string tenor): 
            base(refDate, rateValue, tenor)
        {
            //Getting the last date
            this.EndDate = this.RefDate.AddPeriod(tenor, false).GetBusinessDayAdjustment(BusDayAdjPay);
        }

    }
}
