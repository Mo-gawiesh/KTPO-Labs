using System;
using System.Collections.Generic;
using System.Text;
using static KTPO4311.Ziarmal.Lib.src.SampleCommands.LogAnalyzerActionD;

namespace KTPO4311.Ziarmal.Lib.src.SampleCommands
{
    public interface ILogAnalyzer
    {
        public event LogAnalyzerAction Analyzed;
        public bool IsValidLogFileName(string fileName);
        public void Analyze(string fileName);
    }
}
