using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WelcomeExtended.Loggers
{
    internal class FileLogger : ILogger
    {
        private readonly string _filePath;
        private readonly object _lock = new object();

        public FileLogger(string fileName)
        {
            // Assuming the application runs in the bin/Debug or bin/Release folder,
            // move two levels up to get to the project root.
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string projectRoot = Path.GetFullPath(Path.Combine(baseDir, @"..\..\.."));
            string logDir = Path.Combine(projectRoot, "logs");

            if (!Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir);
            }

            _filePath = Path.Combine(logDir, fileName);
        }

        public IDisposable? BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel)
        {
            return true; // Enables all log levels; adjust as necessary.
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            string message = formatter(state, exception);
            if (!string.IsNullOrWhiteSpace(message))
            {
                lock (_lock)
                {
                    File.AppendAllText(_filePath, $"{DateTime.Now} [{logLevel}] {message}\n");
                }
            }
        }
    }
}
