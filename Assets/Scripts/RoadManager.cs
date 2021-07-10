using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public static RoadManager instance;

    public GameObject roadsHolder;

    public UnityEngine.Material lineMaterial;

    public void Awake()
    {
        instance = this;
    }
}