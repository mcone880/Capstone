using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDescriptor : MonoBehaviour
{
    public AudioClip music;
    [SerializeField] Transform playerSpawn;

    private void Start()
    {
        GameObject player = GameObject.Find("FPSPlayer");
        player.transform.position = playerSpawn.transform.position;
        player.transform.rotation = playerSpawn.transform.rotation;
    }
}
