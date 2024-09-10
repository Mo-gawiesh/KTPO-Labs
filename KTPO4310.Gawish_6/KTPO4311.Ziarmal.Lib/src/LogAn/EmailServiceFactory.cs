using System;
using System.Collections.Generic;
using System.Text;

namespace KTPO4311.Ziarmal.Lib.src.SampleCommands
{
    public class EmailServiceFactory
    {
        private static IEmailService emailService = null;
        /// <summary>
        /// Создание объектов
        /// </summary>
        public static IEmailService Create()
        {
            if (emailService != null)
            {
                return emailService;
            }
            return new FakeEmailService();
        }
        /// <summary>
        /// Метод позволит тестам контролировать, что возвращает фабрика
        /// </summary>
        /// <param name="mgr"></param>
        public static void SetEmailService(IEmailService es)
        {
            emailService = es;
        }
    }
}
