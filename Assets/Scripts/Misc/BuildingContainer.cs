using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingContainer : MonoBehaviour
{
    public GameObject house, farm, store, stadium, airport;

    public GameObject GetBuildingObject(BuildingType buildingType)
    {
        if(buildingType == BuildingType.House)
        {
            return house;
        }
        else if(buildingType == BuildingType.Farm)
        {
            return farm;
        }
        else if (buildingType == BuildingType.Store)
        {
            return store;
        }
        else if (buildingType == BuildingType.Stadium)
        {
            return stadium;
        }
        else if (buildingType == BuildingType.Airport)
        {
            return airport;
        }
        else
        {
            return null;
        }
    }
}

public enum BuildingType { Empty, House, Farm, Store, Stadium, Airport };
