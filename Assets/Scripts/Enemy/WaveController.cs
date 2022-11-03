using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField] List<Wave> waves = new List<Wave>();

    private int currentWave = -1;

    private void Update()
    {
        if (currentWave >= 0 && currentWave < waves.Count && waves[currentWave].CheckCondition()) StartWave();
    }

    public void StartWave()
    {
        currentWave++;
        if (currentWave >= waves.Count)
        {
            enabled = false;
            return;
        }
        waves[currentWave].isStarted = true;
        waves[currentWave].StartSpawns();
    }

    public void StartEncounter()
    {
        currentWave = -1;
        StartWave();
    }
}
