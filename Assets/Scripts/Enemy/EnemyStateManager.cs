using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    BaseEnemyState currentState;
    RepositionState RepositionState = new RepositionState();
    AttackState AttackState = new AttackState();

    // Start is called before the first frame update
    void Start()
    {
        currentState = RepositionState;
        currentState.EnterState();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();
    }
}
