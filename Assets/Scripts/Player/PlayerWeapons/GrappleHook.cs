using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GrappleHook : MonoBehaviour
{
    [SerializeField] float pullSpeed;
    [SerializeField] GameObject hookPrefab;
    [SerializeField] float hookTravelSpeed;
    [SerializeField] Image greenCrosshair;
    [SerializeField] Image redCrosshair;

    private PlayerController player;
    private bool isAttached = false;
    private bool hookLaunched = false;
    private GameObject attachedObject;
    private GameObject hook;

    private float grappleCooldown = 3.0f;
    private float grappleTimer;

    private void Start()
    {
        player = GetComponentInParent<PlayerController>();
        grappleTimer = 0;
    }

    void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        Physics.SphereCast(ray, 1f, out hit, 50);
        if (hit.collider)
        {
            if (hit.collider.gameObject.TryGetComponent<GrappleObject>(out _) && grappleTimer <= 0)
            {
                greenCrosshair.enabled = false;
                redCrosshair.enabled = true;
            } else if(!hit.collider.gameObject.TryGetComponent<GrappleObject>(out _) || grappleTimer >= 0)
            {
                greenCrosshair.enabled = true;
                redCrosshair.enabled = false;
            }
        }

        if (grappleTimer > 0) grappleTimer -= Time.deltaTime;   
        
        if (Input.GetKeyDown(KeyCode.Mouse1) && grappleTimer <= 0)
        {
            Ray ray2 = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHit hit2;
            Physics.SphereCast(ray2, 1.25f, out hit2, 50);
            if (hit2.collider)
            {
                if (hit2.collider.gameObject.TryGetComponent<GrappleObject>(out GrappleObject grapple))
                {
                    attachedObject = grapple.gameObject;
                    hook = Instantiate(hookPrefab, player.transform.position, player.transform.rotation);
                    player.gravityOn = false;
                    player.direction.y = 0;
                    Yeet();
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1) && isAttached)
        {
            Dettach();
            attachedObject = null;
        }
        else if (hookLaunched && hook) Yeet();
        else if (isAttached) Attach(attachedObject);
    }

    private void Yeet()
    {
        hook.transform.LookAt(attachedObject.transform);
        if (Vector3.Distance(hook.transform.position, attachedObject.transform.position) >= 0.8f)
        {
            hook.transform.position += hookTravelSpeed * Time.deltaTime * hook.transform.forward;
            hookLaunched = true;
        }
        else if(Vector3.Distance(hook.transform.position, attachedObject.transform.position) < 0.8f)
        {
            hook.transform.position = attachedObject.transform.position;
            hookLaunched = false;
            isAttached = true;
        }
    }

    private void Attach(GameObject grapple)
    {
        if (grapple.TryGetComponent<Enemy>(out Enemy enemy)) enemy.canMove = false;
        
        Vector3 direction = attachedObject.transform.position - player.transform.position;
        if (Vector3.Distance(player.transform.position, attachedObject.transform.position) >= 0.5) player.GetComponent<ImpactReceiver>().AddImpact(direction, pullSpeed);
        else if (Vector3.Distance(player.transform.position, attachedObject.transform.position) < 0.5) Dettach();
    }

    private void Dettach()
    {
        player.gravityOn = true;
        isAttached = false;
        Destroy(hook);
        hook = null;
        player.canDoubleJump = true;
        grappleTimer = grappleCooldown;
    }
}