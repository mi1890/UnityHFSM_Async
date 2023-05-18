namespace FSM
{
    /// <summary>
    /// A ReverseTransition wraps another transition, but reverses it. The "from"
    /// and "to" states are swapped. Only when the condition of the wrapped transition
    /// is false does it transition.
    /// </summary>
    public class AsyncReverseTransition<TStateId> : AsyncTransitionBase<TStateId>
    {
        public AsyncTransitionBase<TStateId> wrappedTransition;
        private bool shouldInitWrappedTransition;

        public AsyncReverseTransition(
                AsyncTransitionBase<TStateId> wrappedTransition,
                bool shouldInitWrappedTransition = true)
            : base(
                from: wrappedTransition.to,
                to: wrappedTransition.from,
                forceInstantly: wrappedTransition.forceInstantly)
        {
            this.wrappedTransition = wrappedTransition;
            this.shouldInitWrappedTransition = shouldInitWrappedTransition;
        }

        public override void Init()
        {
            if (shouldInitWrappedTransition)
            {
                wrappedTransition.fsm = this.fsm;
                wrappedTransition.Init();
            }
        }

        public override void OnEnter()
        {
            wrappedTransition.OnEnter();
        }

        public override bool ShouldTransition()
        {
            return !wrappedTransition.ShouldTransition();
        }
    }

    public class AsyncReverseTransition : AsyncReverseTransition<string>
    {
        public AsyncReverseTransition(
            AsyncTransitionBase<string> wrappedTransition,
            bool shouldInitWrappedTransition = true)
            : base(wrappedTransition, shouldInitWrappedTransition)
        {
        }
    }
}