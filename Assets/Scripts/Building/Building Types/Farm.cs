using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Building
{
    private void Start()
    {
        buildingType = BuildingType.Farm;
    }

    // does NOT want to be clustered
    public override float ComputeHappiness(Transform plotPosition)
    {
        Collider2D[] nearbyBuildings = GetNearbyBuildings(plotPosition.position);

        float placementHappiness = 10;

        foreach (Collider2D buildingPlotCollider in nearbyBuildings)
        {
            Vector3 buildingPosition = buildingPlotCollider.transform.localPosition;

            float distance = Vector2.Distance(plotPosition.localPosition, buildingPosition);

            placementHappiness -= 10f * (3 - distance);
        }

        placementHappiness = Mathf.Clamp(placementHappiness, -60, 10);

        return placementHappiness;
    }
}
