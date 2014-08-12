using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Common;
namespace Core.InterestRateCurve.BuildingBlock
{
    public class EurZeroRate: OnePaymentStyle
    {
        public EurZeroRate() : base() { }

        public EurZeroRate(Date refDate, double rateValue, string tenor)
            : base(refDate, rateValue, tenor)
        { 
        }

        public override void LoadSpecifications()
        {
            this.BuildingBlockType = BuildingBlockType.EURZERORATE;
            this.BusDayAdjPay = BusinessDayAdjustment.ModifiedFollowing;
            this.DayCount = Dc._ACT_365;
        }
    }
}
