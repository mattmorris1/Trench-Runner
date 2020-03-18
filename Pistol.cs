using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bulletSpawn;
    Vector3 pos;
    Quaternion rotation;
    public AudioClip gunShot;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        pos = bulletSpawn.transform.position;
        rotation = this.transform.rotation;

        bool bDownLeft = OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch); // for left hand

        bool bDownRight = OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch); // for right hand
        if (bDownLeft)
        {
            try
            {
                if(transform.parent.name == "AvatarGrabberLeft" || transform.parent.name == "CustomHandLeft")
                {
                    Shoot();
                }
            }catch(Exception ex)
            {
                Debug.Log(ex);
            }
            
        }
        if (bDownRight)
        {
            try
            {
                if (transform.parent.name == "AvatarGrabberRight" || transform.parent.name == "CustomHandRight")
                {
                    Shoot();
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
            }
        }
    }
    //Plays gunshot noise and creates a new bullet object at the bullet position
    void Shoot()
    {
        audioSource.PlayOneShot(gunShot, 0.5F);
        GameObject newBullet = Instantiate(bullet, pos, rotation);
    }
}
