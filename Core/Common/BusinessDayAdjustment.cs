using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Common
{
    public enum BusinessDayAdjustment
    {
        //ISDA
        Following,
        ModifiedFollowing,
        Preceding,
        Unadjusted
    }
}
