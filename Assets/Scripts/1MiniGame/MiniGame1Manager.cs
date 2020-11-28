using System.Collections;
using Assets.Scripts._1MiniGame;
using Assets.SteamVR.InteractionSystem.Core.Scripts;
using UnityEngine;

public class MiniGame1Manager : MonoBehaviour {
    protected MiniGame1Manager() { }
    public bool gameStarted;
    public Player player;
    public AudioSource audioSource;
    public AudioClip instructions, fireAudioClip, waterAudioClip, stormAudioClip, lightningAudioClip, goodAudioClip, badAudioClip, tooLateAudioClip;
    ExitFromMinigame exit;

    private float newIntructionTime = 6;

    private float timeWaited = 0;
    private ItemType lastPlayed;

    int neededAnswers = 10;
    int currentAnswerCount = 0;

    // Start is called before the first frame update
    private void Start() {
        exit = FindObjectOfType<ExitFromMinigame>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = instructions;
        audioSource.Play();
    }

    // Update is called once per frame
    private void Update() {
        if (gameStarted) {
            timeWaited += Time.deltaTime;
            if (timeWaited >= newIntructionTime) {
                StartCoroutine(PlayLateSound());
            }
        }

        if(currentAnswerCount >= neededAnswers) {
            gameStarted = false;
            exit.ChangeExitAvailibity(false);
        }

    }

    public void StartMiniGame() {
        gameStarted = true;
        exit.ChangeExitAvailibity(true);
        PlayNewSound();
    }

    public IEnumerator PlayLateSound() {
        timeWaited = 0;
        audioSource.clip = tooLateAudioClip;
        audioSource.Play();
        yield return new WaitForSeconds(2);

        PlayNewSound();
    }

    public void PlayNewSound() {
        if (!gameStarted)
            return;

        timeWaited = 0;
        int rand = Random.Range(0, 3);
        lastPlayed = (ItemType)rand;
        switch (lastPlayed) {
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
        timeWaited = 0;
    }

    public void OnInteract(ItemType itemType) {
        if (gameStarted) {
            if (lastPlayed == itemType) {
                audioSource.clip = goodAudioClip;
                currentAnswerCount++;
            } else {
                audioSource.clip = badAudioClip;
            }

            Debug.Log(currentAnswerCount);
            timeWaited = 0;
            audioSource.Play();
            PlayNewSound();
        } 
    }
}
