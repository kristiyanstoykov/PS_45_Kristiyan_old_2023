using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeExtended.Helpers;

namespace WelcomeExtended.Others
{
    internal class Delegates
    {
        public static readonly ILogger logger = LoggerHelper.GetLogger("Hello");
        public static readonly ILogger fileLogger = LoggerHelper.GetFileLogger("error.log");
        public static readonly ILogger userLoginFileLogger = LoggerHelper.GetFileLogger("user.log");

        public delegate void ActionOnError(string errorMessage);
        public delegate void ActionOnSuccess(string successMessage);

        public static void Log(string error)
        {
            logger.LogError(error);
        }

        public static void Log2(string error)
        {
            Console.WriteLine("-- DELEGATES --");
            Console.WriteLine($"{error}");
            Console.WriteLine("-- DELEGATES --");
        }

        public static void FileLog(string error)
        {
            fileLogger.LogError(error);
        }

        public static void UserLogError(string error)
        {
            userLoginFileLogger.LogError(error);
        }

        public static void UserLogSuccess(string message)
        {
            userLoginFileLogger.LogInformation(message);
        }

    }
}
