using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using Assets.Scripts._1MiniGame;
using Assets.SteamVR.InteractionSystem.Core.Scripts;
using UnityEngine;

public class MiniGame1Manager : MonoBehaviour
{
    protected MiniGame1Manager() { }
    public bool gameStarted;
    public Player player;
    public AudioSource audioSource;
    public AudioClip instructions, fireAudioClip, waterAudioClip, stormAudioClip, lightningAudioClip, goodAudioClip, badAudioClip, tooLateAudioClip;
    private Stopwatch intervalStopwatch;
    private Stopwatch reactionStopwatch;
    public long timeIntervalInMs = 6000;
    private long timeWaited = 0;
    public long timeToReactInMs = 5000;
    private long timeElapsed = 0;
    private ItemType lastPlayed;


    // Start is called before the first frame update
    private void Start()
    {
        intervalStopwatch = new Stopwatch();
        reactionStopwatch = new Stopwatch();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = instructions;
        audioSource.Play();
    }

    // Update is called once per frame
    private void Update()
    {
        if (gameStarted)
        {
            timeWaited = intervalStopwatch.ElapsedMilliseconds;
            if (timeWaited >= timeIntervalInMs)
            {
                PlaySound();
                intervalStopwatch.Restart();
            }
        }
    }

    public void StartMiniGame()
    {
        gameStarted = true;
        intervalStopwatch.Start();
    }

    public void PlaySound()
    {
        int rand = Random.Range(0, 3);
        lastPlayed = (ItemType)rand;
        switch (lastPlayed)
        {
            case ItemType.Feuer:
                audioSource.clip = fireAudioClip;
                break;
            case ItemType.Wasser:
                audioSource.clip = waterAudioClip;
                break;
            case ItemType.Sturm:
                audioSource.clip = stormAudioClip;
                break;
            case ItemType.Blitz:
                audioSource.clip = lightningAudioClip;
                break;
        }
        audioSource.Play();
        reactionStopwatch.Start();
    }

    public void OnInteract(ItemType itemType)
    {
        if (gameStarted)
        {
            timeElapsed = reactionStopwatch.ElapsedMilliseconds;
            if (timeElapsed <= timeToReactInMs)
            {
                if (lastPlayed != null)
                {
                    if (lastPlayed == itemType)
                    {
                        audioSource.clip = goodAudioClip;
                        audioSource.Play();
                    }
                    else
                    {
                        audioSource.clip = badAudioClip;
                        audioSource.Play();
                    }
                }
            }
            else
            {
                audioSource.clip = tooLateAudioClip;
                audioSource.Play();
            }
            reactionStopwatch.Reset();
            reactionStopwatch.Stop();
        }
        else
        {
            StartMiniGame();
        }
    }
}
