using UnityEngine;
using TMPro;
using System;

public class PlayerMovement : MonoBehaviour {
    public GameObject Player;

    private int walkingInc = 2;
    private int walkingReset;

    private void Start() { }
    
    private void FixedUpdate() {
        if (Math.Abs(walkingReset) >= 40) {
            walkingInc = -walkingInc;
        }

        // Head
        transform.Find("BodyFrame").Find("HeadFrame").Rotate(-walkingInc * Vector3.forward / 10);

        // Arms
        float upperInc = 0.2f;
        float lowerInc = 0.4f;
        // Left Arm
        transform.Find("BodyFrame").Find("LeftArmFrame").Rotate(walkingInc * Vector3.forward * upperInc);
        if (walkingReset > 0) {
            transform.Find("BodyFrame").Find("LeftArmFrame").Find("ForearmFrame").Rotate(walkingInc * Vector3.forward * lowerInc);
        }
        if (walkingReset == 0 && walkingInc > 0) {
            transform.Find("BodyFrame").Find("LeftArmFrame").Find("ForearmFrame").Rotate(walkingInc * Vector3.forward * lowerInc);
        }
        // Right Arm
        transform.Find("BodyFrame").Find("RightArmFrame").Rotate(-walkingInc * Vector3.forward * upperInc);
        if (-walkingReset > 0) {
            transform.Find("BodyFrame").Find("RightArmFrame").Find("ForearmFrame").Rotate(-walkingInc * Vector3.forward * lowerInc);
        }
        if (walkingReset == 0 && -walkingInc > 0) {
            transform.Find("BodyFrame").Find("RightArmFrame").Find("ForearmFrame").Rotate(-walkingInc * Vector3.forward * lowerInc);
        }

        // Body
        transform.Find("BodyFrame").Find("Body").Rotate(-walkingInc * Vector3.up / 10);

        // Legs
        // Left leg
        transform.Find("BodyFrame").Find("LeftLegFrame").Rotate(-walkingInc * Vector3.forward);
        if (-walkingInc > 0) {
            if (-walkingReset > 0 && -walkingReset < 20) {
                transform.Find("BodyFrame").Find("LeftLegFrame").Find("ShinFrame").Rotate(walkingInc * Vector3.forward);
            }
            if (-walkingReset > 20) {
                transform.Find("BodyFrame").Find("LeftLegFrame").Find("ShinFrame").Rotate(-walkingInc * Vector3.forward);
            }
        }
        // Right leg
        transform.Find("BodyFrame").Find("RightLegFrame").Rotate(walkingInc * Vector3.forward);
        if (walkingInc > 0) {
            if (walkingReset > 0 && walkingReset < 20) {
                transform.Find("BodyFrame").Find("RightLegFrame").Find("ShinFrame").Rotate(-walkingInc * Vector3.forward);
            }
            if (walkingReset > 20) {
                transform.Find("BodyFrame").Find("RightLegFrame").Find("ShinFrame").Rotate(walkingInc * Vector3.forward);
            }
        }

        walkingReset += walkingInc;
    }
}
