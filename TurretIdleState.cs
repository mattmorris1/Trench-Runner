using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretIdleState : TurretState
{
    //This state has the turrets wait idly until the player becomes visible, then moves them to their attack state
    public TurretIdleState(TurretStateController turretStateController) : base(turretStateController) { }
    //Checks to see if the player is within line of sight
    public override void CheckTransitions()
    {
        GameObject player = turretStateController.CheckSight();
        if (player != null)
        {
            turretStateController.SetState(new TurretAttackState(turretStateController));
        }
    }

    public override void Act()
    {
    }
    public override void OnStateEnter()
    {

    }
    public override void OnStateExit()
    {

    }
}
