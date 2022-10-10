using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone : MonoBehaviour
{
    [SerializeField] UIController controller;
    [SerializeField] string sceneName;

    public void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerController>(out _)) controller.OnStartScene(sceneName);
    }
}
