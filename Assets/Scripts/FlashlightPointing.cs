using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightPointing : MonoBehaviour
{
    public Vector3 worldPosition;
    Plane plane = new Plane(Vector3.up, 0); // plane that the flashlight points to

    float rotationSpeed = 10f;

     public Camera mainCamera;

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
