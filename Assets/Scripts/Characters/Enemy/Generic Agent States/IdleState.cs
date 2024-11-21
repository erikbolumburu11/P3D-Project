using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : AgentState
{
    public IdleState(Enemy agent){
        this.agent = agent;
    }
    
    public override void OnEnter()
    {
        agent.GetComponent<NavMeshAgent>().SetDestination(agent.transform.position);
        Debug.Log("Idle State Entered");
    }

    public override void OnExit()
    {
        Debug.Log("Idle State Exited");
    }

    public override void Update()
    {
        Vector3 agentPos = agent.transform.position;
        Vector3 playerPos = FindObject.FindPlayer().transform.position;
        if(Vector3.Distance(agentPos, playerPos) < agent.detectionRange){
            agent.SetState(new ChaseState(agent));
        }
    }
}
