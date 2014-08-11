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
            Date actualDate = sourceDate.ModifiedFollowing();
            Assert.AreEqual(expectedModFollowingDate, actualDate);
        }

    }
}
