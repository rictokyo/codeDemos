using System;
using System.Linq.Expressions;
using SampleBundleApi;

namespace Monad.Identity
{
    public class Maybe<T> : IMaybe<T> where T : class
    {
        protected readonly T MaybeThing;
        private readonly ICommunicator communicator;

        public Maybe(T maybeThing, ICommunicator communicator)
        {
            this.MaybeThing = maybeThing;
            this.communicator = communicator;
        }

        public void MaybeDo(Expression<Action<T>> a)
        {
            var methodCall = a.Body as MethodCallExpression;

            if (methodCall == null) return;

            if (this.MaybeThing != null)
            {
                a.Compile()(this.MaybeThing);
            }
            else
            {
                var methodName = methodCall.Method.Name;
                var messageBoxText = string.Format("no can do: {0}", methodName);

                this.communicator.Say(messageBoxText);
            }
        }

        public object MaybeGet(Func<object> f)
        {
            if (this.MaybeThing != null)
            {
                return f.Invoke();
            }

            return null;
        }
    }
}