using System;
using System.Diagnostics;

namespace DampServer
{
    internal class Logger
    {
        public static void Log(string message, LogLevel level = LogLevel.Normal)
        {
            StackFrame stackFrame = new StackFrame(1, true);

            string method = stackFrame.GetMethod().ToString();
            int line = stackFrame.GetFileLineNumber();

            Console.Write("Log: Line: ");
            Console.Write(line + ", Method:" + method);
            Console.WriteLine(" {0}", stackFrame);
            Console.WriteLine(" {0}", message);
        }
    }


    internal enum LogLevel
    {
        Minor,
        Normal,
        Critical
    }
}