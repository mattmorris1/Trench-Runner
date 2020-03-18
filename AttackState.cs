using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    float targetTime = 1.0f;
    
    public AttackState(StateController stateController) : base(stateController) { }

    //Checks if the player left the line of sight
    public override void CheckTransitions()
    {
        if(stateController.CheckSight() == null)
        {
            stateController.SetState(new PatrolState(stateController));
        }
    }

    //Has the AI point its gun at the player, wait one second and then fire at the player
    public override void Act()
    {
        stateController.gunArm.transform.LookAt(stateController.ai.target);
        targetTime -= Time.deltaTime;
        if (targetTime <= 0.0f)
        {
            stateController.fire();
            targetTime = 1.0f;
        }
    }

    //Looks at the player and sets the player as the nav target and slows the AI to a walk
    public override void OnStateEnter()
    {
        GameObject enemy = stateController.CheckSight();
        stateController.ai.SetTarget(enemy.transform);
        stateController.ai.agent.speed = 0.5f;
    }
}
