using SampleBundleApi;

namespace Monad.Identity
{
    public class CalmMonadState : IMonadState
    {
        private readonly ICommunicator communicator;

        public CalmMonadState(ICommunicator communicator)
        {
            this.communicator = communicator;
        }

        public void ExpressState()
        {
            this.communicator.Say("calm");
        }
    }
}