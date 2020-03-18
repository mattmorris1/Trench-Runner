using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float timeLeft = 10.0f;
    public float bulletSpeed = 3000;
    // Start is called before the first frame update
    void Start()
    {
        //Sets the speed of the bullet when one is created
        this.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        //Keeps track of how long the bullet has existed
        timeLeft -= Time.deltaTime;
        //Deletes the bullet if it has been around for too long, in case the bullet never collides with anything
        if (timeLeft < 0)
        {
            Destroy(this.gameObject);

        }
        
    }

    //Deletes the bullet if it collides with any game object besides the gun
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Gun")
        {
            Destroy(this.gameObject);
        }
        
    }
}
