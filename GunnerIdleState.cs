using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerIdleState : GunnerState
{
    //This state has the turrets wait idly until the player becomes visible, then moves them to their attack state
    public GunnerIdleState(GunnerStateController gunnerStateController) : base(gunnerStateController) { }
    //Checks to see if the player is within line of sight
    public override void CheckTransitions()
    {
        GameObject player = gunnerStateController.CheckSight();
        if (player != null)
        {
            gunnerStateController.SetState(new GunnerAttackState(gunnerStateController));
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
