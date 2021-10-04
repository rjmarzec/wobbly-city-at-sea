using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TippingUI : MonoBehaviour
{
    private IslandTipper islandTipper;
    private Text text;

    private void Start()
    {
        islandTipper = GameObject.Find("Island Tipper").GetComponent<IslandTipper>();
        text = GetComponent<Text>();
    }

    private void Update()
    {
        text.text = "Current Angle: " + islandTipper.currentAngle.ToString("F2") + " degrees";
        text.text += "\nDegrees Per Second: " + islandTipper.rotationPerSecond;
        text.text += "\nHappiness: " + islandTipper.happiness;

        text.text += "\n\nInstant Rotation: " + islandTipper.instantRotation;
    }
}
