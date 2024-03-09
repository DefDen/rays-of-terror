using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    public Rigidbody player;

    private float walkingInc = 0.5f;
    private float walkingReset = 0;

    private float animationDegree = 40f;
    
    private bool isHit = false;

    public float lookRotateSpeed = 1000f;


    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }

    void FixedUpdate()
    {
        RotateTowardsWalking();
    }

    void LateUpdate()
    {
        if (isHit)
        {
            agent.enabled = false;
        }
        else
        {
            agent.enabled = true;
            agent.SetDestination(player.position);
            
            
            WalkingAnimation();
            
        }

        isHit = false;
    }
    
    public void GotHit()
    {
        Debug.Log("I got hit");
        isHit = true;
    }

    void RotateTowardsWalking()
    {
         if (agent.velocity != Vector3.zero) // Ensure the agent is moving before rotating
        {
            Vector3 direction = (agent.steeringTarget - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation * Quaternion.Euler(0f, -90f, 0f), lookRotateSpeed * Time.fixedDeltaTime);
        }
    }

    void WalkingAnimation() {

        if (Math.Abs(walkingReset) > animationDegree) {
            walkingInc = -walkingInc;
        }
        transform.Find("BodyFrame").Find("Body").Rotate(-walkingInc * Vector3.up / 10);
        // Limping leg
        transform.Find("BodyFrame").Find("LimpingLegFrame").Rotate(-walkingInc * Vector3.forward);
        // Working leg
        transform.Find("BodyFrame").Find("WalkingLegFrame").Rotate(walkingInc * Vector3.forward);
        if (walkingReset > 0) {
            transform.Find("BodyFrame").Find("WalkingLegFrame").Find("WalkingShinFrame").Rotate(-walkingInc * Vector3.forward);
        }
        // Arms
        transform.Find("BodyFrame").Find("RightArmFrame").Rotate(-walkingInc * Vector3.forward / 10);
        transform.Find("BodyFrame").Find("LeftArmFrame").Rotate(walkingInc * Vector3.forward / 10);
        // Head
        transform.Find("BodyFrame").Find("HeadFrame").Rotate(-walkingInc * Vector3.forward / 10);
        // Reset
        if (walkingReset == 0 && walkingInc > 0) {
            transform.Find("BodyFrame").Find("WalkingLegFrame").Find("WalkingShinFrame").Rotate(-walkingInc * Vector3.forward);
        }

        walkingReset += walkingInc;
    }
}
