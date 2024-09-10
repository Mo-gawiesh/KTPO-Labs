using System;
using KTPO4310.Gawish.Lib.srs.LogAn;

namespace KTPO4310.Gawish.Service
{ 
    class Program
{
    static void Main(string[] args)
    {
        LogAnalyzer analyzer = new LogAnalyzer();
        Console.WriteLine(analyzer.IsValidLogFileName("Gawish.abc"));
        Console.WriteLine(analyzer.IsValidLogFileName("Gawish.txt"));
        Console.WriteLine(analyzer.IsValidLogFileName("Gawish.cfg"));
        //while (true) { }
        Console.WriteLine("Нажмите любую клавишу");
        Console.ReadKey();
    }
}
}
