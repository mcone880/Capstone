using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryWhenEmpty : MonoBehaviour
{
    private void LateUpdate()
    {
        if (transform.childCount == 0) Destroy(gameObject);
    }
}
