using KTPO4311.Ziarmal.Lib.src.SampleCommands;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTPO4311.Ziarmal.UnitTest.src.LogAn
{
    internal class CommandNSubstituteTests
    {
        [Test]
        public void FirstCommand_Execute_Valid()
        {
            IView mockView = Substitute.For<IView>();
            FirstCommand command = new FirstCommand(mockView);
            command.Execute();
            int iExecute = 1;
            mockView.Received().Render(command.GetType().ToString() + "\n iExecute = " + iExecute);
        }

        [Test]
        public void SampleCommandDecorator_Execute_CallException()
        {
            IView view = Substitute.For<IView>();
            ISampleCommand sampleCommand = Substitute.For<ISampleCommand>();

            SampleCommandDecorator sampleCommandDecorator = new SampleCommandDecorator(sampleCommand, view);
            sampleCommandDecorator.Execute();
            sampleCommand.Received().Execute();
        }

        [Test]
        public void SampleCommandDecorator_Execute_Valid()
        {
            IView view = Substitute.For<IView>();
            ISampleCommand sampleCommand = Substitute.For<ISampleCommand>();

            SampleCommandDecorator sampleCommandDecorator = new SampleCommandDecorator(sampleCommand, view);
            sampleCommandDecorator.Execute();

            view.Received().Render("Начало: " + sampleCommandDecorator.GetType().ToString());
            view.Received().Render("Конец: " + sampleCommandDecorator.GetType().ToString());
        }

        [Test]
        public void ExceptionCommandDecorator_CallException()
        {
            IView view = Substitute.For<IView>();
            ISampleCommand sampleCommand = Substitute.For<ISampleCommand>();
            ExceptionCommandDecorator exceptionCommandDecorator = new ExceptionCommandDecorator(sampleCommand, view);
            exceptionCommandDecorator.Execute();
            sampleCommand.Received().Execute();
        }


        [Test]
        public void ExceptionCommandDecorator_Valid()
        {
            IView view = Substitute.For<IView>();
            ISampleCommand sampleCommand = Substitute.For<ISampleCommand>();
            Exception fakeException = new Exception("fake exception");
            sampleCommand.When(command => command.Execute()).Do(context => { throw fakeException; });
            ExceptionCommandDecorator exceptionCommandDecorator = new ExceptionCommandDecorator(sampleCommand, view);
            exceptionCommandDecorator.Execute();
            view.Received().Render("Ошибка: " + fakeException.Message);
        }
    }
}
