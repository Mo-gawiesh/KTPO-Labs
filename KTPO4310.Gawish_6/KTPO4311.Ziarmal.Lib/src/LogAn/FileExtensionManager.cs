using System;
using System.Collections.Generic;
using System.Xml;

namespace KTPO4311.Ziarmal.Lib.src.SampleCommands
{
    /// <summary>
    /// Менеджер расширений файлов
    /// </summary>
    public class FileExtensionManager : IExtensionManager
    {
        public static List<string> extensions = new List<string>();

        public static void ReadExtension(XmlDocument document)
        {
            extensions.Clear();
            XmlNode settings = document.SelectSingleNode("settings");
            foreach (XmlNode node in settings)
            {
                extensions.Add(node.InnerText);
            }
        }
        /// <summary>
        /// Проверка правильности расширения
        /// </summary>
        public bool IsValid(string fileName)
        {
            //читать конфигурационный файл
            //вернуть true
            //если конфигурация поддерживается
            
            foreach (var extension in extensions)
            {
                if(fileName.EndsWith(extension, StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }    
            }
            return false;

            throw new Exception();
        }
    }
}