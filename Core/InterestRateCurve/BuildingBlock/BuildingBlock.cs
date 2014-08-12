using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Common;

namespace Core.InterestRateCurve.BuildingBlock
{
    public abstract class BuildingBlock
    {
        public Date RefDate { get; set; }
        public Date EndDate { get; set; }
        public Period Tenor { get; set; }
        public double RateValue { get; set; }
        public BuildingBlockType BuildingBlockType { get; set; }
        public Dc DayCount { get; set; }

        protected BuildingBlock() { }

        public BuildingBlock(Date refDate, double rateValue, string tenor)
        {
            this.RefDate = RefDate;
            this.RateValue = rateValue;
            this.Tenor = new Period(tenor);
            LoadSpecifications();
        }

        // Derived class should implement it
        abstract public void LoadSpecifications();

    }
}
