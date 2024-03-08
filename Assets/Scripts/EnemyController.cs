using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    public Rigidbody player;
    
    private bool isHit = false;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (isHit)
        {
            agent.enabled = false;
        }
        else
        {
            agent.enabled = true;
            agent.SetDestination(player.position);
        }

        isHit = false;
    }
    
    public void GotHit()
    {
        Debug.Log("I got hit");
        isHit = true;
    }
}
