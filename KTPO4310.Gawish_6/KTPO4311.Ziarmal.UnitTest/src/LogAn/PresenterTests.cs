using KTPO4311.Ziarmal.Lib.src.SampleCommands;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using static KTPO4311.Ziarmal.Lib.src.SampleCommands.LogAnalyzerActionD;

namespace KTPO4317.Vasilev.UnitTest.src.LogAn
{
    internal class PresenterTests
    {
        [Test]
        public void WhenAnalyzed_CallsViewRender()
        {
            FakeLogAnalyzer logAnalyzer = new FakeLogAnalyzer();
            IView view = Substitute.For<IView>();
            Presenter presenter = new Presenter(logAnalyzer, view);
            logAnalyzer.CallRaiseAnalyzedEvent();
            view.Received().Render("Обработка завершена");
        }

        [Test]
        public void WhenAnalyzed_CallsViewRender_NSubstitute()
        {
            ILogAnalyzer logAnalyzer = Substitute.For<ILogAnalyzer>();
            IView view = Substitute.For<IView>();
            Presenter presenter = new Presenter(logAnalyzer, view);
            logAnalyzer.Analyzed += Raise.Event<LogAnalyzerAction>();
            view.Received().Render("Обработка завершена");
        }
    }

    /// <summary>
    /// Заглушка для имитации вызова события
    /// </summary>
    class FakeLogAnalyzer : LogAnalyzer
    {
        public void CallRaiseAnalyzedEvent()
        {
            base.RaiseAnalyzedEvent();
        }
    }

}
