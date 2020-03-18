using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunnerState
{
    protected GunnerStateController gunnerStateController;
    //constructor
    public GunnerState(GunnerStateController gunnerStateController)
    {
        this.gunnerStateController = gunnerStateController;
    }
    public abstract void CheckTransitions();

    public abstract void Act();

    public virtual void OnStateEnter() { }

    public virtual void OnStateExit() { }
}
