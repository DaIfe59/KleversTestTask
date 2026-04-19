using System;
using System.Collections.Generic;
using System.Text;

namespace KlevernsTestTask3
{
    public class LogEntry
    {
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string Level { get; set; }
        public string Method { get; set; }
        public string Message { get; set; }
    }
}
