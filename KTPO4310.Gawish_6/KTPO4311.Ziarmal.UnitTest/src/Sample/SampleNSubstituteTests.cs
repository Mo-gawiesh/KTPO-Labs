using KTPO4311.Ziarmal.Lib.src.SampleCommands;
using NSubstitute;
using NUnit.Framework;
using System;


namespace KTPO4317.Vasilev.UnitTest.src.Sample
{
    internal class SampleNSubstituteTests
    {
        [Test]
        public void Returns_ParticularArg_Works()
        {
            //Создать поддельный объект 
            IExtensionManager fakeExtensionnManager = Substitute.For<IExtensionManager>();

            //Настроить объект, чтобы метод возвращал true для заданного значения входного параметра
            fakeExtensionnManager.IsValid("validfile.ext").Returns(true);

            //Воздействие на тестируемый объект
            bool result = fakeExtensionnManager.IsValid("validfile.ext");

            //Проверка ожидаемого результата
            Assert.IsTrue(result);
        }

        [Test]
        public void Returns_ArgAny_Works()
        {
            //Создать поддельный объект 
            IExtensionManager fakeExtensionnManager = Substitute.For<IExtensionManager>();

            //Настроить объект, чтобы метод возвращал true независимо от параметров
            fakeExtensionnManager.IsValid(Arg.Any<string>()).Returns(true);

            //Воздействие на тестируемый объект
            bool result = fakeExtensionnManager.IsValid("anyfile.ext");

            //Проверка ожидаемого результата
            Assert.IsTrue(result);
        }

        [Test]
        public void Returns_ArgAny_Throws()
        {
            //Создать поддельный объект 
            IExtensionManager fakeExtensionnManager = Substitute.For<IExtensionManager>();

            //Настроить объект, чтобы метод вызвал исключение, независимо от входных аргументов
            fakeExtensionnManager.When(x => x.IsValid(Arg.Any<string>())).Do(context => { throw new Exception("fake exception"); });

            //Проверка ожидаемого результата
            Assert.Throws<Exception>(() => fakeExtensionnManager.IsValid("anything"));
        }

        [Test]
        public void Received_ParticularArg_Saves()
        {
            //Создать поддельный объект
            IWebService mockWebService = Substitute.For<IWebService>();

            //Воздействие на поддельный объект
            mockWebService.LogError("Поддельное сообщение");

            //Проверка, что поддельный объект сохранил параметры вызова
            mockWebService.Received().LogError("Поддельное сообщение");
        }
    }
}
