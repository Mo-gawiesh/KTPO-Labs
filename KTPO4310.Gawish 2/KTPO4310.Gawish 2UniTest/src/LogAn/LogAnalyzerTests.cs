using System;
using KTPO4310.Gawish.Lib.src.LogAn;
using KTPO4310.Gawish.Lib.srs.LogAn;
using NSubstitute;
using NUnit.Framework;

namespace KTPO4310.Gawish_2UniTest.src.LogAn
{
    [TestFixture]
    public class LogAnalyzerNSubstituteTests
    {
        [TearDown]
        public void AfterEachTest()
        {
            ExtensionManagerFactory.SetManager(null);
            WebServiceFactory.SetWebService(null);
        }

        [Test]
        public void IsValidFileName_NameSupportedExtension_ReturnsTrue()
        {
            // Подготовка теста
            IExtensionManager fakeExtensionManager = Substitute.For<IExtensionManager>();

            // Настроить объект, чтобы метод возвращал true для заданного значения входного параметра
            fakeExtensionManager.IsValid("file.extension").Returns(true);

            // Конфигурируем фабрику для создания поддельных объектов
            ExtensionManagerFactory.SetManager(fakeExtensionManager);

            LogAnalyzer la = new LogAnalyzer();

            // Воздействие на тестируемый объект
            bool result = la.IsValidLogFileName("file.extension");

            Assert.True(result);
        }

        [Test]
        public void IsValidFileName_NameNotSupportedExtension_ReturnsFalse()
        {
            // Подготовка теста
            IExtensionManager fakeExtensionManager = Substitute.For<IExtensionManager>();

            // Настроить объект, чтобы метод возвращал false для заданного значения входного параметра
            fakeExtensionManager.IsValid("file.extension").Returns(false);

            // Конфигурируем фабрику для создания поддельных объектов
            ExtensionManagerFactory.SetManager(fakeExtensionManager);

            LogAnalyzer la = new LogAnalyzer();

            // Воздействие на тестируемый объект
            bool result = la.IsValidLogFileName("file.extension");

            Assert.False(result);
        }

        [Test]
        public void IsValidFileName_ExtManagerThrowsException_ReturnsFalse()
        {
            // Подготовка теста
            IExtensionManager fakeExtensionManager = Substitute.For<IExtensionManager>();

            // Настроить объект, чтобы метод вызвал исключение
            fakeExtensionManager.When(x => x.IsValid(Arg.Any<string>()))
                .Do(context => { throw new Exception("fake exception"); });

            // Конфигурируем фабрику для создания поддельных объектов
            ExtensionManagerFactory.SetManager(fakeExtensionManager);

            LogAnalyzer la = new LogAnalyzer();

            // Воздействие на тестируемый объект
            bool result = la.IsValidLogFileName("file.extension");

            Assert.False(result);
        }

        [Test]
        public void Analyze_TooShortFileName_CallsWebService()
        {
            //Подготовка теста
            IWebService mockWebService = Substitute.For<IWebService>();
            WebServiceFactory.SetWebService(mockWebService);
            LogAnalyzer log = new LogAnalyzer();

            string tooshortFileName = "abc.ext";

            //Воздействие на тестируемый объект
            log.Analyze(tooshortFileName);

            //Проверка ожидаемого результата
            mockWebService.Received().LogError("Слишком короткое имя файла: " + tooshortFileName);
        }

        [Test]
        public void Analyze_WebServiceThrows_SendsEmail()
        {
            //Подготовка теста
            IWebService stubWebService = Substitute.For<IWebService>();
            WebServiceFactory.SetWebService(stubWebService);

            stubWebService.When(x => x.LogError(Arg.Any<string>()))
                .Do(context => { throw new Exception("это подделка"); });

            IEmailService mockEmail = Substitute.For<IEmailService>();
            EmailServiceFactory.SetEmail(mockEmail);

            LogAnalyzer log = new LogAnalyzer();
            string tooShortFileName = "abc.ext";

            //Воздействие на тестируемый объект
            log.Analyze(tooShortFileName);

            //Проверка ожидаемого результата
            mockEmail.Received().SendEmail("someone@somewhere.com", "Невозможно вызвать веб-сервис", "это подделка");
        }
    }

    [TestFixture]
    public class LogAnalyzerTests
    {
        [TearDown]
        public void AfterEachTest()
        {
            ExtensionManagerFactory.SetManager(null);
        }

        [Test]
        public void IsValidFileName_NameSupportedExtension_ReturnsTrue()
        {
            // Подготовка теста
            FakeExtensionManager fakeManager = new FakeExtensionManager();
            fakeManager.WillBeValid = true;

            // Конфигурируем фабрику для создания поддельных объектов
            ExtensionManagerFactory.SetManager(fakeManager);

            LogAnalyzer la = new LogAnalyzer();

            // Воздействие на тестируемый объект
            bool result = la.IsValidLogFileName("file.extension");

            Assert.True(result);
        }

        [Test]
        public void IsValidFileName_NameNotSupportedExtension_ReturnsFalse()
        {
            // Подготовка теста
            FakeExtensionManager fakeManager = new FakeExtensionManager();
            fakeManager.WillBeValid = false;

            // Конфигурируем фабрику для создания поддельных объектов
            ExtensionManagerFactory.SetManager(fakeManager);

            LogAnalyzer la = new LogAnalyzer();

            // Воздействие на тестируемый объект
            bool result = la.IsValidLogFileName("file.extension");

            Assert.False(result);
        }

        [Test]
        public void IsValidFileName_ExtManagerThrowsException_ReturnsFalse()
        {
            // Подготовка теста
            FakeExtensionManager fakeManager = new FakeExtensionManager();
            fakeManager.WillThrow = new Exception();
            fakeManager.WillBeValid = false;

            // Конфигурируем фабрику для создания поддельных объектов
            ExtensionManagerFactory.SetManager(fakeManager);

            LogAnalyzer la = new LogAnalyzer();

            // Воздействие на тестируемый объект
            bool result = la.IsValidLogFileName("file.extension");

            Assert.False(result);
        }

        [Test]
        public void Analyze_TooShortFileName_CallsWebService()
        {
            //Подготовка теста
            FakeWebService mockWebService = new FakeWebService();
            WebServiceFactory.SetWebService(mockWebService);
            LogAnalyzer log = new LogAnalyzer();
            string tooshortFileName = "abc.ext";

            //Воздействие на тестируемый объект
            log.Analyze(tooshortFileName);

            //Проверка ожидаемого результата
            StringAssert.Contains("Слишком короткое имя файла: abc.ext", mockWebService.LastError);
        }

        [Test]
        public void Analyze_WebServiceThrows_SendsEmail()
        {
            //Подготовка теста
            FakeWebService stubWebService = new FakeWebService();
            WebServiceFactory.SetWebService(stubWebService);
            stubWebService.WiilThrowWeb = new Exception("это подделка");

            FakeEmailService mockEmail = new FakeEmailService();
            EmailServiceFactory.SetEmail(mockEmail);

            LogAnalyzer log = new LogAnalyzer();
            string tooShortFileName = "abc.ext";

            //Воздействие на тестируемый объект
            log.Analyze(tooShortFileName);

            //Проверка ожидаемого результата
            //.. здесь тест будет ложным, если неверно хотя бы одно утверждение
            //.. поэтому здесь допустимо несколько утверждений
            StringAssert.Contains("someone@somewhere.com", mockEmail.to);
            StringAssert.Contains("это подделка", mockEmail.body);
            StringAssert.Contains("Невозможно вызвать веб-сервис", mockEmail.subject);
        }

        [Test]
        public void Analyze_WhenAnalyzed_FiredEvent()
        {
            bool analyzedFired = false;
            LogAnalyzer logAnalyzer = new LogAnalyzer();
            logAnalyzer.Analyzed += delegate ()
            {
                analyzedFired = true;
            };
            logAnalyzer.Analyze("validfilename.vld");
            Assert.IsTrue(analyzedFired);
        }
    }

    /// <summary>
    /// Поддельный менеджер расширений файлов
    /// </summary>
    internal class FakeExtensionManager : IExtensionManager
    {
        public bool WillBeValid = false;

        public Exception WillThrow = null;

        public bool IsValid(string fileName)
        {
            if (WillThrow != null)
            {
                throw WillThrow;
            }
            return WillBeValid;
        }
    }

    /// <summary> Поддельная веб-служба </summary>
    internal class FakeWebService : IWebService
    {
        /// <summary>Это поле запоминает состояние после вызова метода LogError 
        /// при тестировании взаимодействия утверждения высказывается относительно</summary>
        public string LastError;
        public Exception WiilThrowWeb = null;

        public void LogError(string message)
        {
            if (WiilThrowWeb != null)
            {
                throw WiilThrowWeb;
            }
            LastError = message;
        }
    }

    /// <summary> Поддельная email-служба </summary>
    internal class FakeEmailService : IEmailService
    {
        /// <summary> Это поле запоминает состояние после вызова метода SendEmail
        /// при тестировании взаимодействия утверждения высказывается относительно </summary>
        public string to, subject, body;

        public void SendEmail(string to, string subject, string body)
        {
            this.to = to;
            this.subject = subject;
            this.body = body;
        }
    }
}
