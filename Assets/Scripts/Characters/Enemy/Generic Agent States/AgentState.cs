public abstract class AgentState
{
    public Enemy agent;

    public AgentState() : base(){ }

    public abstract void Update();
    public abstract void OnEnter();
    public abstract void OnExit();
}