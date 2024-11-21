public class SkeletonEnemy : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        currentState = new IdleState(this);
    }
}