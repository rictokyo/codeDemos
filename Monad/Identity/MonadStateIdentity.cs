using System.Collections.Generic;

namespace Monad.Identity
{
    public class MonadStateIdentity : Identity<IMonadState>, IMonadState
    {
        private readonly IEnumerator<IMonadState> statesEnumerator;

        public MonadStateIdentity(IEnumerable<IMonadState> monkeyStates)
        {
            this.statesEnumerator = monkeyStates.GetEnumerator();
        }

        public void ExpressState()
        {
            if (!this.statesEnumerator.MoveNext())
            {
                this.statesEnumerator.Reset();
                this.statesEnumerator.MoveNext();
            }

            this.Me = this.statesEnumerator.Current;

            this.Me.ExpressState();
        }
    }
}