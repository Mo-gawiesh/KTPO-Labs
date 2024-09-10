using KTPO4311.Ziarmal.Lib.src.SampleCommands;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTPO4311.Ziarmal.UnitTest.src.LogAn
{
    public class LogAnalyzerNSubstituteTests
    {
        [Test]
        public void IsValidFileName_NameSupportedExtension_ReturnTrue()
        {
            //Подготовка теста
            IExtensionManager fakeManager = Substitute.For<IExtensionManager>();
            fakeManager.IsValid("validfile.ext").Returns(true);
            ExtensionManagerFactory.SetManager(fakeManager);
            LogAnalyzer log = new LogAnalyzer();

            //Воздействие на тестируемый объект
            bool result = log.IsValidLogFileName("validfile.ext");

            //Проверка ожидаемого результата
            Assert.True(result);

        }

        [Test]
        public void IsValidFileName_NameNotSupportedExtension_ReturnFalse()
        {
            //Подготовка теста
            IExtensionManager fakeManager = Substitute.For<IExtensionManager>();
            fakeManager.IsValid("validfile.ext").Returns(true);
            ExtensionManagerFactory.SetManager(fakeManager);
            LogAnalyzer log = new LogAnalyzer();

            //Воздействие на тестируемый объект
            bool result = log.IsValidLogFileName("invalidfile.ext");

            //Проверка ожидаемого результата
            Assert.False(result);
        }

        [Test]
        public void IsValidFileName_ExtManagerThrowsException_ReturnFalse()
        {
            //Подготовка теста
            IExtensionManager fakeManager = Substitute.For<IExtensionManager>();
            fakeManager.When(x => x.IsValid(Arg.Any<string>())).Do(context => { throw new Exception("fake extension"); });
            ExtensionManagerFactory.SetManager(fakeManager);
            LogAnalyzer log = new LogAnalyzer();

            //Проверка ожидаемого результата
            Assert.Throws<Exception>(() => fakeManager.IsValid("invalidfile.ext"));
        }

        [Test]
        public void Analyze_WebServiceThrows_SendEmail()
        {
            //Подготовка теста
            IWebService stubWebService = Substitute.For<IWebService>();
            WebServiceFactory.SetWebService(stubWebService);
            stubWebService.When(x => x.LogError(Arg.Any<string>())).Do(context => { throw new Exception("это подделка"); });

            IEmailService mockEmail = Substitute.For<IEmailService>();
            EmailServiceFactory.SetEmailService(mockEmail);
            
            LogAnalyzer log = new LogAnalyzer();
            string tooShortFileName = "abc.ext";

            //Воздействие на тестируемый объект
            log.Analyze(tooShortFileName);

            mockEmail.Received().SendEmail("someone@somewhere.com", "Невозможно выбрать веб-сервис", "это подделка");
        }

        [Test]

        public void Analyze_TooShortFileName_CallWebService()
        {
            //Подготовка теста
            IWebService stubWebService = Substitute.For<IWebService>();
            WebServiceFactory.SetWebService(stubWebService);

            LogAnalyzer log = new LogAnalyzer();
            string tooShortFileName = "abc.ext";

            //Воздействие на тестируемый объект
            log.Analyze(tooShortFileName);

            stubWebService.Received().LogError("Слишком короткое имя файла: " + tooShortFileName);
        }
    }
}
