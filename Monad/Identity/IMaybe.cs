using System;
using System.Linq.Expressions;

namespace Monad.Identity
{
    public interface IMaybe<T> where T : class
    {
        void MaybeDo(Expression<Action<T>> a);
        object MaybeGet(Func<object> f);
    }
}