namespace Monad.Identity
{
    public interface IIdentity<out T>
    {
        T Me { get; }
    }
}