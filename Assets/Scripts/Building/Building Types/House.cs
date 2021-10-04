using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Building
{
    private void Start()
    {
        buildingType = BuildingType.House;
    }

    // wants to be clustered with other things, but mostly a filler object
    public override float ComputeHappiness(Transform plotPosition)
    {
        Collider2D[] nearbyBuildings = GetNearbyBuildings(plotPosition.position);

        float placementHappiness = -20;

        foreach(Collider2D buildingPlotCollider in nearbyBuildings)
        {
            Vector3 buildingPosition = buildingPlotCollider.transform.localPosition;

            float distance = Vector2.Distance(plotPosition.localPosition, buildingPosition);

            // +21 to +0 happiness for every building within 0-3 units of the house
            placementHappiness += 20 * (3 - distance);
        }

        placementHappiness = Mathf.Clamp(placementHappiness, -20, 1);

        return placementHappiness;
    }
}
