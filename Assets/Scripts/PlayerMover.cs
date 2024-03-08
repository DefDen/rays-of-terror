using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public float speed = 2.0f;
    // private Rigidbody rb;

    // Start is called before the first frame update
    // void Start()
    // {
    //     rb = GetComponent<Rigidbody>();
    // }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveZ, 0.0f, moveX);

        // Apply the movement to the sphere's position
        transform.position += Time.deltaTime * movement * speed;
    }
}
