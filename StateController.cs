using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public State currentState;
    public GameObject target;
    public Transform destination;
    public UnityStandardAssets.Characters.ThirdPerson.AICharacterControl ai;
    //public UnityStandardAssets.Utility.
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
        audioSource = GetComponent<AudioSource>();
        path = GameObject.FindGameObjectsWithTag("Path");
        ai = GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();
        SetState(new PatrolState(this));
    }

    void FixedUpdate()
    {
        currentState.CheckTransitions();
        currentState.Act();
        //Debug.Log("Test");
    }
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
    bool CheckLineOfSight(GameObject player){
        //code here to check line of sight to player object
        target = player;
        Vector3 targetDir = target.transform.position - transform.position;
        float angle = Vector3.Angle(targetDir, transform.forward);
        RaycastHit hitInfo;
        Physics.Raycast(transform.position, (target.transform.position - transform.position), out hitInfo, 1000);
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

                float dis = Vector3.Distance(transform.position, player.transform.position);
                nearestDetectedDistance = 500.0f;
                nearestDetectedPlayer = null;
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
    //This function is used to check if a player is within hearing range.
    public GameObject CheckHearing()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        //this checks to see whether or not they can detect any players
        if (players != null)
        {
            foreach (GameObject player in players)
            {

                float dis = Vector3.Distance(transform.position, player.transform.position);
                nearestDetectedDistance = 100.0f;
                nearestDetectedPlayer = null;

                if (dis < 10/*player.DR.DRSound*/)
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
    //This function will be used to fire once implemented
    public void fire()
    {
        audioSource.PlayOneShot(gunShot, 0.6F);
        //gunArm.transform.LookAt(ai.target);
        pos = bulletSpawn.transform.position;
        rot = Quaternion.LookRotation(ai.target.position - bulletSpawn.transform.position);
        rot.x = rot.x + UnityEngine.Random.Range(-0.01f, 0.01f);
        rot.y = rot.y + UnityEngine.Random.Range(-0.01f, 0.01f);
        rot.z = rot.z + UnityEngine.Random.Range(-0.01f, 0.01f);
        GameObject newBullet = Instantiate(bullet, pos, rot);
    }
    public void GetNextNavPoint()
    {
        pathCount = (pathCount + 1);
        if(pathCount >= path.Length)
        {
            pathCount = 0;
        }
        ai.SetTarget(path[pathCount].transform);
        //return path[pathCount].transform;
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Destroy(this.gameObject);
        }
    }
}
