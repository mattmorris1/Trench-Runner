using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerAttackState : GunnerState
{
    float targetTime = 0.5f;
    GameObject enemy;
    // Start is called before the first frame update
    public GunnerAttackState(GunnerStateController gunnerStateController) : base(gunnerStateController) { }
    public override void CheckTransitions()
    {
        if (gunnerStateController.CheckSight() == null)
        {
            gunnerStateController.SetState(new GunnerIdleState(gunnerStateController));
        }
    }

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
    public override void OnStateEnter()
    {
        enemy = gunnerStateController.CheckSight();
    }
}

