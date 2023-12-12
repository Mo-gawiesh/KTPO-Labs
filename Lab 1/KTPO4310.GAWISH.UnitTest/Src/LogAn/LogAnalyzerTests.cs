using System;
using KTPO4310.GAWISH.Lib.src.LogAn;
using NUnit.Framework;

namespace KTPO4310.GAWISH.UnitTest.Src.LogAn
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        [Test]
        public void IsValidLogFileName_BadExtension_ReturnsFalse()
        {
            LogAnalyzer analyzer = new LogAnalyzer();
            bool result = analyzer.IsValidLogFileName("fileWithBadExtension.foo");

            Assert.False(result);
        }

        [Test]
        public void IsValidLogFileName_GoodExtensionUppercase_ReturnsTrue()
        {
            LogAnalyzer analyzer = new LogAnalyzer();
            bool result = analyzer.IsValidLogFileName("fileWithGoodUppercaseExtension.SLF");

            Assert.True(result);
        }

        [Test]
        public void IsValidLogFileName_GoodExtensionLowercase_ReturnsTrue()
        {
            LogAnalyzer analyzer = new LogAnalyzer();
            bool result = analyzer.IsValidLogFileName("fileWithGoodLowercaseExtension.slf");

            Assert.True(result);
        }

        [TestCase("fileWithValidExtension.slf")]
        [TestCase("fileWithValidExtension.SLF")]
        public void IsValidLogFileName_ValidExtension_ReturnsTrue(string fileName)
        {
            LogAnalyzer analyzer = new LogAnalyzer();
            bool result = analyzer.IsValidLogFileName(fileName);

            Assert.True(result);
        }

        [Test]
        public void IsValidLogFileName_EmptyFileName_Throws()
        {
            LogAnalyzer analyzer = new LogAnalyzer();
            var exception = Assert.Catch<Exception>(() => analyzer.IsValidLogFileName(""));
            StringAssert.Contains("name is empty", exception.Message);
        }

        [TestCase("fileWithNotValidExtension.foo", false)]
        [TestCase("fileWithValidExtension.SLF", true)]
        public void IsValidLogFileName_WhenCalled_ChangesWasLastValidFileName(string fileName, bool expected)
        {
            LogAnalyzer analyzer = new LogAnalyzer();

            analyzer.IsValidLogFileName(fileName);

            Assert.AreEqual(expected, analyzer.WasLastValidFileName);
        }
    }
}
