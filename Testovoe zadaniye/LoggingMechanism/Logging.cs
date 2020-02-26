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


    //class Logging
    //{
    //    private static Logging instance;

    //    private Logging()
    //    { }

    //    public void LoggMessage(string className, string message, [CallerMemberName] string memberName = "")
    //    {
    //        System.IO.File.AppendAllText($"Logs/mylog.txt", $"\n {DateTime.Now} Class {className} - method: {memberName} - action: {message}");
    //    }

    //    public static Logging getInstance()
    //    { 
    //        if (instance == null)
    //        instance = new Logging();
    //        return instance;
    //    }
    //}

    abstract class Logger
    {
        
    }

    class ConsoleLogger : Logger
    {
        public void LoggMessage(string className, string message, [CallerMemberName] string memberName = "")
        {
            Console.WriteLine($"\n {DateTime.Now} Class {className} - method: {memberName} - action: {message}");
        }
    }

    class TxtLogger : Logger
    {
        public void LoggMessage(string className, string message, [CallerMemberName] string memberName = "")
        {
            System.IO.File.AppendAllText($"Logs/mylog.txt", $"\n {DateTime.Now} Class {className} - method: {memberName} - action: {message}");
        }
    }

    abstract class LoggerCreator
    {
        public abstract Logger FactoryMethod();
    }

    class ConsoleLoggerCreator : LoggerCreator
    {
        public override Logger FactoryMethod() { return new ConsoleLogger(); }
    }

    class TxtLoggerCreator : LoggerCreator
    {
        public override Logger FactoryMethod() { return new TxtLogger(); }
    }
}

