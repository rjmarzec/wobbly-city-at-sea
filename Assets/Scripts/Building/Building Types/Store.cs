using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : Building
{
    private void Start()
    {
        buildingType = BuildingType.Store;
    }

    // doesn't want to clump next to other stores
    public override float ComputeHappiness(Transform plotPosition)
    {
        Collider2D[] nearbyBuildings = GetNearbyBuildings(plotPosition.position);

        float placementHappiness = 10;

        foreach (Collider2D buildingCollider in nearbyBuildings)
        {
            if(buildingCollider.GetComponent<Plot>().building.buildingType == BuildingType.Store)
            {
                Vector3 buildingPosition = buildingCollider.transform.localPosition;

                float distance = Vector2.Distance(plotPosition.localPosition, buildingPosition);

                placementHappiness -= 35 * (3 - distance);
            }
        }

        placementHappiness = Mathf.Clamp(placementHappiness, -60, 5);

        return placementHappiness;
    }
}
