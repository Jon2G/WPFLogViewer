using System;
using System.Runtime.CompilerServices;
using System.Text;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using WPFLogViewer.Models;
using WPFLogViewer.ViewModels;

namespace WPFLogViewer.Serilog
{

    public class LogsSink : ILogEventSink
    {
        private readonly IFormatProvider _formatProvider;
        public LogsSink(IFormatProvider formatProvider=null)
        {
            _formatProvider = formatProvider;
        }
        public void Emit(LogEvent logEvent)
        {
            if (logEvent is null)
                throw new ArgumentNullException(nameof(logEvent));
            LogEntry logEntry = logEvent.ToLogEntry(_formatProvider);
            LogViewerViewModel.Current.Add(logEntry);
        }
    }
}
