using System;
using System.IO;
using System.IO.Pipes;
using System.Diagnostics;
using System.Threading;

class PipeServer
{
    static void Main()
    {
        Process pipeClient = new Process();

        pipeClient.StartInfo.FileName = @"../../../Client/bin/Debug/Client.exe";
        

        using (AnonymousPipeServerStream pipeServerOut =
            new AnonymousPipeServerStream(PipeDirection.Out,
            HandleInheritability.Inheritable))
        {
            AnonymousPipeServerStream pipeServerIn = new AnonymousPipeServerStream(PipeDirection.In, HandleInheritability.Inheritable);

            //// Show that anonymous pipes do not support Message mode. 
            //try
            //{
            //    Console.WriteLine("[SERVER] Setting ReadMode to \"Message\".");
            //    pipeServerOut.ReadMode = PipeTransmissionMode.Message;
            //}
            //catch (NotSupportedException e)
            //{
            //    Console.WriteLine("[SERVER] Exception:\n    {0}", e.Message);
            //}

            //Console.WriteLine("[SERVER] Current TransmissionMode: {0}.",
            //    pipeServerOut.TransmissionMode);

            // Pass the client process a handle to the server.
            pipeClient.StartInfo.Arguments =
                pipeServerOut.GetClientHandleAsString();
            pipeClient.StartInfo.Arguments += " ";
            pipeClient.StartInfo.Arguments += pipeServerIn.GetClientHandleAsString();
            
            pipeClient.StartInfo.UseShellExecute = false;
            
            pipeClient.Start();

            pipeServerOut.DisposeLocalCopyOfClientHandle();

            try
            {
                // Read user input and send that to the client process. 
                using (StreamWriter sw = new StreamWriter(pipeServerOut))
                {
                    sw.AutoFlush = true;
                    // Send a 'sync message' and wait for client to receive it.
                    sw.WriteLine("SYNC");
                    pipeServerOut.WaitForPipeDrain();
                    // Send the console input to the client process.
                    Console.Write("[SERVER] Enter text: ");
                    sw.WriteLine(Console.ReadLine());

                    using (StreamReader sr = new StreamReader(pipeServerIn))
                    {
                        // Display the read text to the console 
                        string temp;
                        Thread.Sleep(1000);

                        temp = sr.ReadLine();
                        {
                            Console.WriteLine(temp);
                       }
                        
                    }
                    
                }
            }
            // Catch the IOException that is raised if the pipe is broken 
            // or disconnected. 
            catch (IOException e)
            {
                Console.WriteLine("[SERVER] Error: {0}", e.Message);
            }
        }

        pipeClient.WaitForExit();
        pipeClient.Close();
        Console.WriteLine("[SERVER] Client quit. Server terminating.");
    }
}