using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;
using Serilog;
using WPFLogViewer.Models;

namespace WPFLogViewer.ViewModels
{
    internal class LogViewerViewModel
    {
        private static readonly Lazy<LogViewerViewModel> _Current
            = new Lazy<LogViewerViewModel>(() => new LogViewerViewModel());
        internal static LogViewerViewModel Current => _Current.Value;
        public ObservableCollection<LogEntry> LogEntries { get; set; }
        private static readonly object _LockObject = new object();
        public LogViewerViewModel()
        {
            LogEntries = new ObservableCollection<LogEntry>();
            BindingOperations.EnableCollectionSynchronization(LogEntries, _LockObject);
        }
        internal void Add(LogEntry logEntry)
        {
            Application.Current?.Dispatcher?.BeginInvoke(() =>
            {
                if (LogEntries.Count > 500)
                {
                    LogEntries.Clear();
                }
                LogEntries.Add(logEntry);
            });
        }
    }
}
