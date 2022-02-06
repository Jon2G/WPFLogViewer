using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Display;
using WPFLogViewer.Models;

namespace WPFLogViewer.Serilog
{
    public static class LogEventExtensions
    {
        public static ILogger Here(this ILogger logger,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return logger
                .ForContext("MemberName", memberName)
                .ForContext("FilePath", sourceFilePath)
                .ForContext("LineNumber", sourceLineNumber);
        }

        internal static LogEntry ToLogEntry(this LogEvent logEvent, IFormatProvider? _formatProvider=null)
        {
            logEvent.RenderMessage();
            using (StringWriter renderSpace = new StringWriter())
            {
                string message=logEvent.RenderMessage(_formatProvider);
                return new LogEntry()
                {
                    Timestamp = logEvent.Timestamp,
                    Message = message
                };
            }
        }
    }
}
