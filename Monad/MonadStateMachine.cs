using Monad.Identity;
using SampleBundleApi;

namespace Monad
{
    class MonadStateMachine : Sample, IMonadStateMachine
    {
        private readonly ICommunicator communicator;

        public MonadStateMachine(string sampleName, ICommunicator communicator) : base(sampleName)
        {
            this.communicator = communicator;
        }

        public override void Start()
        {
            var monadStates = new IMonadState[]
            {
                new MaybeMonadState(new CalmMonadState(this.communicator), this.communicator),
                new MaybeMonadState(new HappyMonadState(this.communicator), this.communicator),
                new MaybeMonadState(null, this.communicator),
            };

            var monadStateIdentity = new MonadStateIdentity(monadStates);

            var monad1 = new Identity.Monad(monadStateIdentity);
            var monad2 = new MaybeMonad(monad1, this.communicator);

            for (var i = 0; i < 5; i++)
            {
                monad2.DoStuff();
            }
        }

        public override void Dispose()
        {

        }
    }
}
