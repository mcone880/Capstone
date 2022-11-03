using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEnemiesHealth : Condition
{
    [SerializeField] List<float> healthPercentages = new List<float>();

    public override bool checkCondition(List<GameObject> enemies)
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            Health enemyH = enemies[i].GetComponent<Health>();
            if (enemyH.health > enemyH.maxhealth * healthPercentages[i])
            {
                return false;
            }
        }
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        type = ConditionType.SPECIFIC_ENEMY_HEALTH;
    }
}
