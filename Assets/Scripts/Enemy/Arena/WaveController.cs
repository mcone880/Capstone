using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField] List<Wave> waves = new List<Wave>();
    private List<GameObject> Barriers = new List<GameObject>();

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
            foreach (GameObject barrier in Barriers)
            {
                barrier.GetComponent<MeshRenderer>().enabled = false;
                barrier.GetComponent<BoxCollider>().enabled = false;
            }
            enabled = false;
            return;
        }
        waves[currentWave].isStarted = true;
        waves[currentWave].StartSpawns();
    }

    public void StartEncounter(List<GameObject> barriers)
    {
        Barriers = barriers;
        currentWave = -1;
        StartWave();
    }
}
