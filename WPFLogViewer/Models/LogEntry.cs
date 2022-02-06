using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFLogViewer.Models
{
    internal class LogEntry : INotifyPropertyChangedModel
    {
        private DateTimeOffset _dateTime;
        private string _message;

        public DateTimeOffset Timestamp
        {
            get => _dateTime;
            set
            {
                _dateTime = value;
                Raise(() => Timestamp);
            }
        }



        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                Raise(() => Message);
            }
        }
    }
}
