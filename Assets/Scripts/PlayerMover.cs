using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public float speed = 2.0f;
    public Vector3 worldPosition;
    public Camera mainCamera;
    public GameObject gameOverScreen;
    
    private Plane _plane = new Plane(Vector3.up, 0); // plane that the player points to
    private const float RotationSpeed = 5f;
    private bool _isDead;

    private void Start()
    {
        gameOverScreen.SetActive(false);
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0.0f, moveZ);

        transform.position += Time.deltaTime * movement * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            Debug.Log("I should be dead");
            gameOverScreen.SetActive(true);
        }
    }

    void Update()
    {
        float distance;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        
        if (_plane.Raycast(ray, out distance))
        {
            worldPosition = ray.GetPoint(distance);

            Vector3 direction = worldPosition - transform.position;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                targetRotation *= Quaternion.Euler(90f, 0f, 0f);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
            }
        }
    }
}
