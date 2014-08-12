using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Common;
using Core.InterestRateCurve.BuildingBlock;
using Core.Instrument;
namespace Core.InterestRateCurve.BuildingBlock
{
    public class SwapStyle:BuildingBlock
    {
        public SwapLeg SwapLeg1 { get; set; }
        public SwapLeg SwapLeg2 { get; set; }

        public Schedule ScheduleLeg1 { get; set; }
        public Schedule ScheduleLeg2 { get; set; }

        public override void LoadSpecifications()
        {
            throw new NotImplementedException();
        }
    }
}
