using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public float speed = 2.0f;
    public Vector3 worldPosition;
    Plane plane = new Plane(Vector3.up, 0); // plane that the player points to

    float rotationSpeed = 5f;

     public Camera mainCamera;
     
    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0.0f, moveZ);

        // Apply the movement to the sphere's position
        transform.position += Time.deltaTime * movement * speed;
    }

    void Update()
    {
          float distance;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        // checks if ray hit the plane
        // could be changed to any game object for more flexible but weirder controls
        if (plane.Raycast(ray, out distance))
        {
            worldPosition = ray.GetPoint(distance);

            Vector3 direction = worldPosition - transform.position;

            // make sure the direction is not exactly zero to avoid errors
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // apply 90-degree offset on the X-axis due to model orientation
                targetRotation *= Quaternion.Euler(90f, 0f, 0f);

                // for smooth rotation towards mouse
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

        }
    }
}