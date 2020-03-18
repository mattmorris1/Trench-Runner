using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Ensures the game object with the music persists across scenes so the music isn't interupted
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
