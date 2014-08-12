using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Common;

namespace Core.InterestRateCurve.BuildingBlock
{
    public interface IBuildingBlockFactory
    {
        BuildingBlock CreateBuildingBlock(Date refDate, double rateValue, string tenor, BuildingBlockType type);
        BuildingBlock CreateEmptyBuildingBlock(BuildingBlockType type);
    }
}
