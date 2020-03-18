using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerAttackState : GunnerState
{
    float targetTime = 0.5f;
    GameObject enemy;
    
    public GunnerAttackState(GunnerStateController gunnerStateController) : base(gunnerStateController) { }
    //Checks if the player left the line of sight
    public override void CheckTransitions()
    {
        if (gunnerStateController.CheckSight() == null)
        {
            gunnerStateController.SetState(new GunnerIdleState(gunnerStateController));
        }
    }

    //Has the turret face the player, wait for 1/2 a second and then fire at the player
    public override void Act()
    {
        gunnerStateController.gunArm.transform.LookAt(enemy.transform);
        targetTime -= Time.deltaTime;
        if (targetTime <= 0.0f)
        {
            gunnerStateController.fire(enemy);
            targetTime = 0.5f;
        }
    }

    //Looks for the player
    public override void OnStateEnter()
    {
        enemy = gunnerStateController.CheckSight();
    }
}

