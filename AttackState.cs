using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    float targetTime = 1.0f;
    // Start is called before the first frame update
    public AttackState(StateController stateController) : base(stateController) { }
    public override void CheckTransitions()
    {
        if(stateController.CheckSight() == null)
        {
            stateController.SetState(new PatrolState(stateController));
        }
    }

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
    public override void OnStateEnter()
    {
        GameObject enemy = stateController.CheckSight();
        //stateController.ai.target = enemy.transform;
        stateController.ai.SetTarget(enemy.transform);
        stateController.ai.agent.speed = 0.5f;
    }
}
