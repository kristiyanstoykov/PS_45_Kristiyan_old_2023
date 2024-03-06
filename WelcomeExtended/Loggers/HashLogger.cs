using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;


namespace WelcomeExtended.Loggers
{
    internal class HashLogger : ILogger
    {

        private readonly ConcurrentDictionary<int, string> _logMessage;
        private readonly String _name;

        public HashLogger(String name)
        {
            _logMessage = new ConcurrentDictionary<int, string>();
            _name = name;
        }

        public IDisposable? BeginScope<TState>(TState state)
        {
            // This method does not support scopes
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            // The logger is always enabled
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception, string> formatter)
        {
            var message = formatter(state, exception);

            switch(logLevel)
            {
                case LogLevel.Critical:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case LogLevel.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            Console.WriteLine("- LOGGER -");
            var messageToBeLogged = new StringBuilder();
            messageToBeLogged.AppendLine($"[{logLevel}]");
            messageToBeLogged.AppendFormat(" [{0}]", _name);
            Console.WriteLine(messageToBeLogged);
            Console.WriteLine($" {formatter(state, exception)}");
            Console.WriteLine("- LOGGER -");
            Console.ResetColor();
            _logMessage[eventId.Id] = message;

        }

        public void PrintAllMessages()
        {
            Console.WriteLine($"- All Messages Logged by {_name} -");
            foreach (var entry in _logMessage)
            {
                Console.WriteLine($"ID {entry.Key}: {entry.Value}");
            }
            Console.WriteLine("- End of Messages -");
        }

        public void PrintMessageById(int id)
        {
            Console.WriteLine($"- Message with ID {id} -");
            if (_logMessage.TryGetValue(id, out var message))
            {
                Console.WriteLine(message);
            }
            else
            {
                Console.WriteLine("No message with that ID");
            }
            Console.WriteLine("- End of Message -");
        }

        public void DeleteMessageById(int id)
        {
            Console.WriteLine($"- Deleting Message with ID {id} -");
            if (_logMessage.TryRemove(id, out var message))
            {
                Console.WriteLine("Message Deleted");
            }
            else
            {
                Console.WriteLine("No message with that ID");
            }
            Console.WriteLine("- End of Deletion -");
        }

    }
}
