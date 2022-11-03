using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCondition : Condition
{
    [SerializeField] public float timeToFinish;
    private float timeElapsed;

    public override bool checkCondition(float time)
    {
        timeElapsed += time;
        return timeElapsed >= timeToFinish;
    }

    // Start is called before the first frame update
    void Start()
    {
        type = ConditionType.TIME_PASSED;
    }
}
