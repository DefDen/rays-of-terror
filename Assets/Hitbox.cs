using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hitbox : MonoBehaviour
{
    public GameObject gameOverScreen;

    public TextMeshProUGUI endText;

     private void Start()
    {
        gameOverScreen.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {

            Debug.Log("Monster!");
            
            EnemyController otherScript = other.GetComponent<EnemyController>();

            if (otherScript != null && otherScript.canHurtPlayer)
            {
                endText.text = "You've been infected";
                gameOverScreen.SetActive(true);
            }
        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            endText.text = "You escaped!";
            gameOverScreen.SetActive(true);
        }
    }
}
