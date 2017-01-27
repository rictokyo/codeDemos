using System;
using SampleBundleApi;

namespace Communicator
{
    public class Communicator : ICommunicator
    {
        public void Say(string message)
        {
            Console.WriteLine(message);
        }
    }
}
