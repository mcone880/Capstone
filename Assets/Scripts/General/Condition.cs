using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition : MonoBehaviour
{
    protected bool isMet = false;
    [HideInInspector] public ConditionType type;

    public enum ConditionType
    {
        ALL_ENEMIES_DEAD,
        SPECIFIC_ENEMIES_DEAD,
        SPECIFIC_ENEMY_HEALTH,
        TIME_PASSED
    }

    public virtual bool checkCondition(List<GameObject> enemies) { return false; }
    public virtual bool checkCondition(float time) { return false; }
}
