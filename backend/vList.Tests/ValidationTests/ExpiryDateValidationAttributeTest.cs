using vlist.Validation;

namespace vList.Tests.ValidationTests
{
    internal class ExpiryDateValidationAttributeTest
    {
        [TestCaseSource(nameof(ExpiryDateAttributeSourceProvider))]
        public void ExpiryDateAttributeTest(string inputDate, bool expected)
        {
            ExpiryDateValidationAttribute expDateAttr = new ExpiryDateValidationAttribute();
            bool actual = expDateAttr.IsValid(inputDate);

            Assert.That(actual, Is.EqualTo(expected));
        }

        internal static object[] ExpiryDateAttributeSourceProvider =
        {
            new object [] { (DateTime.Now).AddMinutes(70).ToString(DateValidation.DateFormat), true },
            new object [] { (DateTime.Now).AddDays(20).ToString(DateValidation.DateFormat), true },
            new object [] { (DateTime.Now).AddDays(-20).ToString(DateValidation.DateFormat), false },
            new object [] { (DateTime.Now).AddMinutes(20).ToString(DateValidation.DateFormat), false },
            new object [] { (DateTime.Now).AddMinutes(20).ToString(DateValidation.DateFormat), false },
            new object [] { (DateTime.Now).ToString(DateValidation.DateFormat), false },
            new object [] { "2023/07/27T00:00:00+02", false },
        };
    }
}
