using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemyState
{
    protected GameObject player;
    public abstract void EnterState();
    public abstract void UpdateState();
}
