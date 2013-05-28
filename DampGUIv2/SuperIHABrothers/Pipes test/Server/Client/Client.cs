using System;
using System.IO;
using System.IO.Pipes;

class PipeClient
{
    static void Main(string[] args)
    {
        if (args.Length > 0)
        {
            using (PipeStream pipeClientIn =
                new AnonymousPipeClientStream(PipeDirection.In, args[0]))
            {
                PipeStream pipeClientOut = new AnonymousPipeClientStream(PipeDirection.Out, args[1]);

                Console.WriteLine(args[0]);
                Console.WriteLine(args[1]);

                // Show that anonymous Pipes do not support Message mode. 
                //try
                //{
                //    Console.WriteLine("[CLIENT] Setting ReadMode to \"Message\".");
                //    pipeClientIn.ReadMode = PipeTransmissionMode.Message;
                //}
                //catch (NotSupportedException e)
                //{
                //    Console.WriteLine("[CLIENT] Execption:\n    {0}", e.Message);
                //}

                //Console.WriteLine("[CLIENT] Current TransmissionMode: {0}.",
                //   pipeClientIn.TransmissionMode);

                using (StreamReader sr = new StreamReader(pipeClientIn))
                {
                    // Display the read text to the console 
                    string temp;

                    // Wait for 'sync message' from the server. 
                    do
                    {
                        Console.WriteLine("[CLIENT] Wait for sync...");
                        temp = sr.ReadLine();
                    }
                    while (!temp.StartsWith("SYNC"));
                

                    // Read the server data and echo to the console. 
                    temp = sr.ReadLine();
                    {
                        Console.WriteLine("[CLIENT] Echo: " + temp);
                    }

                    
                    using (StreamWriter sw = new StreamWriter(pipeClientOut))
                    {
                        sw.AutoFlush = true;
                        // Send a 'sync message' and wait for client to receive it.
                        sw.WriteLine("Whatever");
                        pipeClientOut.WaitForPipeDrain();
                    }
                }


            }
        }
        Console.Write("[CLIENT] Press Enter to continue...");
        Console.ReadLine();


    }
}