using System.Collections.Concurrent;

namespace data_layer {
    public class Logger {
        private readonly ConcurrentQueue<string> _logQueue = new ConcurrentQueue<string>();
        private readonly AutoResetEvent _logSignal = new AutoResetEvent(false);
        private readonly string _logFilePath;
        private bool _running = true;
        private string file_name;

        public Logger() {
            DateTime now = DateTime.Now;
            string current_date = now.ToString();
            current_date = current_date.Replace(" ", "_");
            current_date = current_date.Replace(":", "-");
            _logFilePath = Directory.GetCurrentDirectory() + $"\\Logs{current_date}.txt";
            Thread logThread = new Thread(ProcessLogQueue) {
                IsBackground = true
            };
            logThread.Start();
        }

        public void Log(string message) {
            _logQueue.Enqueue(message);
            _logSignal.Set();
        }

        private void ProcessLogQueue() {
            while (_running) {
                _logSignal.WaitOne();
                while (_logQueue.TryDequeue(out string logEntry)) {
                    File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);
                }
            }
        }

        public void Stop() {
            _running = false;
            _logSignal.Set();  // Ensure we unblock the log thread if it's waiting
        }
    }
}
