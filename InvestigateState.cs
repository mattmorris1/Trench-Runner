using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigateState : State
{
    GameObject heardPlayer;
    GameObject searchPoint = new GameObject();
    int searchCount = 0;
    int maxCounts = 3;
    public InvestigateState(StateController stateController) : base(stateController) { }

    //checks for the player or for the maximum number of searches to be completed
    public override void CheckTransitions()
    {
        GameObject player = stateController.CheckSight();
        if (player != null)
        {
            stateController.SetState(new AttackState(stateController));
        }
        if(searchCount >= maxCounts)
        {
            stateController.SetState(new PatrolState(stateController));
        }
    }

    //Sets a random investigation point near to the last known location of the player and then moves in that direction
    public override void Act()
    {
        if (searchPoint == null || stateController.ai.DestinationReached())
        {
            //Creating a new area nearby to search at
            searchPoint.transform.SetPositionAndRotation(new Vector3(
                stateController.transform.position.x + Random.Range(-10.0f, 10.0f), 
                stateController.transform.position.y, 
                stateController.transform.position.z + Random.Range(-10.0f, 10.0f)), 
                new Quaternion(0, 0, 0, 0));
            stateController.ai.SetTarget(searchPoint.transform);
            //Keeps track of the number of areas that have been searched thus far
            searchCount++;
            Debug.Log(searchCount);
        }
    }

    //When entering this state, listen for the player and decide on a point to search at
    public override void OnStateEnter()
    {
        heardPlayer = stateController.CheckHearing();
        if(heardPlayer != null)
        {
            searchPoint.transform.SetPositionAndRotation(new Vector3(
                heardPlayer.transform.position.x + Random.Range(-10.0f, 10.0f), 
                heardPlayer.transform.position.y, 
                heardPlayer.transform.position.z + Random.Range(-10.0f, 10.0f)), 
                new Quaternion(0,0,0,0));
            stateController.ai.SetTarget(searchPoint.transform);
            Debug.Log(searchPoint.transform);
        }
        //Speeds the AI up
        stateController.ai.agent.speed = 2f;
    }
    public override void OnStateExit()
    {
        //Destroys the current nav target
        UnityEngine.Object.Destroy(searchPoint);
    }
}
