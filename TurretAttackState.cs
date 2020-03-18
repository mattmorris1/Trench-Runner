using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttackState : TurretState
{
    float targetTime = 1.0f;
    GameObject enemy;
    
    public TurretAttackState(TurretStateController turretStateController) : base(turretStateController) { }

    //Checks if the player left the line of sight
    public override void CheckTransitions()
    {
        if (turretStateController.CheckSight() == null)
        {
            turretStateController.SetState(new TurretIdleState(turretStateController));
        }
    }

    //Has the turret face the player, wait for 1 second and then fire at the player
    public override void Act()
    {
        turretStateController.gunArm.transform.LookAt(enemy.transform);
        targetTime -= Time.deltaTime;
        if (targetTime <= 0.0f)
        {
            turretStateController.fire(enemy);
            targetTime = 1.0f;
        }
    }

    //Looks for the player
    public override void OnStateEnter()
    {
        enemy = turretStateController.CheckSight();
    }
}
