using NUnit.Framework;
using KTPO4311.Ziarmal.Lib.src.SampleCommands;
using System;

namespace KTPO4311.Ziarmal.UnitTest.src.LogAn
{
    [TestFixture]
    public class LogAnalyzerTests
    {

        [Test]
        public void Analyze_WebServiceThrows_SendEmail()
        {
            //Подготовка теста
            FakeWebService stubWebService = new FakeWebService();
            WebServiceFactory.SetWebService(stubWebService);
            stubWebService.WillThrow = new Exception("это подделка");

            FakeEmailService mockEmail = new FakeEmailService();
            EmailServiceFactory.SetEmailService(mockEmail);

            LogAnalyzer log = new LogAnalyzer();
            string tooShortFileName = "abc.ext";

            //Воздействие на тестируемый объект
            log.Analyze(tooShortFileName);

            //Проверка ожидаемого результата
            //..тест будет ложным, если неверно хоть одно утверждение

            StringAssert.Contains("someone@somewhere.com", mockEmail.to);
            StringAssert.Contains("это подделка", mockEmail.body);
            StringAssert.Contains("Невозможно выбрать веб-сервис", mockEmail.subject);
        }
        
        [Test]
        
        public void Analyze_TooShortFileName_CallWebService()
        {
            //Подготовка теста
            FakeWebService mockWebService = new FakeWebService();
            WebServiceFactory.SetWebService(mockWebService);
            LogAnalyzer log = new LogAnalyzer();
            string tooShortFileName = "abc.ext";

            //Воздействие на тестируемый объект
            log.Analyze(tooShortFileName);

            //Проверка ожидаемого результата
            StringAssert.Contains("Слишком короткое имя файла: abc.ext", mockWebService.LastError);
        }
        
        [Test]
        public void IsValidFileName_NameSupportedExtension_ReturnTrue()
        {
            //Подготовка теста
            FakeExtensionManager fakeManager = new FakeExtensionManager();
            fakeManager.WillBeValid = true;
            ExtensionManagerFactory.SetManager(fakeManager);
            LogAnalyzer log = new LogAnalyzer();

            //Воздействие на тестируемый объект
            bool result = log.IsValidLogFileName("short.wxt");

            //Проверка ожидаемого результата
            Assert.True(result);
            
        }
        
        [Test]
        public void IsValidFileName_NameNotSupportedExtension_ReturnFalse()
        {
            //Подготовка теста
            FakeExtensionManager fakeManager = new FakeExtensionManager();
            fakeManager.WillBeValid = false;
            ExtensionManagerFactory.SetManager(fakeManager);
            LogAnalyzer log = new LogAnalyzer();

            //Воздействие на тестируемый объект
            bool result = log.IsValidLogFileName("short.wxt");

            //Проверка ожидаемого результата
            Assert.False(result);
        }

        [Test]
        public void IsValidFileName_ExtManagerThrowsException_ReturnFalse()
        {
            //Подготовка теста
            FakeExtensionManager fakeManager = new FakeExtensionManager();
            Exception ex = new NullReferenceException("Отсутствует такой файл");
            fakeManager.WillThrow = ex;
            ExtensionManagerFactory.SetManager(fakeManager);
            LogAnalyzer log = new LogAnalyzer();

            //Проверка ожидаемого результата
            var exception = Assert.Catch<Exception>(() => log.IsValidLogFileName("short.wxt"));
            StringAssert.Contains("Отсутствует такой файл", exception.Message);
        }

        [Test]
        public void Analyze_WhenAnalyzed_FiredEvent()
        {
            //подготовка теста
            bool analyzedFired = false;
            LogAnalyzer logAnalyzer = new LogAnalyzer();
            //..используем анонимный метод в качестве обработчика события
            logAnalyzer.Analyzed += delegate ()
            {
                analyzedFired = true;
            };

            //воздействие на тестируемый объект
            logAnalyzer.Analyze("validfilename.vld");
            //проверка ожидаемого результата
            Assert.IsTrue(analyzedFired);

        }

        [TearDown]
        public void AfterEachTest()
        {
            ExtensionManagerFactory.SetManager(null);
            WebServiceFactory.SetWebService(null);
            EmailServiceFactory.SetEmailService(null);
        }
    }

    internal class FakeExtensionManager : IExtensionManager
    {
        /// <summary>
        /// Это поле позволяет настроить поддельный результат для метода IsValid
        /// </summary>

        public bool WillBeValid = false;
        public Exception WillThrow = null;
        public bool IsValid(string fileName)
        {
            if(WillThrow != null)
            {
                throw WillThrow;
            }
            return WillBeValid;
        }
    }
}
