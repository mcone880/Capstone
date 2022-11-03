using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDeadCondition : Condition
{
    List<GameObject> group = new List<GameObject>();

    void Start()
    {
        type = ConditionType.ALL_ENEMIES_DEAD;
    }

    public override bool checkCondition(List<GameObject> enemies)
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null) enemies.RemoveAt(i);
        }
        if (enemies.Count <= 0) return true;
        else return false;
    }
}
