using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class DraggableBuilding : MonoBehaviour
{
    [HideInInspector] public bool dragging;
    private LayerMask mask;
    private int buildingsPlacedCount;
    private IslandTipper islandTipper;
    private SpriteRenderer sr;

    public GameObject building;
    public Transform startingPoint;
    public Transform endingPoint;

    public GameObject victoryObject;

    public List<GameObject> buildingSpawnOrder;

    void Start()
    {
        dragging = false;

        mask = 1 << LayerMask.NameToLayer("Plot");
        islandTipper = GameObject.Find("Island Tipper").GetComponent<IslandTipper>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // actually move the building if the player is dragging them
        if(dragging)
        {
            // have the dragged object follow the mouse
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        // while the not being dragged, slowly move the building from the starting point to the end
        // point, with its speed being dictated by the town happiness
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, endingPoint.position, 1.5f*(1 + islandTipper.happiness/100) * Time.deltaTime);
        }
    }

    private void OnMouseDown()
    {
        dragging = true;
    }

    private void OnMouseUp()
    {
        if(dragging)
        {
            dragging = false;

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // check for a box underneath us. if there is a spot to place this building,
            // place it there. otherwise, return us back to our starting point
            Collider2D[] plotCollider = Physics2D.OverlapPointAll(mousePosition, mask);
            if (plotCollider != null && plotCollider.Length != 0) 
            {
                if(buildingsPlacedCount == buildingSpawnOrder.Count - 1)
                {
                    Plot plot = plotCollider[0].GetComponent<Plot>();
                    plot.PlaceBuilding(building);

                    transform.position = startingPoint.position;
                    endingPoint = startingPoint;

                    islandTipper.gameOver = true;

                    Invoke("SetVictoryActive", 2);
                    GameEvents.Victory.Invoke();
                }
                else
                {
                    Plot plot = plotCollider[0].GetComponent<Plot>();
                    plot.PlaceBuilding(building);
                    GameEvents.BuildingPlaced.Invoke();

                    transform.position = startingPoint.position;

                    // keep track of the number of buildings placed in total, which matters for spawn order
                    buildingsPlacedCount++;

                    // change the building to the next one in the order we want to spawn
                    building = buildingSpawnOrder[buildingsPlacedCount];
                    sr.sprite = building.GetComponent<SpriteRenderer>().sprite;
                }
            }
            else
            {
                transform.position = endingPoint.position;
            }
        }
    }

    public void SetVictoryActive()
    {
        victoryObject.SetActive(true);
    }
}
