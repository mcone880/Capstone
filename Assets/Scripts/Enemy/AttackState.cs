using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseEnemyState
{
    List<Attack> Attacks = new List<Attack>();

    public override void EnterState()
    {
         player = GameObject.Find("FPSPlayer");
    }

    public Attack ChooseBestAttack()
    {
        return null;
    }

    public void PreformAttack(Attack attack)
    {
        attack.DoAttack();
    }

    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }
}
