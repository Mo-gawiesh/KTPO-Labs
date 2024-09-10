using System;
using System.Collections.Generic;
using System.Text;

namespace KTPO4311.Ziarmal.Lib.src.SampleCommands
{
    public interface IEmailService
    {
        void SendEmail(string to, string subject, string body);
    }
}
