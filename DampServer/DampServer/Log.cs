using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DampServer
{
    class Logger
    {
        public static void Log(string message, LogLevel level = LogLevel.Normal)
        {
            var stackFrame = new StackFrame(1, true);

            var method = stackFrame.GetMethod().ToString();
            var line = stackFrame.GetFileLineNumber();

            Console.Write("Log: Line: ");
            Console.Write(line + ", Method:" + method);
            Console.WriteLine(" {0}", message);
        }
    }


    enum LogLevel
    {
        Minor,
        Normal,
        Critical

    }
}
