using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Testovoe_zadaniye.LoggingMechanism
{
    //public class Logging
    //{
    //    public string LoggMessage(string className, string message, [CallerMemberName] string memberName = "")
    //    {
    //        string p = System.IO.File.AppendAllText($"Logs/mylog.txt", $"\n {DateTime.Now} Class {className} - method: {memberName} - action: {message}");
    //    }


    public class Logging
    {
        private static Logging instance;

        private Logging()
        { }

        public void LoggMessage(string className, string message, [CallerMemberName] string memberName = "")
        {
            System.IO.File.AppendAllText($"Logs/mylog.txt", $"\n {DateTime.Now} Class {className} - method: {memberName} - action: {message}");
        }

        public static Logging getInstance()
        {
            if (instance == null)
                instance = new Logging();
            return instance;
        }
    }

    abstract class Logger
    {

        public abstract void LoggMessage(string className, string message, [CallerMemberName] string memberName = "");
    }

    class ConsoleLogger : Logger
    {
        private static ConsoleLogger instance;

        private ConsoleLogger()
        { }

        public override void LoggMessage(string className, string message, [CallerMemberName] string memberName = "")
        {
            System.IO.File.AppendAllText($"Logs/mylog.txt", $"\n {DateTime.Now} Class {className} - method: {memberName} - action: {message}");
        }

        public static ConsoleLogger getInstance()
        {
            if (instance == null)
                instance = new ConsoleLogger();
            return instance;
        }
    }

    class TxtLogger : Logger
    {
        private static TxtLogger instance;

        private TxtLogger()
        { }

        public override void LoggMessage(string className, string message, [CallerMemberName] string memberName = "")
        {
            System.IO.File.AppendAllText($"Logs/mylog.txt", $"\n {DateTime.Now} Class {className} - method: {memberName} - action: {message}");
        }

        public static TxtLogger getInstance()
        {
            if (instance == null)
                instance = new TxtLogger();
            return instance;
        }
    }

    abstract class LoggerCreator
    {
        public abstract Logger FactoryMethod();
    }

    class ConsoleLoggerCreator : LoggerCreator
    {
        public override Logger FactoryMethod() { return ConsoleLogger.getInstance(); }
    }

    class TxtLoggerCreator : LoggerCreator
    {
        public override Logger FactoryMethod() { return ConsoleLogger.getInstance(); }
    }
}

