using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretIdleState : TurretState
{

    public TurretIdleState(TurretStateController turretStateController) : base(turretStateController) { }
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
