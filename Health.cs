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
        health = 10;
        healthCounter.text = health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
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
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            health--;
            healthCounter.text = health.ToString();
            if(health <= 0)
            {
                SceneManager.LoadScene("Dead Screen");
            }
            damageIndicator.SetActive(true);
            damageIndicatorIsSet = true;
            timer = 0.5f;
        }
    }
}
