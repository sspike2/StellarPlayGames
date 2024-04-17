using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGameState 
{
    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void Update() { }
    public virtual void SuspendState() { }
    public virtual void ResumeState() { }

}
