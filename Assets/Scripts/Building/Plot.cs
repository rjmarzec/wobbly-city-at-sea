using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    private SpriteRenderer sr;
    private IslandTipper islandTipper;
    private DraggableBuilding nextBuilding;
    private bool alreadyCalculatedHappiness;
    private float buildingHappiness;

    public Building building;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        islandTipper = GameObject.Find("Island Tipper").GetComponent<IslandTipper>();
        nextBuilding = GameObject.Find("Island Tipper/Building Queue/Next Building").GetComponent<DraggableBuilding>();

        if(building != null)
        {
            buildingHappiness = building.ComputeHappiness(transform);
            PlaceBuilding(GameObject.Find("Misc/Building Container").GetComponent<BuildingContainer>().GetBuildingObject(building.buildingType));
        }
    }

    private void Update()
    {
        if(building == null)
        {
            if (Input.GetMouseButtonDown(0) && nextBuilding.dragging)
            {
                buildingHappiness = nextBuilding.building.GetComponent<Building>().ComputeHappiness(transform);

                if (buildingHappiness > 0)
                {
                    float nonPrimaryValue = Mathf.Clamp(1 - buildingHappiness / 20, 0, 1);
                    sr.color = new Color(nonPrimaryValue, 1, nonPrimaryValue);
                }
                else
                {
                    float nonPrimaryValue = Mathf.Clamp(1 + buildingHappiness / 20, 0, 1);
                    sr.color = new Color(1, nonPrimaryValue, nonPrimaryValue);
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                sr.color = new Color(1, 1, 1, 0.35f);
                alreadyCalculatedHappiness = false;
            }
        }
    }

    public void PlaceBuilding(GameObject placedBuilding)
    {
        // update the layer so we don't get recongized later 
        gameObject.layer = LayerMask.NameToLayer("Building");

        building = placedBuilding.GetComponent<Building>();

        islandTipper.AddBuilding(building.constantWeight, building.instantWeight, transform.localPosition.x, buildingHappiness);
        sr.sprite = placedBuilding.GetComponent<SpriteRenderer>().sprite;
        sr.color = Color.white;
    }
}
