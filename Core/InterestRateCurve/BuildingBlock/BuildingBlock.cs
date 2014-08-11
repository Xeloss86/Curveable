using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Common;

namespace Core.InterestRateCurve.BuildingBlock
{
    public class BuildingBlock: IBuildingBlockFactory
    {
        public DateTime RefDate { get; set; }
        public DateTime EndDate { get; set; }
        public Period Tenor { get; set; }
        public double RateValue { get; set; }
        public BuildingBlockType BuildingBlockType { get; set; }
        public Dc DayCount { get; set; }


        protected BuildingBlock() { }

        public BuildingBlock(DateTime refDate, double rateValue, string tenor)
        {
            this.RefDate = RefDate;
            
        }

        //According to BuildingBlockType will create the right BuildingBlock
        public BuildingBlock CreateBuildingBlock(DateTime refDate, double rateValue, string tenor, BuildingBlockType blockType)
        {
            //switch (blockType)
            //{ 
            //    case BuildingBlockType.EURZERORATE:
            //        return new EurZeroRate(refDate, refValue, tenor);
            //        break;

                
            //}
            return null;
        }
    }
}
