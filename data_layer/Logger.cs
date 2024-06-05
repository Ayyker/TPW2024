using System.Text.Json;
using System.Timers;

namespace data_layer {
    public class Logger {
        private System.Timers.Timer timer;
        private readonly List<Ball> balls;

        public Logger(List<Ball> b) {
            this.balls = b;
            SetTimer();
        }
        private void OnTimedEvent(Object obj, ElapsedEventArgs e) {
            using StreamWriter streamWriter = new StreamWriter(Directory.GetCurrentDirectory() + "\\Logs.txt", true);
            string msg = ("Date: " + e.SignalTime + "\n");
            foreach (Ball ball in balls) {
                streamWriter.WriteLine(msg + JsonSerializer.Serialize(ball));
            }
        } 
        private void SetTimer() {
            timer = new System.Timers.Timer(100);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }
        public void Stop() {
            timer.Stop();
        }
    }
}
