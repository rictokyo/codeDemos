using SampleBundleApi;

namespace StrategyResolver
{
    class StrategyResolver : Sample, IStrategyResolver
    {
        private readonly ICommunicator communicator;

        public StrategyResolver(string sampleName, ICommunicator communicator) : base(sampleName)
        {
            this.communicator = communicator;
        }

        public override void Start()
        {
            var message = string.Format("Started {0}", this.SampleName);

            this.communicator.Say(message);
        }

        public void Resolve(IConditionalState state)
        {
            
        }
    }
}
