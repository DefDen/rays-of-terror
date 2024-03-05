using UnityEngine;
using TMPro;
using System;

public class TransformFunctions : MonoBehaviour {
    public GameObject Flashlight;
    public GameObject button;

    private bool flashlightOn;
    private int flashlightInc = 5;
    private int flashlightReset = 10;

    private void Start() { }
    
    private void FixedUpdate() {
        if (flashlightOn) {
            if (Math.Abs(flashlightReset) > 10) {
                flashlightInc = -flashlightInc;
                flashlightOn = false;
            }
            transform.Find("SwitchBodyFrame").Find("SwitchFrame").Rotate(flashlightInc * Vector3.right);
            flashlightReset += flashlightInc;
        }
    }

    public void ToggleFlashlight() { flashlightOn = !flashlightOn; }
}
