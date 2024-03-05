using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Debug.Log(agent.SetDestination(new Vector3(-78f, 12f, 0f)));
        
    }
}
