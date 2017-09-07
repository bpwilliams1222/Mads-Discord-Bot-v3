using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadBot_3.Models
{
    public class BotError
    {
        public BotError(Exception c)
        {
            LogId = Guid.NewGuid();
            timestamp = DateTime.UtcNow;
            Error = c.Message;
            stackTrace = c.StackTrace;
            if(c.InnerException != null)
            {
                if (c.InnerException.Message != null)
                    InnerException = c.InnerException.Message;
                if (c.InnerException.StackTrace != null)
                    innerStackTrace = c.InnerException.StackTrace;
            }
        }

        public BotError()
        {
            LogId = Guid.NewGuid();
            timestamp = DateTime.UtcNow;
        }
        public Guid LogId { get; set; }
        public string Error { get; set; }
        public string InnerException { get; set; }
        public DateTime timestamp { get; set; }
        public string stackTrace { get; set; }
        public string innerStackTrace { get; set; }
    }
}
