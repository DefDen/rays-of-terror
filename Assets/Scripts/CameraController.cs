using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Reference to the player's transform
    public Vector3 offset = new Vector3(0f, 40f, -15f); // Offset from the player
    public float mouseSensitivity = 100f; // Sensitivity of the mouse movement

    // Update is called once per frame
    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            // float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

            transform.position = desiredPosition;
            // transform.Rotate(Vector3.up * mouseX);
        }
    }
}
