using ConsoleTelemtry.Other;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/** 
 * 
 * http://www.c-sharpcorner.com/article/using-azure-application-insights-in-powershell/
 * 
 * **/
namespace ConsoleTelemtry
{
    class Program
    {
        static Microsoft.ApplicationInsights.TelemetryClient tc;

        static void Main(string[] args)
        {
            string msg = string.Empty;

            if (args.Length > 0)
            {
                msg = String.Join(" ", args);
                Console.WriteLine(msg);

            }


            tc = new Microsoft.ApplicationInsights.TelemetryClient();
            tc.InstrumentationKey = " -b03d-431b-a82d-f3ab8b9c929d";// 
                                                                           // Set session data:
            tc.Context.User.Id = Environment.UserName;
            tc.Context.Session.Id = Guid.NewGuid().ToString();
            tc.Context.Device.OperatingSystem = Environment.OSVersion.ToString();
            tc.Context.Location.Ip = Class1.GetLocalIPAddress();

            string input = string.Empty;
            Console.WriteLine("Enter one or more lines of text (press CTRL+Z to exit):");
            Console.WriteLine();
            do
            {
                //Console.Write("   ");
                input = Console.ReadLine();
                
                if (input != null)
                {
                    //input = input.Trim();
                    
                    if (String.IsNullOrWhiteSpace(input))
                    {
                        ;//continue;
                    }

                    Console.WriteLine("\tData read was " + input);
                }


                //Console.SetIn(new StreamReader(Console.OpenStandardInput(8192))); // This will allow input >256 chars

                //while (Console.In.Peek() != -1)
                //while (true)
                //{
                //    input = Console.In.ReadLine();
                //    Console.WriteLine("\tData read was " + input);
                //    if (0 == string.Compare(input, "quit", true))
                //        break;
                //}
                {

                // Log a page view:
                tc.TrackPageView("Form1");

            Dictionary<string, string> li = new Dictionary<string, string>();
            li.Add("LEVEL", "ERROR");

            string temp = (args.Length > 0) ? args[0] : "";
            string right = string.Format("{0}-{1}", "tbd.exe", msg);
            li.Add("NAME", right);

            temp = (args.Length > 1) ? args[1] : "n/a";
            right = string.Format("{0}", input);
            li.Add("TEXT", right);

            //tc.TrackEvent("hithere", null);
            tc.TrackEvent("log", li);

            //.TelemetryClient();
            Console.WriteLine("\tDid it go.(" + input + ")");

            }   // end of read input

            } while (input != null); // end of input do!
            OnClosing();

        }

        static private void OnClosing()
        {            
            if (tc != null)
            {
                tc.Flush(); // only for desktop apps

                // Allow time for flushing:
                System.Threading.Thread.Sleep(1000);
            }

            Console.WriteLine("Good bye cruel world.");
        }

    }
}
