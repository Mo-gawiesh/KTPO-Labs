using System;
using System.Collections.Generic;
using System.Text;

namespace KTPO4311.Ziarmal.Lib.src.SampleCommands
{
    public class ExceptionCommandDecorator : ISampleCommand
    {
        ISampleCommand sampleCommand;
        IView view;

        public ExceptionCommandDecorator(ISampleCommand sampleCommand, IView view)
        {
            this.sampleCommand = sampleCommand;
            this.view = view;
        }

        public void Execute()
        {
            try
            {
                sampleCommand.Execute();
            }
            catch (Exception ex) 
            {
                view.Render("Ошибка: " + ex.Message);
            }
        }
    }
}
