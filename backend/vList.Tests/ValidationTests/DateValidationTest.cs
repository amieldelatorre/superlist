using vlist.Validation;

namespace vList.Tests.ValidationTests
{
    internal class DateValidationTest
    {
        [TestCaseSource(nameof(DateValidationSourceProvider))]
        public void DatesValidationTest(string inputDate, bool expected)
        {
            bool actual = DateValidation.IsValidDate(inputDate);

            Assert.That(actual, Is.EqualTo(expected));
        }

        internal static object[] DateValidationSourceProvider =
        {
            new object [] { "2023-07-27T00:00:00+02", true },
            new object [] { "2023-07-08T01:44:15+00", true },
            new object [] { "2023-07-08T01:44:15+00:00", true },
            new object [] { "2023/07/27T00:00:00+02", false },
            new object [] { "2023-07-27T00:00:00+2", false },
            new object [] { "2023-07-08T13:35:06.9378172+12:00", false }
        };

    }
}
