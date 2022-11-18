using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionState : BaseEnemyState
{
    public override void EnterState()
    {
        player = GameObject.Find("FPSPlayer");
    }

    public Vector3 FindBestPosition(bool isRanged, int agressivness)
    {
        return Vector3.zero;
    }

    public override void UpdateState()
    {

    }
}
