using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEnemiesDead : Condition
{
    public override bool checkCondition(List<GameObject> enemies)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null) enemies.RemoveAt(i);
        }
        if (enemies.Count <= 0) return true;
        else return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        type = ConditionType.SPECIFIC_ENEMIES_DEAD;
    }

}
