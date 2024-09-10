using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTPO4310.Gawish.Lib.src.LogAn
{
    public static class EmailServiceFactory
    {
        private static IEmailService customEmail = null;

        ///<summary> Создание объектов </summary>

        public static IEmailService Create()
        {
            if (customEmail != null)
            {
                return customEmail;
            }

            return new EmailService();
        }

        ///<summary> Метод позволит тестам контролировать
        ///что возвращает фабрика </summary>>

        public static void SetEmail(IEmailService str)
        {
            customEmail = str;
        }
    }
}
