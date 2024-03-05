using UnityEngine;
using TMPro;
using System;

public class ZombieMovement : MonoBehaviour {
    public GameObject Zombie;
    public GameObject button;

    private bool walking;
    private int walkingInc = 2;
    private int walkingReset;

    private void Start() { }
    
    private void FixedUpdate() {
        if (walking) {
            if (Math.Abs(walkingReset) > 40) {
                walkingInc = -walkingInc;
            }
            transform.Find("BodyFrame").Find("Body").Rotate(-walkingInc * Vector3.up / 10);
            // Limping leg
            transform.Find("BodyFrame").Find("LimpingLegFrame").Rotate(-walkingInc * Vector3.forward);
            // Working leg
            transform.Find("BodyFrame").Find("WalkingLegFrame").Rotate(walkingInc * Vector3.forward);
            if (walkingReset > 0) {
                transform.Find("BodyFrame").Find("WalkingLegFrame").Find("WalkingShinFrame").Rotate(-walkingInc * Vector3.forward);
            }
            // Arms
            transform.Find("BodyFrame").Find("RightArmFrame").Rotate(-walkingInc * Vector3.forward / 10);
            transform.Find("BodyFrame").Find("LeftArmFrame").Rotate(walkingInc * Vector3.forward / 10);
            // Head
            transform.Find("BodyFrame").Find("HeadFrame").Rotate(-walkingInc * Vector3.forward / 10);
            // Reset
            if (walkingReset == 0 && walkingInc > 0) {
                transform.Find("BodyFrame").Find("WalkingLegFrame").Find("WalkingShinFrame").Rotate(-walkingInc * Vector3.forward);
            }
            walkingReset += walkingInc;
        }
    }

    public void ToggleWalk() { walking = !walking; }
}
