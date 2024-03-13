using System;
using UnityEngine;
using TMPro;

public class PlayerMover : MonoBehaviour
{
    public float speed = 2.0f;
    public Vector3 worldPosition;
    public Camera mainCamera;

    private Rigidbody rb;

    public GameObject gameOverScreen;

    public bool isPerson = false;

    public TextMeshProUGUI endText;
    
    private Plane _plane = new Plane(Vector3.up, 0); // plane that the player points to
    private const float RotationSpeed = 5f;

    private float walkingInc = 4.0f;
    private float walkingReset = 0f;

    private void Start()
    {
        gameOverScreen.SetActive(false);
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0, moveZ).normalized * speed;

        if (isPerson && movement.magnitude > 0.1f)
        {
            WalkingAnimation();
        }

        rb.velocity = movement;
        //transform.position += Time.deltaTime * movement;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {   
            EnemyController otherScript = other.GetComponent<EnemyController>();
            if (otherScript != null && otherScript.canHurtPlayer)
            {
                endText.text = "You've been infected";
                gameOverScreen.SetActive(true);
            }
        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            endText.text = "You escaped!";
            gameOverScreen.SetActive(true);
        }
    }

    void Update()
    {
        float distance;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        
        if (_plane.Raycast(ray, out distance))
        {
            worldPosition = ray.GetPoint(distance);

            Vector3 direction = worldPosition - transform.position;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                targetRotation *= Quaternion.Euler(0f, -90f, 0f); // Adding a 90-degree offset to the Y angle
                targetRotation.eulerAngles = new Vector3(0f, targetRotation.eulerAngles.y, 0f); // Locking X and Z rotation
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
            }
        }
    }

    void WalkingAnimation() { 
        if (Math.Abs(walkingReset) >= 40) {
            walkingInc = -walkingInc;
        }
       
        // Head
        transform.Find("BodyFrame").Find("HeadFrame").Rotate(-walkingInc * Vector3.forward / 10);

        // Arms
        float upperInc = 0.2f;
        float lowerInc = 0.4f;
        // Left Arm
        transform.Find("BodyFrame").Find("LeftArmFrame").Rotate(walkingInc * Vector3.forward * upperInc);
        if (walkingReset > 0) {
            transform.Find("BodyFrame").Find("LeftArmFrame").Find("ForearmFrame").Rotate(walkingInc * Vector3.forward * lowerInc);
        }
        if (walkingReset == 0 && walkingInc > 0) {
            transform.Find("BodyFrame").Find("LeftArmFrame").Find("ForearmFrame").Rotate(walkingInc * Vector3.forward * lowerInc);
        }

        // Body
        transform.Find("BodyFrame").Find("Body").Rotate(-walkingInc * Vector3.up / 10);

        // Legs
        // Left leg
        transform.Find("BodyFrame").Find("LeftLegFrame").Rotate(-walkingInc * Vector3.forward);
        if (-walkingInc > 0) {
            Transform shin = transform.Find("BodyFrame").Find("LeftLegFrame").Find("ShinFrame");
            if (-walkingReset < 10 && shin.localRotation.z > -0.25f) {
                shin.Rotate(0.5f * walkingInc * Vector3.forward);
            }
            if (-walkingReset > 10 && shin.localRotation.z < 0) {
                shin.Rotate(-1 * walkingInc * Vector3.forward);
            }
            if (shin.localRotation.z > 0) {
                shin.localRotation = Quaternion.Euler(0,0,0);
            }
        }
        // Right leg
        transform.Find("BodyFrame").Find("RightLegFrame").Rotate(walkingInc * Vector3.forward);
        if (walkingInc > 0) {
            Transform shin = transform.Find("BodyFrame").Find("RightLegFrame").Find("ShinFrame");
            if (walkingReset < 10 && shin.localRotation.z > -0.25f) {
                shin.Rotate(-0.5f * walkingInc * Vector3.forward);
            }
            if (walkingReset > 10 && shin.localRotation.z < 0) {
                shin.Rotate(1 * walkingInc * Vector3.forward);
            }
            if (shin.localRotation.z > 0) {
                shin.localRotation = Quaternion.Euler(0,0,0);
            }
        }
        walkingReset += walkingInc;
    }
}
