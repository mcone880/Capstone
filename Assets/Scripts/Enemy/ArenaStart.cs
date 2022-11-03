using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaStart : MonoBehaviour
{
    [SerializeField] List<GameObject> Barriers;
    [SerializeField] WaveController WaveController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerController>(out PlayerController player))
        {
            foreach(GameObject barrier in Barriers)
            {
                barrier.GetComponent<MeshRenderer>().enabled = true;
                barrier.GetComponent<BoxCollider>().enabled = true;
            }
            GetComponent<Collider>().enabled = false;

            WaveController.StartEncounter();

            foreach (GameObject barrier in Barriers)
            {
                barrier.GetComponent<MeshRenderer>().enabled = false;
                barrier.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
