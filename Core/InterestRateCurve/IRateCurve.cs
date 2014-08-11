using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.InterestRateCurve
{
    public interface IRateCurve
    {
        //return referenced date
        DateTime RefDate();

        //return discount factor for target date
        double Df(DateTime targetDate);

        //return forward rate starting on StartDate for a tenor defined directly in the class
        double Fwd(DateTime startDate);
 
        //return forward start swap as same building block used in building curve recalculated starting on custom StartDate, Tenor is the tenor of swap
        double SwapFwd(DateTime startDate, string tenor);

        //Return SwapStyle used for bootstrapping, it is swap type used as inputs (i.e. EurSwapVs6m, EurSwapVs3m, ...)


    }
}
