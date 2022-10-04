using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DashController : MonoBehaviour
{
    public TMP_Text dashPointsTxT;
    
    PlayerController player;
    
    public float dashSpeed;
    public float dashTime;
    private int dashPoints = 99;
    float elapsed = 0f;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
        //canvas = GameObject.Find("Canvas");
        //dashPointsTxT = canvas.Get
    }

    // Update is called once per frame
    void Update()
    {
        dashPointsTxT.text = "Dash Points: " + dashPoints;
        elapsed += Time.deltaTime;
        if (elapsed >= 1f)
        {
            elapsed = elapsed % 1f;
            DashRecharge();
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && dashPoints >= 33)
        {
            dashPoints -= 33;
            StartCoroutine(Dash());
        }
    }

    private void DashRecharge()
    {
            if (player.character.isGrounded) dashPoints += 12;
            else dashPoints += 6;
        if (dashPoints > 99) dashPoints = 99;
            dashPointsTxT.text = "Dash Points: " + dashPoints;
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;

        while(Time.time < startTime + dashTime)
        {
            player.gravityOn = false;
            player.isDashing = true;
            Vector3 actualDirection = player.direction;

            actualDirection.y = 0;
            if (actualDirection.sqrMagnitude == 0) actualDirection = player.transform.forward;
            actualDirection.y = 0;
            player.character.Move(actualDirection.normalized * dashSpeed * Time.deltaTime);
            yield return null;
        }
        player.directionY = 0;
        player.gravityOn = true;
        player.isDashing = false;
    }
}
