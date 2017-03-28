namespace SampleBundleApi
{
    public abstract class Sample : ISample
    {
        protected Sample(string sampleName)
        {
            this.SampleName = sampleName;
        }

        public string SampleName { get; private set; }

        public abstract void Dispose();
        public abstract void Start();
    }
}