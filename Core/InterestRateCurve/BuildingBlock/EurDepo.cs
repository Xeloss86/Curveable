using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Common;

namespace Core.InterestRateCurve.BuildingBlock
{
    public class EurDepo:OnePaymentStyle
    {
        public EurDepo() : base() { }

        public EurDepo(Date refDate, double rateValue, string tenor):base(refDate, rateValue, tenor)
        { 
            
        }

        public override void LoadSpecifications()
        {
            this.BuildingBlockType = BuildingBlockType.EURDEPO;
            this.BusDayAdjPay = BusinessDayAdjustment.ModifiedFollowing;
            this.DayCount = Dc._ACT_360;
        }
    }
}
