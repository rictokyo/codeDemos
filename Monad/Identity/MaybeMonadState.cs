using SampleBundleApi;

namespace Monad.Identity
{
    public class MaybeMonadState : Maybe<IMonadState>, IMonadState
    {
        public MaybeMonadState(IMonadState maybeThing, ICommunicator communicator)
            : base(maybeThing, communicator)
        {
        }

        public void ExpressState()
        {
            MaybeDo(_ => this.MaybeThing.ExpressState());
        }
    }
}