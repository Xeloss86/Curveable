using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Common;
namespace Core.InterestRateCurve.BuildingBlock
{
    public class BuildingBlockFactory: IBuildingBlockFactory
    {
        public BuildingBlock CreateBuildingBlock(Date refDate, double rateValue, string tenor, BuildingBlockType type)
        {
            switch (type)
            { 
                case BuildingBlockType.EURZERORATE:
                    return new EurZeroRate(refDate, rateValue, tenor);
                case BuildingBlockType.EURDEPO:

                    break;
                case BuildingBlockType.EURSWAP3M:

                    break;
                case BuildingBlockType.EURSWAP6M:

                    break;
                case BuildingBlockType.EONIASWAP:

                    break;
                case BuildingBlockType.EURBASIS6M3M:
                    break;

                default:
                    return null;
            }
            return null;
        }

        public BuildingBlock CreateEmptyBuildingBlock(BuildingBlockType type) { return null; }
    }
}
