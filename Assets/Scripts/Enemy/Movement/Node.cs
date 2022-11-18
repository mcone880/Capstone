using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] public Area parent;
    GameObject player;
    public float value = 0;
    // Start is called before the first frame update
    public void OnValidate()
    {
        player = GameObject.Find("FPSPlayer");
    }

    public bool CanSeePlayer(float eyeHeight)
    {
        var playerCenter = player.GetComponent<PlayerController>().centerMass;
        var eyePos = transform.position;
        eyePos.y += eyeHeight;

        var rayDir = playerCenter - eyePos;
        if (Physics.Raycast(eyePos, rayDir, out RaycastHit hit, parent.parentArea.transform.localScale.magnitude, 11))
        {
            if(hit.collider.gameObject == player)
            {
                return true;
            }
        }
        return false;
    }
}
