using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttackState : TurretState
{
    float targetTime = 1.0f;
    GameObject enemy;
    // Start is called before the first frame update
    public TurretAttackState(TurretStateController turretStateController) : base(turretStateController) { }
    public override void CheckTransitions()
    {
        if (turretStateController.CheckSight() == null)
        {
            turretStateController.SetState(new TurretIdleState(turretStateController));
        }
    }

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
    public override void OnStateEnter()
    {
        enemy = turretStateController.CheckSight();
    }
}
