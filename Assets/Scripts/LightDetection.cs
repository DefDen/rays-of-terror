using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetection : MonoBehaviour { 
public Transform coneOfLight;
    public int verticalRays = 5; // Number of rays for the vertical angle
    public int horizontalRays = 5; // Number of rays for the horizontal angle
    public float maxRayDistance = 50f; // Maximum distance to visualize the rays
    public float rotationSpeed = 5f;
    public float verticalAngleRange = 98f; // Vertical angle range in degrees

    void Update()
    {
        bool hitObject = false;

        // Cast rays within the cone
        for (int i = 0; i < verticalRays; i++)
        {
            for (int j = 0; j < horizontalRays; j++)
            {
                // calculate vertical angle for rays
                float verticalAngle = (i / (float)(verticalRays - 1)) * verticalAngleRange - (verticalAngleRange / 2f);

                // calculate horizontal angle for rays
                float horizontalAngle = (j / (float)(horizontalRays - 1)) * 360f;

                // change angles to radians
                float verticalAngleRad = verticalAngle * Mathf.Deg2Rad;
                float horizontalAngleRad = horizontalAngle * Mathf.Deg2Rad;

                // calculate the direction of the ray based on spherical coordinates
                Vector3 rayDirection = new Vector3(
                    Mathf.Sin(verticalAngleRad) * Mathf.Cos(horizontalAngleRad),
                    Mathf.Cos(verticalAngleRad),
                    Mathf.Sin(verticalAngleRad) * Mathf.Sin(horizontalAngleRad)
                );
                rayDirection = coneOfLight.TransformDirection(rayDirection);

                // cast the ray
                RaycastHit hit;
                if (Physics.Raycast(coneOfLight.position, rayDirection, out hit, maxRayDistance))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        hitObject = true;
                        // Break the loop to prevent multiple rotations from multiple rays
                        break;
                    }
                }

                // Visualize the ray in the game view
                Debug.DrawRay(coneOfLight.position, rayDirection * maxRayDistance, Color.green);
            }
            if (hitObject)
                break;
        }
        if (hitObject)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }
}