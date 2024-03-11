using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    public Rigidbody player;
    public float lookRotateSpeed = 1000f;

    private float _walkingInc = 0.5f;
    private float _walkingReset = 0;
    private float _animationDegree = 40f;
    private bool _isHit;
    
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
        if (_isHit)
        {
            agent.enabled = false;
        }
        else
        {
            agent.enabled = true;
            agent.SetDestination(player.position);
            WalkingAnimation();
        }
        _isHit = false;
    }
    
    public void GotHit()
    {
        _isHit = true;
    }

    void RotateTowardsWalking()
    {
        if (agent.velocity != Vector3.zero)
        {
            Vector3 direction = (agent.steeringTarget - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation * Quaternion.Euler(0f, -90f, 0f), lookRotateSpeed * Time.fixedDeltaTime);
        }
    }

    void WalkingAnimation() {

        if (Math.Abs(_walkingReset) > _animationDegree) {
            _walkingInc = -_walkingInc;
        }
        transform.Find("BodyFrame").Find("Body").Rotate(-_walkingInc * Vector3.up / 10);
        // Limping leg
        transform.Find("BodyFrame").Find("LimpingLegFrame").Rotate(-_walkingInc * Vector3.forward);
        // Working leg
        transform.Find("BodyFrame").Find("WalkingLegFrame").Rotate(_walkingInc * Vector3.forward);
        if (_walkingReset > 0) {
            transform.Find("BodyFrame").Find("WalkingLegFrame").Find("WalkingShinFrame").Rotate(-_walkingInc * Vector3.forward);
        }
        // Arms
        transform.Find("BodyFrame").Find("RightArmFrame").Rotate(-_walkingInc * Vector3.forward / 10);
        transform.Find("BodyFrame").Find("LeftArmFrame").Rotate(_walkingInc * Vector3.forward / 10);
        // Head
        transform.Find("BodyFrame").Find("HeadFrame").Rotate(-_walkingInc * Vector3.forward / 10);
        // Reset
        if (_walkingReset == 0 && _walkingInc > 0) {
            transform.Find("BodyFrame").Find("WalkingLegFrame").Find("WalkingShinFrame").Rotate(-_walkingInc * Vector3.forward);
        }
        _walkingReset += _walkingInc;
    }
}
