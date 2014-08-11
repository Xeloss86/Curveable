using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Calculator;

namespace Core.Instrument
{
    public class Bond
    {
        private InterestRateCalculator eng;

        public double Rate { get; set; }
        public int NumberOfPeriods { get; set; }
        public double Coupon { get; set; }

        public Bond(int numberPeriods, double interest, double coupon, int paymentPerYear)
        {
            NumberOfPeriods = numberPeriods;
            Rate = interest/(double) paymentPerYear;
            Coupon = coupon;

            eng = new InterestRateCalculator(this.NumberOfPeriods, Rate);
        }

        public Bond(InterestRateCalculator calculator, double coupon, int paymentPerYear)
        {
            eng = calculator;
            this.Coupon = coupon;
            NumberOfPeriods = calculator.NumberOfPeriods;
            Rate = eng.Rate/(double) paymentPerYear;
        }

        public double Price(double redemptionValue)
        {
            //present value of coupon payments
            double pvCoupon = eng.PresentValueConstant(Coupon);

            //present value of redemtion value
            double pvPar = eng.PresentValueConstant(redemptionValue);

            return pvCoupon + pvPar;
        }


    }
}
