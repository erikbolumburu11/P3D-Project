using UnityEngine;
using UnityEngine.AI;

public class ChaseState : AgentState
{
    public ChaseState(Enemy agent){
        this.agent = agent;
    }
    
    public override void OnEnter()
    {
        Debug.Log("Chase State Entered");
    }

    public override void OnExit()
    {
        Debug.Log("Chase State Exited");
    }

    public override void Update()
    {
        // Logic
        agent.GetComponent<NavMeshAgent>().SetDestination(FindObject.FindPlayer().transform.position);

        // Transition
        Vector3 agentPos = agent.transform.position;
        Vector3 playerPos = FindObject.FindPlayer().transform.position;
        if(Vector3.Distance(agentPos, playerPos) >= agent.detectionRange){
            agent.SetState(new IdleState(agent));
        }
    }
}