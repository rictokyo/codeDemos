namespace Monad.Identity
{
    public class Logic : ILogic
    {
        public void DoLogic(IMonad monad)
        {
            monad.DoStuff();
        }
    }
}