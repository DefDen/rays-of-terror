using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightPointing : MonoBehaviour
{
    public Vector3 worldPosition;
    Plane plane = new Plane(Vector3.up, 0);

    float rotationSpeed = 5f;

     public Camera mainCamera;

    void Update()
    {
        float distance;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            worldPosition = ray.GetPoint(distance);

            // Calculate the direction from flashlight to mouse position
            Vector3 direction = worldPosition - transform.position;

            // Make sure the direction is not exactly zero to avoid errors
            if (direction != Vector3.zero)
            {
                // Calculate the target rotation based on the direction
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // Apply the 90-degree offset on the X-axis
                targetRotation *= Quaternion.Euler(90f, 0f, 0f);

                // Smoothly rotate the flashlight towards the mouse position
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

        }


    }
}
