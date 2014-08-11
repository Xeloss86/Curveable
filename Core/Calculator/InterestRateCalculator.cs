using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Calculator
{
    public class InterestRateCalculator
    {
        public InterestRateCalculator(int numberPeriods, double interest)
        {
            this.Rate = interest;
            this.NumberOfPeriods = numberPeriods;
        }

        public double Rate { get; set; }
        public int NumberOfPeriods { get; set; }

        public double FutureValue(double principal)
        {
            double factor = 1.0 + this.Rate;
            return principal*Math.Pow(factor, NumberOfPeriods);
        }


        public double FutureValue(double principal, int m)
        {
            double R = Rate/m;
            int newPeriods = m*this.NumberOfPeriods;
            InterestRateCalculator myBond = new InterestRateCalculator(newPeriods, R);

            return myBond.FutureValue(principal);
        }

        public double OrdinaryAnnuity(double A)
        {
            double factor = 1.0 + Rate;
            return A*((Math.Pow(factor, NumberOfPeriods)/Rate));
        }

        public double PresentValue(double futureValue)
        {
            double factor = 1.0 + Rate;
            return futureValue*(1.0/Math.Pow(factor, NumberOfPeriods));
        }

        public double PresentValueConstant(double coupon)
        {
            double factor = 1.0 + Rate;
            double presentValue = 0.00;
            for(int t=0; t <  NumberOfPeriods; t++)
            {
                presentValue += 1.0/Math.Pow(factor, t + 1);
            }

            return presentValue * coupon;
        }

        public double PresentValueOrdianyAnnuity(double a)
        {
            double factor = 1.0 + Rate;
            double numerator = 1.0 - (1.0/Math.Pow(factor, NumberOfPeriods));
            return (a*numerator)/Rate;
        }



    }
}
