using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;

namespace DampStub
{
    internal class GameStarter
    {
        private static void Main(string[] args)
        {
            Process pipeClient = new Process();
            pipeClient.StartInfo.FileName =
                @"../../../SuperIHABrothers/SuperIHABrothers/bin/x86/Debug/SuperIHABrothers.exe";


            AnonymousPipeServerStream pipeServerOut = new AnonymousPipeServerStream(PipeDirection.Out, HandleInheritability.Inheritable);

            AnonymousPipeServerStream pipeServerIn = new AnonymousPipeServerStream(PipeDirection.In, HandleInheritability.Inheritable);

            pipeClient.StartInfo.Arguments = pipeServerOut.GetClientHandleAsString();
            pipeClient.StartInfo.Arguments += " ";
            pipeClient.StartInfo.Arguments += pipeServerIn.GetClientHandleAsString();

            pipeClient.StartInfo.UseShellExecute = false;

            pipeClient.Start();

            Console.ReadLine();
        }
    }
}