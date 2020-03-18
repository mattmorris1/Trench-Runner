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
        //Gets reference to the audio component
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Keeps the spawn position the same as the bullet spawn game object
        pos = bulletSpawn.transform.position;
        rotation = this.transform.rotation;

        //Checks if the player is pulling the trigger with the right or left hand
        bool bDownLeft = OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch); // for left hand

        bool bDownRight = OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch); // for right hand
        //If the left hand trigger is pulled
        if (bDownLeft)
        {
            try
            {
                //If this game object is parented to the left hand, then fire this gun
                if(transform.parent.name == "AvatarGrabberLeft" || transform.parent.name == "CustomHandLeft")
                {
                    Shoot();
                }
            }catch(Exception ex)
            {
                Debug.Log(ex);
            }
            
        }
        //If the right hand trigger is pulled
        if (bDownRight)
        {
            try
            {
                //If this game object is parented to the right hand, then fire this gun
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
    //Plays gunshot noise and creates a new bullet object at the bullet position. The player gun has no variation to bullet trajectory
    void Shoot()
    {
        audioSource.PlayOneShot(gunShot, 0.5F);
        GameObject newBullet = Instantiate(bullet, pos, rotation);
    }
}
