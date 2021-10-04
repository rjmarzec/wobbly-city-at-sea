using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMover : MonoBehaviour
{
    private float startingY;

    private void Start()
    {
        startingY = transform.position.y;
    }

    void Update()
    {
        float newX = transform.position.x + Time.deltaTime * 0.5f;
        float newY = startingY + 0.2f * Mathf.Sin(Mathf.PI * Time.time / 2);

        if(newX >= 28.5f * 1.5f)
        {
            newX -= 28.5f * 3f;
        }

        transform.position = new Vector3(newX, newY, 0);

    }
}
