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

    public override void Act()
    {
        if (searchPoint == null || stateController.ai.DestinationReached())
        {
            searchPoint.transform.SetPositionAndRotation(new Vector3(
                stateController.transform.position.x + Random.Range(-10.0f, 10.0f), 
                stateController.transform.position.y, 
                stateController.transform.position.z + Random.Range(-10.0f, 10.0f)), 
                new Quaternion(0, 0, 0, 0));
            stateController.ai.SetTarget(searchPoint.transform);
            //Debug.Log(searchPoint);
            searchCount++;
            Debug.Log(searchCount);
        }
    }
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
        stateController.ai.agent.speed = 2f;
    }
    public override void OnStateExit()
    {
        //Destroy(searchPoint);
        UnityEngine.Object.Destroy(searchPoint);
    }
}
