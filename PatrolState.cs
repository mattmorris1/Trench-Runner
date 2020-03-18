using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    Transform destination;
    // Start is called before the first frame update
    public PatrolState(StateController stateController) : base(stateController) { }
    public override void CheckTransitions()
    {
        GameObject player = stateController.CheckSight();
        if (player  != null){
            stateController.SetState(new AttackState(stateController));
        }
        /*else
        {
            player = stateController.CheckHearing();
            if(player != null)
            {
                stateController.SetState(new InvestigateState(stateController));
            }
        }*/
    }

    public override void Act()
    {
        
        /*destination = stateController.ai.target;
        if (destination == null || stateController.ai.DestinationReached())
        {
            stateController.GetNextNavPoint();
            //Debug.Log("Test");
            
        }*/
    }
    public override void OnStateEnter()
    {
        stateController.ai.SetTarget(null);
        //stateController.GetNextNavPoint();
        //stateController.ai.agent.speed = 2.5f;
    }

}
