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
        //rigidBody.AddForce(transform.forward * movementSpeed);
        this.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            Destroy(this.gameObject);

        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Gun")
        {
            Destroy(this.gameObject);
        }
        
    }
}
