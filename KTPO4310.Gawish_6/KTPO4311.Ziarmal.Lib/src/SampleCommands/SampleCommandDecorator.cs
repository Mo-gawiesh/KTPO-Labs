using System;
using System.Collections.Generic;
using System.Text;

namespace KTPO4311.Ziarmal.Lib.src.SampleCommands
{
    public class SampleCommandDecorator : ISampleCommand
    {
        ISampleCommand sampleCommand;
        IView view;

        public SampleCommandDecorator(ISampleCommand sampleCommand, IView view)
        {
            this.sampleCommand = sampleCommand;
            this.view = view;
        }

        public void Execute()
        {
            view.Render("Начало: " + this.GetType().ToString());
            try
            {
                sampleCommand.Execute();
            }
            finally
            {
                view.Render("Конец: " + this.GetType().ToString());
            }
        }
    }
}
