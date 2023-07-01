using vlist.Validation;

namespace vList.Tests.ValidationTests
{
    internal class DateValidationTest
    {
        [TestCaseSource(nameof(DateFormatAttributeSourceProvider))]
        public void DateFormatAttributeTest(string inputDate, bool expected)
        {
            bool actual = DateValidation.IsValidDate(inputDate);

            Assert.That(actual, Is.EqualTo(expected));
        }



        internal static object[] DateFormatAttributeSourceProvider =
        {
            new object [] { "2023-07-27T00:00:00+02", true },
            new object [] { "2023/07/27T00:00:00+02", false },
            new object [] { "2023-07-27T00:00:00+2", false }
        };

    }
}
