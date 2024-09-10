using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTPO4310.Gawish.Lib.src.LogAn
{
    public static class WebServiceFactory
    {
        private static IWebService customWeb = null;

        ///<summary> Создание объектов </summary>
        public static IWebService Create()
        {
            if (customWeb != null)
            {
                return customWeb;
            }

            return new WebService();
        }

        ///<summary> Метод позволит тестам контролировать
        ///что возвращает фабрика </summary>>
        public static void SetWebService(IWebService str)
        {
            customWeb = str;
        }
    }
}