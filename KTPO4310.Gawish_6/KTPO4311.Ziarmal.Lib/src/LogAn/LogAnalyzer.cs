using System;
using static KTPO4311.Ziarmal.Lib.src.SampleCommands.LogAnalyzerActionD;

namespace KTPO4311.Ziarmal.Lib.src.SampleCommands
{
    /// <summary>
    /// Анализатор лог. файлов
    /// </summary>
    public class LogAnalyzer : ILogAnalyzer
    {
        /// <summary>
        /// Объявление события
        /// </summary>
        public event LogAnalyzerAction Analyzed = null;
        
        /// <summary>
        /// Проверка правильности имени файла
        /// </summary>
        public bool IsValidLogFileName(string fileName)
        {
            return ExtensionManagerFactory.Create().IsValid(fileName);
        }

        /// <summary>
        /// Анализировать лог. файл
        /// </summary>
        public void Analyze(string fileName)
        {
            if(fileName.Length < 8)
            {
                try
                {
                    IWebService webService = WebServiceFactory.Create();
                    webService.LogError("Слишком короткое имя файла: " + fileName);
                    //Передать внешней службе сообщение об ошибке
                }
                catch(Exception e)
                {
                    IEmailService emailService = EmailServiceFactory.Create();
                    emailService.SendEmail("someone@somewhere.com", "Невозможно выбрать веб-сервис", e.Message);
                    //Отправить сообщение об ошибке по электронной почте
                }
            }

            //Обработка лога
            //..

            RaiseAnalyzedEvent();
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
