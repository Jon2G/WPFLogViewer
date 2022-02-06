using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Serilog;
using WPFLogViewer.Serilog;

namespace WPFLogViewer.Test
{
    internal class DemoLogsGenerator
    {
        private string TestData = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum";
        private List<string> words;
        private int maxword;
        private int index;
        private System.Threading.Timer Timer;
        private System.Random random;
        public DemoLogsGenerator()
        {
            var sink = new WPFLogViewer.Serilog.LogsSink();
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Sink(sink)
                .WriteTo.Console()
                .CreateLogger();

            random = new Random();
            words = TestData.Split(' ').ToList();
            maxword = words.Count - 1;
            Timer = new Timer(x => RandomLog(), null, 1000, 1000);
        }
        private void RandomLog()
        {
            string Message = string.Join(" ", Enumerable.Range(5, random.Next(10, 50))
                .Select(x => words[random.Next(0, maxword)]));
            int level = random.Next(0, 5);
            switch (level)
            {
                case 0:
                    Log.Logger.Verbose(Message);
                    break;
                case 1:
                    Log.Logger.Debug(Message);
                    break;
                case 3:
                    Log.Logger.Here().Warning(Message);
                    break;
                case 4:
                    Log.Logger.Here().Error(Message);
                    break;
                case 5:
                    Log.Logger.Here().Fatal(Message);
                    break;
            }
        }
    }
}
