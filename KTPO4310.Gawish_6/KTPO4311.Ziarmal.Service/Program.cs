using KTPO4311.Ziarmal.Lib.src.SampleCommands;
using System.Xml;

void CheckExtensionIsValid(string fileName)
{
    LogAnalyzer analyzer = new LogAnalyzer();
    if (analyzer.IsValidLogFileName(fileName))
        Console.WriteLine($"Расширение файла {fileName} валидно");
    else
        Console.WriteLine($"Расширение файла {fileName} не валидно");
}

XmlDocument doc = new XmlDocument();
doc.Load(filename: "C:\\Users\\Ziarmal N\\OneDrive\\Desktop\\OC\\KTPO4311.Zairmal_2\\settings.xml");
FileExtensionManager.ReadExtension(doc);
CheckExtensionIsValid(".txt");
CheckExtensionIsValid(".xml");
CheckExtensionIsValid(".xlsx");
CheckExtensionIsValid(".obj");
CheckExtensionIsValid(".css");
CheckExtensionIsValid(".cs");
CheckExtensionIsValid(".js");

