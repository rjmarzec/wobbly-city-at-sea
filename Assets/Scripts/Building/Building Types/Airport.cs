using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airport : Building
{
    private void Start()
    {
        buildingType = BuildingType.Stadium;
    }

    // wants to bevery far from the center of the map
    public override float ComputeHappiness(Transform plotPosition)
    {
        float placementHappiness = -50 + 10 * (Mathf.Abs(plotPosition.localPosition.x)-3) ;

        placementHappiness = Mathf.Clamp(placementHappiness, -70, 10);

        return placementHappiness;
    }
}
