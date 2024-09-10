using KTPO4310.Gawish.Lib.src.LogAn;
using System;

namespace KTPO4310.Gawish.Lib.srs.LogAn
{
    /// <summary>
    /// Анализатор лог. файлов
    /// </summary>
    public class LogAnalyzer
    {
        public IExtensionManager _iem;
        public IWebService iws;
        public IEmailService ies;

        public LogAnalyzer()
        {
            _iem = ExtensionManagerFactory.Create();
            iws = WebServiceFactory.Create();
            ies = EmailServiceFactory.Create();
        }

        /// <summary>
        /// Проверка правильности расширения
        /// </summary>
        public bool IsValidLogFileName(string fileName)
        {
            return _iem.IsValid(fileName);
        }

        public void Analyze(string fileName)
        {
            if (fileName.Length < 8)
            {
                try
                {
                    //Передать внешней службе сообщение об ошибке
                    IWebService srv = WebServiceFactory.Create();
                    srv.LogError("Слишком короткое имя файла: " + fileName);
                }
                catch (Exception e)
                {
                    //Отправить сообщение по  электронной почте
                    ies.SendEmail("someone@somewhere.com", "Невозможно вызвать веб-сервисе", e.Message);
                }

            }
        }
    }
}
