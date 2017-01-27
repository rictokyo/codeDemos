namespace Monad.Identity
{
    public class Monad : IMonad
    {
        public Monad(IMonadState state)
        {
            this.State = state;
        }

        public IMonadState State { get; private set; }

        public void DoStuff()
        {
            this.State.ExpressState();
        }

    }
}