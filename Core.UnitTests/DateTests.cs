using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Core.Common;
namespace Core.UnitTests
{
    [TestFixture]
    public class DateTests
    {
        [TestCase(2014, 8, 12, 41863)]
        [TestCase(2014, 1, 14, 41653)]
        public void Test_SerialValue_Property_Returns_Correct_Value(int year, int month, int day, double expectedExcelDate)
        {
            Date excelDate = new Date(year, month, day);
            double actualValue = excelDate.SerialValue;
            Assert.AreEqual(expectedExcelDate, actualValue);
        }

        [TestCase(41863, 1, 41864)]
        [TestCase(41863, 4, 41869)]
        [TestCase(41863, -2, 41859)]
        public void Test_Add_Bus_Days_Returns_Correct_Date(double originalDateValue, int daysToAdd, double expectedDate)
        {
            Date targetDate = new Date(originalDateValue);
            Assert.AreEqual(targetDate.AddBusDays(daysToAdd).SerialValue, expectedDate);
        }

        [TestCase(41881, 41880)]
        public void Test_Modified_Following_Returns_Correct_Excel_Date(double sourceDateVal, double expectedDateVal)
        {
            Date sourceDate = new Date(sourceDateVal);
            Date expectedModFollowingDate = new Date(expectedDateVal);
            Date actualDate = sourceDate.GetModifiedFollowing();
            Assert.AreEqual(expectedModFollowingDate, actualDate);
        }

        [TestCase(2014, 9,  41899)]
        [TestCase(2014, 12, 41990)]
        [TestCase(2015, 3,  42081)]
        [TestCase(2015, 6,  42172)]
        public void Test_Generate_Imm_Date_Returns_Correct_Date(int year, int month, double expected)
        {
            Date actual =  Date.GenerateImmDate(year, month);
            Date expectedDate = new Date(expected);
            Assert.AreEqual(expectedDate, actual);
        }

        [TestCase(41863, "1d", 41864)]
        [TestCase(41863, "1w", 41870)]
        [TestCase(41863, "1m", 41894)]
        public void Test_Add_Period_Returns_Correct_Date(double date, string period, double expectedDate)
        {
            Date d = new Date(date);
            Date actual = d.AddPeriod(period, false);
            Date expected = new Date(expectedDate);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(41863, BusinessDayAdjustment.Unadjusted, 41863)]
        [TestCase(41863, BusinessDayAdjustment.Preceding, 41863)]
        [TestCase(41861, BusinessDayAdjustment.Preceding, 41859)]
        [TestCase(41861, BusinessDayAdjustment.Following, 41862)]
        [TestCase(41881, BusinessDayAdjustment.ModifiedFollowing, 41880)]
        public void Test_Bus_Day_Adjustment_Returns_Valid_Results(double date, BusinessDayAdjustment convention, double expected)
        {
            Date actual = new Date(date).GetBusinessDayAdjustment(convention);
            Assert.AreEqual(expected, actual.SerialValue);
        }

        [TestCase(41863,"1d", 41864)]
        public void Test_Shift_Dates_Returns_Valid_Results(double date, string tenor, double expected)
        {
            Date[] shifted = Date.GetShiftedDates(new List<Date> { new Date(date) }, tenor);
            Assert.AreEqual(expected, shifted[0].SerialValue);
        }

        [TestCase(41881, BusinessDayAdjustment.ModifiedFollowing, 41880)]
        public void Test_Get_Bus_Day_Adjusted_Dates_Returns_Valid_Result(double date, BusinessDayAdjustment convention, double expected)
        {
            Date[] adjusted = Date.GetBusDayAdjustedDates(new List<Date> { new Date(date) }, convention);
            Assert.AreEqual(expected, adjusted[0].SerialValue);
        }

        [TestCase(41863, 41864, Dc._ACT_365, 0.002739726)]
        [TestCase(41863, 41864, Dc._ACT_360, 0.002777777)]
        public void Test_Calculate_YearFraction_Returns_Valid_Result(double date1, double date2, Dc dayCount, double expected)
        {
            Date date1Obj = new Date(date1);
            Date date2Obj = new Date(date2);
            double actual = date1Obj.CalcYearFraction(date2Obj, dayCount);
            Assert.AreEqual(Math.Round(expected, 6), Math.Round(actual, 6));
        }
    }
}
