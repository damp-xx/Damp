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

            
            /*********** Sending a playerName **********************/
            StreamWriter sw = new StreamWriter(pipeServerOut);
            {
                sw.AutoFlush = true;
                // Send a 'sync message' and wait for client to receive it.
                sw.WriteLine("CPN:Anders Hvirvelkær");
                sw.WriteLine("CHS:10000");
                pipeServerOut.WaitForPipeDrain();
            }
            /*********************************************************************/

            /******************** Receiving new Highscore ***********************/
            StreamReader sr = new StreamReader(pipeServerIn);          
                string receivedString = sr.ReadLine();
            Console.WriteLine(receivedString);
            /*********************************************************************/

            /******************** Receiving new Highscore ***********************/
            string receivedStringAch = sr.ReadLine();
            Console.WriteLine(receivedStringAch);
            /*********************************************************************/
            
            Console.ReadLine();
        }
    }
}