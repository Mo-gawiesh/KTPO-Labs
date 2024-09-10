using System;
using KTPO4310.Gawish.Lib.src.LogAn;
using KTPO4310.Gawish.Lib.srs.LogAn;

namespace KTPO4310.Gawish.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            LogAnalyzer analyzer = new LogAnalyzer();
            Console.WriteLine(analyzer.IsValidLogFileName("Glukhov.abc"));
            Console.WriteLine(analyzer.IsValidLogFileName("Glukhov.txt"));
            Console.WriteLine(analyzer.IsValidLogFileName("Glukhov.cfg"));
            Console.WriteLine("Нажмите любую клавишу");
            Console.ReadKey();
        }
    }
}
