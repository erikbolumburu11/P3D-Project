using System.Linq;
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
        Vector3 agentPos = agent.transform.position;
        Vector3 playerPos = FindObject.FindPlayer().transform.position;

        // Logic


        //// Chase
        agent.GetComponent<NavMeshAgent>().SetDestination(FindObject.FindPlayer().transform.position);

        Vector3[] corners = agent.GetComponent<NavMeshAgent>().path.corners;
        float pathDistance = 0;

        for (int i = 1; i < corners.Length; i++)
        {
            pathDistance += Vector3.Distance(corners[i - 1], corners[i]);
        }

        if(pathDistance > agent.detectionRange) {
            agent.SetState(new IdleState(agent));
            return;
        }

        //// Attack
        if(Vector3.Distance(agentPos, playerPos) <= agent.attackRange){
            agent.GetComponent<Animator>().SetTrigger("Attack");
        }

        // Transition
        if(Vector3.Distance(agentPos, playerPos) >= agent.detectionRange){
            agent.SetState(new IdleState(agent));
        }
    }
}