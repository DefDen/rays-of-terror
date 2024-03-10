using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StaticLightRaySender : MonoBehaviour { 

    // variables for light detection
    public int rays = 10; // Number of rays for the vertical angle
    public int slices = 10; // Number of rays for the horizontal angle
    private float coneAngle; // angle of the cone
    public float coneAngleOffset = 0; // offset because spot light is wider than angle says
    private float maxRayDistance; // Maximum distance to visualize the rays
    public string targetTag = "Monster";


    // battery for static light
    float maxBattery = 100f;
    public float batteryDrainRate = 10f; // per second

    private bool lightOn = true;
    private float currentBattery;

    Light myLight;


    void Start()
    {
        currentBattery = maxBattery;
        myLight = GetComponent<Light>();
        myLight.enabled = true;

        coneAngle = myLight.spotAngle + coneAngleOffset;
        maxRayDistance = myLight.range;
    }
    

    void Update()
    {
        // check if battery is dead
        if (currentBattery <= 0) {
            lightOn = false;
            myLight.enabled = false;
            currentBattery = 0;
        // if battery not dead, check if player is toggling flashlight
        } 

        // if flashlight is on, send out the light detection rays
        if (lightOn) {
            sendDetectorRays();

            // drain battery once rays are cast
            currentBattery -= batteryDrainRate * Time.deltaTime;
        }

        currentBattery = Mathf.Clamp(currentBattery, 0, maxBattery);
    }

    void sendDetectorRays() {
        for (int i = 0; i < rays; i++)
        {
            for (int j = 0; j < slices; j++)
            {
                // calculate angle for each ray in the slice
                float angleForRays = (float)(i+1) / (rays) * coneAngle / 2f;

                // calculate angle for each slice
                float angleForSlice = (float)(j+1) / (slices) * 360f;

                // change angles to radians
                float angleForRaysRad = angleForRays * Mathf.Deg2Rad;
                float angleForSliceRad = angleForSlice * Mathf.Deg2Rad;

                // calculate the direction of the ray (where it's pointing in forward z direction)
                Vector3 rayDirection = new Vector3(
                    Mathf.Sin(angleForRaysRad) * Mathf.Cos(angleForSliceRad),
                    Mathf.Sin(angleForRaysRad) * Mathf.Sin(angleForSliceRad),
                    Mathf.Cos(angleForRaysRad)
                );
                // change direction to be pointing in the light's direction
                rayDirection = transform.TransformDirection(rayDirection);

                // cast the ray
                RaycastHit hit;
                if (Physics.Raycast(transform.position, rayDirection, out hit, maxRayDistance))
                {
                    if (hit.collider.CompareTag(targetTag))
                    {
                        // Check if the object has a script with a method named isHit and call it
                        EnemyController targetScript = hit.collider.GetComponentInParent<EnemyController>();
                        if (targetScript != null)
                        {
                            targetScript.GotHit();
                        }
                    }
                }

                // Visualize the ray in the game view
                Debug.DrawRay(transform.position, rayDirection * maxRayDistance, Color.green);
            }
        }
    }
}