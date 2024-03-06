using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTarget : MonoBehaviour { 
    
    bool isHit = false;

    public float rotationSpeed = 100f;

    void Update()
    {
        if (isHit)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
        
        isHit = false;
    }

    public void GotHit()
    {
        isHit = true;
    }
}