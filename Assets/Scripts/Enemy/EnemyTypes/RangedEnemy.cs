using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject projectile;
    [SerializeField] private int projectilesPerAttack;
    [SerializeField] private float timeBetweenShotInBurst;
    [SerializeField] private GameObject muzzle;
    [SerializeField] private float fireTimer;
    [SerializeField] public float minPlayerDist;
    [SerializeField] public float maxPlayerDist;

    [HideInInspector]public float fireTime;
    public override void RangedAttack()
    {
        if(fireTime <= 0)
        {
            StartCoroutine(BurstFire(projectilesPerAttack));
            fireTime = fireTimer;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Area>(out Area area))
        {
            if (currentArea != area) currentArea = area;
        }
    }
    public override void MeleeAttack()
    {
    }

    public override void Start()
    {
        base.Start();
        fireTime = fireTimer;
    }
    public void Update()
    {
        float playerDist = Vector3.Distance(player.transform.position, transform.position);
        fireTime -= Time.deltaTime;
        Debug.DrawRay(eyes.position, transform.forward, Color.red);

        if(!CheckForPlayer() || playerDist < minPlayerDist || playerDist > maxPlayerDist)
        {
            Reposition();
        } 
        else
        {
            transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position, Vector3.up);
            RangedAttack();
        }
        animator.SetFloat("Speed", navMesh.velocity.magnitude);
    }

    IEnumerator BurstFire(int projNum)
    {
        for(int i = 0; i < projNum; i++)
        {
            Instantiate(projectile, muzzle.transform.position, muzzle.transform.rotation);
            yield return new WaitForSeconds(timeBetweenShotInBurst);
        }
    }

    private bool CheckForPlayer()
    {
        var playerCenter = player.GetComponent<PlayerController>().centerMass;
        var eyePos = muzzle.transform.position;

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
        foreach(var node in area.nodes)
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

        if (distFromPlayer > minPlayerDist && distFromPlayer < maxPlayerDist) node.value += 10;
        node.value -= distFromMe * 0.25f;
        if (node.transform.position.y > player.transform.position.y) node.value++;
    }
    private void AssignAreaValue(Area area)
    {
        area.value = 0;

        float distFromPlayer = Vector3.Distance(area.transform.position, player.transform.position);
        float distFromMe = Vector3.Distance(area.transform.position, transform.position);

        if (distFromPlayer > minPlayerDist && distFromPlayer < maxPlayerDist) area.value += 10;
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
        if(FindBestNode(currentArea) != null)
        {
            bestNode = FindBestNode(currentArea);
        } 
        else
        {
            bestNode = FindBestNode(FindBestArea());
        }

        if(bestNode && canMove) navMesh.SetDestination(bestNode.transform.position);
    }
}
