using System;
using System.Collections.Generic;
using System.Text;

namespace KTPO4311.Ziarmal.Lib.src.SampleCommands
{
    public class FakeWebService : IWebService
    {
        ///<summary> Это поле запоминает состояние после вызрва LogError
        ///при тестировании взаимодействия утверждения высказываются относительно </summary>


        public string LastError;


        public Exception WillThrow = null;


        public void LogError(string message)
        {
            if (WillThrow != null)
            {
                throw WillThrow;
            }
            LastError = message;
        }
    }
}
