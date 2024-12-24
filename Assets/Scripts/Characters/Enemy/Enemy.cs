using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public AgentState currentState;
    Animator animator;
    NavMeshAgent agent;

    public float moveSpeed;
    public float detectionRange;
    public float attackRange;

    [Header("Debug")]
    [SerializeField] string currentStateName;

    protected virtual void Awake(){
        SetState(new IdleState(this));

        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
    }

    void Update(){
        currentState.Update();

        currentStateName = currentState.GetType().Name;

        SetAnimationParameters();
    }

    void SetAnimationParameters(){
        animator.SetBool("isMoving", agent.velocity.magnitude > 0.2f);
    }

    public void SetState(AgentState state){
        currentState?.OnExit();

        currentState = state;

        currentState.OnEnter();
    }
}