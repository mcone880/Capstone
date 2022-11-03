using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] List<GameObject> keyEnemies = new List<GameObject>();
    [SerializeField] List<Vector3> keyEnemySpawns = new List<Vector3>();
    private List<GameObject> spawnedKeyEnemies = new List<GameObject>();
    
    [SerializeField] List<GameObject> enemies = new List<GameObject>();
    [SerializeField] List<Vector3> enemySpawns = new List<Vector3>();
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    [SerializeField] Condition condition;

    [HideInInspector]public bool isStarted = false;
    [HideInInspector]public bool waveDone = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void StartSpawns()
    {
        if(keyEnemies.Count > 0)
        {
            for(int i = 0; i < keyEnemies.Count; i++)
            {
                spawnedKeyEnemies.Add(Instantiate(keyEnemies[i], keyEnemySpawns[i], Quaternion.identity));
            }
        }

        for(int i = 0; i < enemies.Count; i++)
        {
            spawnedEnemies.Add(Instantiate(enemies[i], enemySpawns[i], Quaternion.identity));
        }
    }

    public bool CheckCondition()
    {
        switch (condition.type)
        {
            case Condition.ConditionType.ALL_ENEMIES_DEAD:
                AllDeadCondition condit1 = (AllDeadCondition)condition;
                return condit1.checkCondition(spawnedEnemies);
            case Condition.ConditionType.SPECIFIC_ENEMIES_DEAD:
                KeyEnemiesDead condit2 = (KeyEnemiesDead)condition;
                return condit2.checkCondition(spawnedKeyEnemies);
            case Condition.ConditionType.SPECIFIC_ENEMY_HEALTH:
                KeyEnemiesHealth condit3 = (KeyEnemiesHealth)condition;
                return condit3.checkCondition(spawnedKeyEnemies);
            case Condition.ConditionType.TIME_PASSED:
                TimerCondition condit4 = (TimerCondition)condition;
                return condit4.checkCondition(condit4.timeToFinish);
            default:
                return false;
        }
    }
}
