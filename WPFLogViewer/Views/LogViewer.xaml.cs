using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFLogViewer.ViewModels;

// ReSharper disable once CheckNamespace
namespace WPFLogViewer
{
    /// <summary>
    /// Interaction logic for LogViewer.xaml
    /// </summary>
    public partial class LogViewer : UserControl
    {
        public LogViewer()
        {
            this.DataContext = LogViewerViewModel.Current;

                InitializeComponent();
            LogViewerViewModel.Current.LogEntries.CollectionChanged += LogEntries_CollectionChanged;
        }

        private void LogEntries_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (VisualTreeHelper.GetChildrenCount(this.ItemsControl) > 0)
            {
                ScrollViewer scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(ItemsControl, 0);
                scrollViewer.ScrollToBottom();
            }
        }
    }
}
