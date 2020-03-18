using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public State currentState;
    public GameObject target;
    public Transform destination;
    public UnityStandardAssets.Characters.ThirdPerson.AICharacterControl ai;
    public GameObject[] path;
    public float detectionRange = 50;
    public GameObject[] players;
    private GameObject nearestDetectedPlayer;
    private float nearestDetectedDistance = 100.0f;
    private int pathCount = 0;
    public GameObject bullet;
    public GameObject bulletSpawn;
    Vector3 pos;
    Quaternion rot;
    public GameObject gunArm;
    public AudioClip gunShot;
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        //Gets reference to audio source
        audioSource = GetComponent<AudioSource>();
        //Finds the patrol path
        path = GameObject.FindGameObjectsWithTag("Path");
        //Gets references to the AI character controller
        ai = GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();
        //Set the starting state to patrol
        SetState(new PatrolState(this));
    }

    void FixedUpdate()
    {
        //Check for transitions and act on the current state
        currentState.CheckTransitions();
        currentState.Act();
    }
    
    //Used to switch from the last state to the new state, updates the game object to show the current state
    public void SetState(State state)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }

        currentState = state;
        gameObject.name = "AI agent in state " + state.GetType().Name;

        if (currentState != null)
        {
            currentState.OnStateEnter();
        }
    }

    //This code is used to check if the enemy AI can see the player object.
    bool CheckLineOfSight(GameObject player){
        target = player;
        Vector3 targetDir = target.transform.position - transform.position;
        float angle = Vector3.Angle(targetDir, transform.forward);
        //Raycast to the player, check what is returned by the recast
        RaycastHit hitInfo;
        Physics.Raycast(transform.position, (target.transform.position - transform.position), out hitInfo, 1000);
        //Check if the player is in front of the enemy in its cone of vision and check the raycast to see if anything is in the way of the player
        if (angle < 90.0f && (hitInfo.collider.tag == player.tag || hitInfo.collider.tag == "Shield" || hitInfo.collider.tag == "Gun"))
        {
            return true;
        }
        else
        {
            return false;
        }
            
        
    }

    //This function is used to check if a player is within sight range and the field of vision.
    public GameObject CheckSight()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        //this checks to see whether or not they can detect any players
        if (players != null)
        {
            foreach (GameObject player in players)
            {
                //If there's multiple players this finds the nearest one and uses that for its detection
                float dis = Vector3.Distance(transform.position, player.transform.position);
                nearestDetectedDistance = 500.0f;
                nearestDetectedPlayer = null;
                //Calling the previous function to check if there's line of sight on the player, then see if its within detection range
                bool canSee = CheckLineOfSight(player);
                if (canSee == true && dis < 500)
                {
                    if (dis < nearestDetectedDistance)
                    {
                        nearestDetectedPlayer = player;
                        nearestDetectedDistance = dis;
                    }
                }
            }
        }
        return nearestDetectedPlayer;
    }
    //This function is used to check if a player is within hearing range. Similar to the check sight function, but does not check for direct field of vision.
    //This function is used to put the AI into the investigate state and not the attack state
    public GameObject CheckHearing()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        //this checks to see whether or not they can detect any players
        if (players != null)
        {
            foreach (GameObject player in players)
            {
                //Finds the nearest detected player if there are multiple players
                float dis = Vector3.Distance(transform.position, player.transform.position);
                nearestDetectedDistance = 100.0f;
                nearestDetectedPlayer = null;

                //checks if the player is within a short range 
                if (dis < 10)
                {
                    if (dis < nearestDetectedDistance)
                    {
                        nearestDetectedPlayer = player;
                        nearestDetectedDistance = dis;
                        Debug.Log(nearestDetectedPlayer);
                    }
                }
            }
        }
        return nearestDetectedPlayer;
    }

    //This function is used to fire.
    public void fire()
    {
        //Plays gunshot noise
        audioSource.PlayOneShot(gunShot, 0.6F);
        //Creates a new bullet at the bullet spawn, gives it a slight variation to the bullet to make it less accurate, then instantiates the new bullet
        pos = bulletSpawn.transform.position;
        rot = Quaternion.LookRotation(ai.target.position - bulletSpawn.transform.position);
        rot.x = rot.x + UnityEngine.Random.Range(-0.01f, 0.01f);
        rot.y = rot.y + UnityEngine.Random.Range(-0.01f, 0.01f);
        rot.z = rot.z + UnityEngine.Random.Range(-0.01f, 0.01f);
        GameObject newBullet = Instantiate(bullet, pos, rot);
    }

    //Looks for the next nav point along the navigation path
    public void GetNextNavPoint()
    {
        pathCount = (pathCount + 1);
        if(pathCount >= path.Length)
        {
            pathCount = 0;
        }
        ai.SetTarget(path[pathCount].transform);
        
    }

    //Checks to see if the object collided with a bullet game object, and destroys this AI object if it did
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Destroy(this.gameObject);
        }
    }
}
