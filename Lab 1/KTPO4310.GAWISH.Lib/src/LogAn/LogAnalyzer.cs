using System;

namespace KTPO4310.GAWISH.Lib.src.LogAn
{
    public class LogAnalyzer
    {
        public bool WasLastValidFileName { get; set; }

        public bool IsValidLogFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("name is empty");
            }

            if (fileName.EndsWith(".SLF", System.StringComparison.CurrentCultureIgnoreCase))
            {
                WasLastValidFileName = true;
                return true;
            }

            return false;
        }
    }
}
