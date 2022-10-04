using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolObject : MonoBehaviour
{
    [SerializeField] List<Transform> patrolPoints = new List<Transform>();
    [SerializeField] float patrolSpeed;
    private Transform targetPoint;
    private Transform startPoint;
    private int targetNum;
    private int startNum;

    // Start is called before the first frame update
    void Start()
    {
        targetPoint = patrolPoints[0];
        startPoint = targetPoint;
        targetNum = 0;
        startNum = targetNum;
        print(targetPoint.name);
        for(int i = 1; i < patrolPoints.Count; i++)
        {
            if(Vector3.Distance(transform.position, patrolPoints[i].position) < Vector3.Distance(transform.position, targetPoint.position))
            {
                targetPoint = patrolPoints[i];
                targetNum = i;
                startPoint = patrolPoints[i];
                startNum = targetNum;
                print("Start: " + startPoint.name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(targetPoint);
        if(Vector3.Distance(transform.position, targetPoint.position) > 0.1f)
        {
            transform.position += patrolSpeed * Time.deltaTime * transform.forward;
        }
        else
        {
            if (targetNum + 1 < patrolPoints.Count)
            {
                targetPoint = patrolPoints[targetNum + 1];
                targetNum++;
                print(targetPoint.name);
            }
            else
            {
                targetPoint = startPoint;
                print(targetPoint.name);
                targetNum = startNum;
            }
        }
    }
}
