using MongoDB.Driver.Core.Events;
using vlist.Validation;

namespace vList.Tests.ValidationTests
{
    internal class ExpiryDateValidationAttributeTest
    {
        [TestCaseSource(nameof(ExpiryDateAttributeSourceProvider))]
        public void ExpiryDateAttributeTest(string inputDate, bool expected, string added)
        {
            ExpiryDateValidationAttribute expDateAttr = new ExpiryDateValidationAttribute();
            bool actual = expDateAttr.IsValid(inputDate);

            Assert.That(actual, Is.EqualTo(expected));
        }

        internal static object[] ExpiryDateAttributeSourceProvider =
        {
            new object [] { (DateTime.Now).AddMinutes(70).ToString(DateValidation.DateFormats[0]), true, "+70min" },
            new object [] { (DateTime.Now).AddDays(20).ToString(DateValidation.DateFormats[1]), true, "+20days" },
            new object [] { (DateTime.Now).AddDays(-20).ToString(DateValidation.DateFormats[2]), false, "-20days" },
            new object [] { (DateTime.Now).AddMinutes(20).ToString(DateValidation.DateFormats[0]), false, "+20mins"},
            new object [] { (DateTime.Now).AddMinutes(20).ToString(DateValidation.DateFormats[1]), false, "+20mins" },
            new object [] { (DateTime.Now).ToString(DateValidation.DateFormats[2]), false, "+0" },
            new object [] { "2023/07/27T00:00:00+02", false, "static" },
        };
    }
}
