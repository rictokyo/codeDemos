namespace Monad.Identity
{
    public interface IMonad
    {
        IMonadState State { get; }
        void DoStuff();
    }
}