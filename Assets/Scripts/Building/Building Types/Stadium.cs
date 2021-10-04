using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stadium : Building
{
    private void Start()
    {
        buildingType = BuildingType.Stadium;
    }

    // NEEDS to clump, otherwise gets a harsh penalty
    public override float ComputeHappiness(Transform plotPosition)
    {
        Collider2D[] nearbyBuildings = GetNearbyBuildings(plotPosition.position);

        float placementHappiness = -80;

        foreach(Collider2D buildingPlotCollider in nearbyBuildings)
        {
            Vector3 buildingPosition = buildingPlotCollider.transform.localPosition;

            float distance = Vector2.Distance(plotPosition.localPosition, buildingPosition);

            // +30 to +0 happiness for every building within 0-3 units of the house
            placementHappiness += 10 * (distance - 3);
        }

        placementHappiness = Mathf.Clamp(placementHappiness, -50, 30);

        return placementHappiness;
    }
}
