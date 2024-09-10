using KTPO4310.Gawish.Lib.src.LogAn;
using System;

namespace KTPO4310.Gawish.Lib.srs.LogAn
{
    /// <summary>
    /// Анализатор лог. файлов
    /// </summary>
    public class LogAnalyzer : ILogAnalyzer
    {
        public IExtensionManager _iem;
        public IWebService iws;
        public IEmailService ies;

        /// <summary>Объявление события</summary>
        public event LogAnalyzerAction Analyzed = null;

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
            bool result;
            try
            {
                result = _iem.IsValid(fileName);
            }
            catch (Exception)
            {
                return false;
            }

            return result;
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
                    ies.SendEmail("someone@somewhere.com", "Невозможно вызвать веб-сервис", e.Message);
                }

            }

            // Обработка лога
            // ..

            // Вызов события
            if (Analyzed != null)
            {
                // Analyzed();
                RaiseAnalyzedEvent();
            }
        }

        protected void RaiseAnalyzedEvent()
        {
            //Вызов события
            if (Analyzed != null)
            {
                Analyzed();
            }
        }
    }
}
