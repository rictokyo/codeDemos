using SampleBundleApi;

namespace Monad.Identity
{
    public class MaybeMonad : Maybe<IMonad>, IMonad
    {
        public MaybeMonad(IMonad maybeThing, ICommunicator communicator)
            : base(maybeThing, communicator)
        {
        }

        public IMonadState State
        {
            get
            {
                var x = MaybeGet(() => this.MaybeThing.State);

                return x as IMonadState;
            }
        }

        public void DoStuff()
        {
            MaybeDo(_ => this.MaybeThing.DoStuff());
        }

        public static bool operator ==(MaybeMonad m1, MaybeMonad m2)
        {
            if (m1 == null || m2 == null) return false;

            return m1.State == m2.State;
        }

        public static bool operator !=(MaybeMonad m1, MaybeMonad m2)
        {
            return !(m1 == m2);
        }

        protected bool Equals(MaybeMonad obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            return this == obj;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as MaybeMonad);
        }

        public override int GetHashCode()
        {
            if (this.State == null) return this.MaybeThing.GetHashCode();

            return this.MaybeThing.GetHashCode() ^ this.State.GetHashCode();
        }
    }
}