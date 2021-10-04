using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioController : MonoBehaviour
{
    private AudioSource audioSource;
    private IslandTipper islandTipper;

    public AudioClip placeClip;
    public AudioClip explosionClip;
    public AudioClip splashClip;
    public AudioClip victoryClip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        islandTipper = GameObject.Find("Island Tipper").GetComponent<IslandTipper>();

        GameEvents.BuildingPlaced.AddListener(Placed);
        GameEvents.GameOver.AddListener(Explosion);
        GameEvents.Splash.AddListener(Splash);
        GameEvents.Victory.AddListener(Victory);
    }

    private void Update()
    {
        if(islandTipper.gameOver)
        {
            audioSource.volume -= Time.deltaTime / 10.0f;
        }
    }

    private void Placed()
    {
        audioSource.PlayOneShot(placeClip);
    }

    private void Explosion()
    {
        audioSource.PlayOneShot(explosionClip);
    }

    private void Splash()
    {
        audioSource.PlayOneShot(splashClip);
    }

    private void Victory()
    {
        audioSource.PlayOneShot(victoryClip);
    }
}

public static class GameEvents
{
    public static UnityEvent BuildingPlaced = new UnityEvent();
    public static UnityEvent GameOver = new UnityEvent();
    public static UnityEvent Splash = new UnityEvent();
    public static UnityEvent Victory = new UnityEvent();
}
