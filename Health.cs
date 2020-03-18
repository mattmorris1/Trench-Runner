using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    private int health;
    public Text healthCounter;
    public GameObject damageIndicator;
    bool damageIndicatorIsSet = false;
    float timer = 0.5F;
    
    // Start is called before the first frame update
    void Start()
    {
        //Sets the players health to 10, updates the health tracker
        health = 10;
        healthCounter.text = health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //Removes the damage indicator once its been visible for half a second
        if(damageIndicatorIsSet == true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                damageIndicator.SetActive(false);
                damageIndicatorIsSet = false;
            }
        }
        
    }

    //Checks if the player collided with a bullet
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //Lowers the players health and updates the health tracker
            health--;
            healthCounter.text = health.ToString();
            //Checks if the player dropped below zero health
            if(health <= 0)
            {
                SceneManager.LoadScene("Dead Screen");
            }
            //Activates the damage indicator and resets its counter
            damageIndicator.SetActive(true);
            damageIndicatorIsSet = true;
            timer = 0.5f;
        }
    }
}
