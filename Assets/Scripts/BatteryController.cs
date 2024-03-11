using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BatteryController : MonoBehaviour
{
    void Update() 
    {
         transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject flashlight = GameObject.Find("PlayerLight");

            // Check if the object is found
            if (flashlight != null)
            {
                LightRaySender otherScript = flashlight.GetComponent<LightRaySender>();

                if (otherScript != null)
                {
                    float batteryValue = otherScript.currentBattery + 10;
                    if (batteryValue > 100) 
                    {
                        batteryValue = 100;
                    }
                    otherScript.currentBattery = batteryValue;
                }
            } 
            gameObject.SetActive(false);
        }
    }
}
