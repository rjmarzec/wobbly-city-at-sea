using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandTipper : MonoBehaviour
{
    public float currentAngle = 0.0f;
    public float rotationPerSecond = 0.0f;
    public float instantRotation = 0.0f;

    public float totalWeight = 0.0f;
    public float weightedWeight = 0.0f;
    public float instantWeight = 0.0f;

    public float happiness = 0.0f;

    public bool gameOver = false;
    public float scalingFactor = 1.0f;

    public GameObject victoryObject;
    public GameObject defeatObject;

    private void Update()
    {
        if(!gameOver)
        {
            UpdateCurrentAngle();
            UpdateVisualRotation();

            // if we're outside these ranges, you lose
            if (currentAngle >= 30 || currentAngle <= -30)
            {
                GameEvents.GameOver.Invoke();

                rotationPerSecond = 0;
                instantWeight = 0;
                happiness = -80;

                gameOver = true;
                StartCoroutine("GameOver");
            }
        }
    }

    public IEnumerator GameOver()
    {
        float currentTime = 0;
        float startingY = transform.position.y;

        while(currentTime <= 0.3f)
        {
            currentTime += Time.deltaTime;

            transform.position += Vector3.up * 20 * Time.deltaTime / (transform.position.y - startingY + 1f);
            yield return null;
        }
        yield return new WaitForSeconds(0.3f);

        bool playedSplash = false;
        while(currentTime <= 1.0)
        {
            currentTime += Time.deltaTime;

            if(!playedSplash && currentTime >= 0.4f)
            {
                playedSplash = true;
                GameEvents.Splash.Invoke();
            }

            transform.position += Vector3.down * 20 * Time.deltaTime;
            yield return null;
        }

        defeatObject.SetActive(true);
    }

    private void UpdateCurrentAngle()
    {
        // update what the current angle should be
        currentAngle += rotationPerSecond * Time.deltaTime;
        currentAngle += instantRotation * Time.deltaTime;
        instantRotation -= instantRotation * Time.deltaTime;
    }

    private void UpdateVisualRotation()
    {
        // set this object's rotation to the new angle
        try 
        {
            transform.rotation = Quaternion.Euler(0, 0, currentAngle);
        }
        catch
        {
            transform.rotation = Quaternion.Euler(0, 0, (currentAngle + 0.01f)*1.01f);
        }
        
    }

    public void AddBuilding(float buildingWeight, float buildingInstantWeight, float buildingXDistance, float buildingHappiness)
    {
        // when a new building is placed, add its weight to the running total,
        // add the instant weight, and computed the weighted weight based on 
        // how far the building is from the island center
        float distance = Mathf.Abs(buildingXDistance);
        float sign = Mathf.Sign(buildingXDistance);

        totalWeight += buildingWeight;
        weightedWeight += buildingWeight * sign * (distance - 0.2f)*(distance - 0.2f);
        instantWeight = buildingInstantWeight * sign * (distance - 0.2f) * (distance - 0.2f) * 4f;

        // update the rotation speeds to match the new weights
        UpdateRotationSpeed();

        // update the hapiness score, but limit it between -100 and 100
        happiness = Mathf.Clamp(happiness + buildingHappiness, -80, 100);
    }

    private void UpdateRotationSpeed()
    {
        // convert the weight of building into a constant rotation and clamp it reasonable values
        rotationPerSecond = -weightedWeight / (totalWeight + 5.0f);
        if(rotationPerSecond > 5.0f)
        {
            rotationPerSecond = 5.0f;
        }
        else if(rotationPerSecond < -5.0f)
        {
            rotationPerSecond = -5.0f;
        }
        else if(rotationPerSecond >= 0 && rotationPerSecond < 0.1)
        {
            rotationPerSecond = 0.1f;
        }
        else if (rotationPerSecond <= 0 && rotationPerSecond > -0.1)
        {
            rotationPerSecond = -0.1f;
        }

        // convert the instant weight into an instant rotation
        instantRotation = -instantWeight / (totalWeight + 5.0f);
        if (instantRotation > 10.0f)
        {
            instantRotation = 10.0f;
        }
        else if (instantRotation < -10.0f)
        {
            instantRotation = -10.0f;
        }
    }
}
