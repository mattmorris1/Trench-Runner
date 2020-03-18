using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerIdleState : GunnerState
{

    public GunnerIdleState(GunnerStateController gunnerStateController) : base(gunnerStateController) { }
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
