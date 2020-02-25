using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Testovoe_zadaniye.LoggingMechanism
{
    public class Logging
    {
        public string ClassOrMethodName { get; set; }
        public void LoggMassage(string partName)
        {
            System.IO.File.AppendAllText($"Logs/mylog.txt", $"\n {DateTime.Now} method {partName} - done");
        }
        public void LoggMassageClass(string partName)
        {
            System.IO.File.AppendAllText($"Logs/mylog.txt", $"\n {DateTime.Now} Class {partName} - done");
        }
        public string DefineMethodName([CallerMemberName] string memberName = "")
        {
            var a = memberName;
            return a;
        }
       


    }
}
