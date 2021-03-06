﻿using System;
using System.Linq;
using SampleBundleApi;
using Spring.Context.Support;

namespace ConsoleTester
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var ctx = ContextRegistry.GetContext();

            var samples = ctx.GetObjectsOfType(typeof(ISample));

            var i = 0;

            
            Console.WriteLine();

            foreach (ISample sample in samples.Values)
            {
                Console.WriteLine("{0} - {1}", sample.SampleName, i++);
            }

            Console.WriteLine();
            Console.WriteLine("{0} - {1}", "Quit", "q");

            Console.WriteLine("Enter a choice:");
            var choice = Console.ReadLine();

            if (!(string.Compare(choice, "q", StringComparison.InvariantCultureIgnoreCase) == 0) && !string.IsNullOrEmpty(choice))
            {
                int choiceNumber;

                if (int.TryParse(choice, out choiceNumber))
                {
                    var sample = samples.Values.ToArray()[choiceNumber] as ISample;

                    if (sample != null)
                    {
                        sample.Start();
                    }
                }
            }

            Console.WriteLine("Press any key to exit..");

            foreach(ISample sample in samples.Values)
            {
                sample.Dispose();
            }

            Console.ReadLine();
        }
    }
}
