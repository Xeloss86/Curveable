using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Instrument;
using Core.Common;

namespace Core.Instrument
{
    public class SwapLeg
    {
        public Rule SwapScheduleGenerationRule { get; set; }
        public string PayFreq { get; set; }
        public BusinessDayAdjustment RollConvention { get; set; }
        public string SwapPaymentLag { get; set; }
        public BusinessDayAdjustment PayConvention { get; set; }
        public Dc DayCount { get; set; }
        public FixOrFloat FixedOrFloating { get; set; }
        public string UnderlyingRateTenor { get; set; }

        public SwapLeg(Rule scheduleRule, string payFreq, BusinessDayAdjustment rollConvention, BusinessDayAdjustment payConvention,
            string paymentLag, Dc dayCount, FixOrFloat fixedOrFloating, string underlyingRateTenor)
        {
            if (FixedOrFloating == FixOrFloat.Fixed && !string.IsNullOrEmpty(underlyingRateTenor))
            {
                throw new ArgumentException("Fixed Leg cannot have underlying tenor specified!");
            }

            this.SwapScheduleGenerationRule = scheduleRule;
            this.PayFreq = PayFreq;
            this.RollConvention = rollConvention;
            this.PayConvention = PayConvention;
            this.SwapPaymentLag = paymentLag;
            this.DayCount = dayCount;
            this.FixedOrFloating = fixedOrFloating;
            this.UnderlyingRateTenor = underlyingRateTenor;
        }
    }
}
