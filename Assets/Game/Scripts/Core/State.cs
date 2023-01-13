namespace Game.Scripts.Core
{
    public class State
    {
        public delegate void OnUpdate();
        public OnUpdate onUpdate;

        public delegate void OnStateEnter();
        public OnStateEnter onStateEnter;

        public delegate void OnStateExit();
        public OnStateExit onStateExit;

        public State(OnStateEnter onStateEnter, OnUpdate onUpdate, OnStateExit onStateExit)
        {
            this.onStateEnter = onStateEnter ?? (() => { });
            this.onUpdate = onUpdate ?? (() => { });
            this.onStateExit = onStateExit ?? (() => { });
        }
    }
}