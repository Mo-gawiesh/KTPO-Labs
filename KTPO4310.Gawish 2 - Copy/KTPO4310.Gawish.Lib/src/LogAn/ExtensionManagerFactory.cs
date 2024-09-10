using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTPO4310.Gawish.Lib.src.LogAn
{
    /// <summary>
    /// Фабрика диспетчеров расширений файлов
    /// </summary>
    public static class ExtensionManagerFactory
    {
        private static IExtensionManager customManager = null;

        /// <summary>
        /// Создание объектов
        /// </summary>
        /// <returns></returns>
        public static IExtensionManager Create()
        {
            if (customManager != null)
            {
                return customManager;
            }

            return new FileExtensionManager();
        }

        /// <summary>
        /// Метод позволит тестам контролировать, что возвращает фабрика
        /// </summary>
        /// <param name="iem"></param>
        public static void SetManager(IExtensionManager iem)
        {
            customManager = iem;
        }
    }
}
