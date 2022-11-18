using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDestroyer : MonoBehaviour
{
    [SerializeField] List<GameObject> Obstacles = new List<GameObject>();
    public void Activate()
    {
        foreach (GameObject obstacle in Obstacles) Destroy(obstacle);
    }
}
