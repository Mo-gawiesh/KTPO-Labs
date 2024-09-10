using System;
using System.Collections.Generic;
using System.Text;

namespace KTPO4311.Ziarmal.Lib.src.SampleCommands
{
    public class FakeEmailService : IEmailService
    {

        public string to;
        public string subject;
        public string body;

        public void SendEmail(string to, string subject, string body)
        {
            this.to = to;
            this.subject = subject;
            this.body = body;
        }

        public string Statement()
        {
            return $"This message sent to {to}. Subject of this message - {subject}. Message: {body} ";
        }
    }
}
