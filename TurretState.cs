using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretState
{
    protected TurretStateController turretStateController;
    //constructor
    public TurretState(TurretStateController turretStateController)
    {
        this.turretStateController = turretStateController;
    }
    public abstract void CheckTransitions();

    public abstract void Act();

    public virtual void OnStateEnter() { }

    public virtual void OnStateExit() { }
}
