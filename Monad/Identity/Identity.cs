namespace Monad.Identity
{
    public class Identity<T> : IIdentity<T>
    {
        public T Me { get; set; }
    }
}