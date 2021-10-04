using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappinessMeter : MonoBehaviour
{
    private IslandTipper islandTipper;

    public Transform maxHappiness;
    public Transform minHappiness;

    private void Start()
    {
        islandTipper = GameObject.Find("Island Tipper").GetComponent<IslandTipper>();
    }

    private void Update()
    {
        transform.position = minHappiness.position + (Mathf.InverseLerp(-80.0f, 100.0f, islandTipper.happiness)*(maxHappiness.position - minHappiness.position));
    }
}
