using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] public float hitRange;
    [SerializeField] public Animator animator;
    [SerializeField] float hitDamage;
    [SerializeField] BoxCollider attackHitbox;

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Area>(out Area area))
        {
            if (currentArea != area) currentArea = area;
        }
    }
    public override void Start()
    {
        base.Start();
    }

    public void Update()
    {
        float playerDist = Vector3.Distance(player.transform.position, transform.position);
        Debug.DrawRay(eyes.position, transform.forward, Color.red);

        if (!CheckForPlayer() || playerDist > hitRange)
        {
            Reposition();
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position, Vector3.up);
            MeleeAttack();
        }
        animator.SetFloat("Speed", navMesh.velocity.magnitude);
    }

    private bool CheckForPlayer()
    {
        var playerCenter = player.GetComponent<PlayerController>().centerMass;
        var eyePos = eyes.transform.position;

        var rayDir = playerCenter - eyePos;
        if (Physics.Raycast(eyePos, rayDir, out RaycastHit hit, sightDist, 11))
        {
            if (hit.collider.gameObject == player) return true;
        }
        return false;
    }
    private Node FindBestNode(Area area)
    {
        List<Node> possibleNodes = new List<Node>();
        float currentNodeVal = 0;
        Node target = null;
        foreach (var node in area.nodes)
        {
            if (node.CanSeePlayer(eyes.position.y)) possibleNodes.Add(node);
        }

        if (possibleNodes.Count == 0) return null;

        for (int i = 0; i < possibleNodes.Count; i++)
        {
            AssignNodeValue(possibleNodes[i]);
            if (possibleNodes[i].value > currentNodeVal)
            {
                currentNodeVal = possibleNodes[i].value;
                target = possibleNodes[i];
            }
        }
        return target;
    }
    private void AssignNodeValue(Node node)
    {
        node.value = 0;
        float distFromPlayer = Vector3.Distance(node.transform.position, player.transform.position);
        float distFromMe = Vector3.Distance(node.transform.position, transform.position);

        node.value -= distFromMe * 0.25f;
        if (node.transform.position.y > player.transform.position.y) node.value++;
    }
    private void AssignAreaValue(Area area)
    {
        area.value = 0;

        float distFromPlayer = Vector3.Distance(area.transform.position, player.transform.position);
        float distFromMe = Vector3.Distance(area.transform.position, transform.position);

        area.value -= distFromMe * 0.25f;
    }
    private Area FindBestArea()
    {
        Area bestArea = null;
        Area parent = currentArea.parentArea;
        List<Area> possibleAreas = new List<Area>();
        float currentAreaVal = 0;

        foreach (var area in parent.childrenAreas)
        {
            if (FindBestNode(area) != null) possibleAreas.Add(area);
        }

        for (int i = 0; i < possibleAreas.Count; i++)
        {
            AssignAreaValue(possibleAreas[i]);
            if (possibleAreas[i].value > currentAreaVal)
            {
                currentAreaVal = possibleAreas[i].value;
                bestArea = possibleAreas[i];
            }
        }
        return bestArea;
    }
    private void Reposition()
    {
        Node bestNode = null;
        if (FindBestNode(currentArea) != null)
        {
            bestNode = FindBestNode(currentArea);
        }
        else
        {
            bestNode = FindBestNode(FindBestArea());
        }

        if (bestNode && canMove) navMesh.SetDestination(bestNode.transform.position);
    }

    public override void MeleeAttack()
    {
        print("Melee Attack!");
    }

    public override void RangedAttack()
    {
    }
}
