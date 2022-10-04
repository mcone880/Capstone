using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float timer;
    private float spawnTimer;
    private void Start()
    {
        spawnTimer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            Debug.Log("Spawn");
            Instantiate(projectilePrefab, transform.position, transform.rotation);
            spawnTimer = timer;
        }

    }
}
