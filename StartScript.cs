﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    //Placed on the scene loader spheres, if this object is shot load the specified scene
    public string sceneToLoad;
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
