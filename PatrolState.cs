using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    Transform destination;

    public PatrolState(StateController stateController) : base(stateController) { }

    //Checks for the player
    public override void CheckTransitions()
    {
        GameObject player = stateController.CheckSight();
        if (player  != null){
            stateController.SetState(new AttackState(stateController));
        }
    }

    public override void Act()
    {
        
    }
    public override void OnStateEnter()
    {
        stateController.ai.SetTarget(null);
    }

}
