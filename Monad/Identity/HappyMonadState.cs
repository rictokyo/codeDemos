using SampleBundleApi;

namespace Monad.Identity
{
    public class HappyMonadState : IMonadState
    {
        private readonly ICommunicator communicator;

        public HappyMonadState(ICommunicator communicator)
        {
            this.communicator = communicator;
        }

        public void ExpressState()
        {
            this.communicator.Say("happy");
        }
    }
}