using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public BuildingType buildingType;
    public int constantWeight = 1;
    public int instantWeight = 1;

    public abstract float ComputeHappiness(Transform plotPosition);

    protected Collider2D[] GetNearbyBuildings(Vector3 plotPosition)
    {
        LayerMask mask = 1 << LayerMask.NameToLayer("Building");
        return Physics2D.OverlapCircleAll(plotPosition, 2.5f, mask);
    }
}
