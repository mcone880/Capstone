using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    [SerializeField] public List<Node> nodes = new List<Node>();
    [SerializeField] public Area parentArea;
    [SerializeField] public List<Area> childrenAreas;
    [HideInInspector] public float value;
}
