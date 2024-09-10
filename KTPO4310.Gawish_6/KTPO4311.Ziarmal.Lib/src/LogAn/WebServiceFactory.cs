using System;
using System.Collections.Generic;
using System.Text;

namespace KTPO4311.Ziarmal.Lib.src.SampleCommands
{
    /// <summary>
    /// Фабрика веб сервисов
    /// </summary>
    public class WebServiceFactory
    {
        private static IWebService webService = null;
        /// <summary>
        /// Создание объектов
        /// </summary>
        public static IWebService Create()
        {
            if (webService != null)
            {
                return webService;
            }
            return new FakeWebService();
        }
        /// <summary>
        /// Метод позволит тестам контролировать, что возвращает фабрика
        /// </summary>
        /// <param name="mgr"></param>
        public static void SetWebService(IWebService ws)
        {
            webService = ws;
        }
    }
}
