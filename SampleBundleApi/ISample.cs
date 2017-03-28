using System;

namespace SampleBundleApi
{
    public interface ISample : IDisposable
    {
        string SampleName { get; }
        void Start();
    }
}