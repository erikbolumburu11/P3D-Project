public class TestEnemy : Enemy
{
    protected override void Awake()
    {
        currentState = new IdleState(this);
    }
}