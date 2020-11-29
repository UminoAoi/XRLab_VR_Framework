using System.Collections;
using System.Collections.Generic;
using Assets.Scripts._1MiniGame;
using Assets.SteamVR.InteractionSystem.Core.Scripts;
using UnityEngine;

public class MiniGame1Manager : MonoBehaviour {
    protected MiniGame1Manager() { }
    public bool gameStarted;
    public Player player;
    public AudioSource audioSource;
    public AudioClip instructions, fireAudioClip, waterAudioClip, earthAudioClip, windAudioClip, goodAudioClip, badAudioClip, tooLateAudioClip;
    ExitFromMinigame exit;

    private float newInstructionTime = 6;

    private float timeWaited = 0;
    private ItemType lastPlayed;

    int neededAnswers = 10;
    int currentAnswerCount = 0;

    public GameObject itemsParent, itemSpotsParent;
    public List<Transform> itemSpots;
    private List<GameObject> items;

    // Start is called before the first frame update
    private void Start() {
        exit = FindObjectOfType<ExitFromMinigame>();
        audioSource = GetComponent<AudioSource>();
        items = new List<GameObject>();

        for(int i = 0; i < itemsParent.transform.childCount; i++) {
            items.Add(itemsParent.transform.GetChild(i).gameObject);
        }
        itemSpots = new List<Transform>(itemSpotsParent.GetComponentsInChildren<Transform>());

        audioSource.clip = instructions;
        audioSource.Play();
    }

    // Update is called once per frame
    private void Update() {
        if (gameStarted) {
            timeWaited += Time.deltaTime;
            if (timeWaited >= newInstructionTime) {
                StartCoroutine(PlayLateSound());
            }
        }

        if(currentAnswerCount >= neededAnswers) {
            gameStarted = false;
            exit.ChangeExitAvailibity(false);
        }

    }

    public IEnumerator TryNextRound() {
        if (currentAnswerCount >= neededAnswers) {
            gameStarted = false;
            exit.ChangeExitAvailibity(false);
        } else {
            yield return new WaitForSeconds(1.5f);
            PlayNewSound();
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
        SwapPositions();
        yield return new WaitForSeconds(2);

        PlayNewSound();
    }

    public void PlayNewSound() {
        if (!gameStarted)
            return;

        timeWaited = 0;
        int rand = Random.Range(0, 4);
        lastPlayed = (ItemType)rand;
        switch (lastPlayed) {
            case ItemType.Feuer:
                audioSource.clip = fireAudioClip;
                break;
            case ItemType.Wasser:
                audioSource.clip = waterAudioClip;
                break;
            case ItemType.Erde:
                audioSource.clip = earthAudioClip;
                break;
            case ItemType.Wind:
                audioSource.clip = windAudioClip;
                break;
            default:
                Debug.Log(lastPlayed);
                break;
        }
        audioSource.Play();
        timeWaited = 0;
    }

    public void SwapPositions()
    {
        foreach (var item in items)
        {
            Vector3 lastPosition = item.transform.position;
            int rand = Random.Range(0, itemSpots.Count - 1);
            Vector3 newPosition = itemSpots[rand].position;

            GameObject itemOnPosition = isFree(newPosition);
            if (itemOnPosition == null)
            {
                item.transform.position = newPosition;
            }
            else
            {
                GameObject itemToSwap = itemOnPosition;
                item.transform.position = itemToSwap.transform.position;
                itemToSwap.transform.position = lastPosition;
            }
        }
    }

    private GameObject isFree(Vector3 newPosition)
    {
        foreach (var item in items)
        {
            if (item.transform.position == newPosition)
            {
                return item;
            }
        }

        return null;
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
            SwapPositions();
            StartCoroutine(TryNextRound());
        }
        else
        {
            if (itemType == ItemType.Start)
            {
              StartMiniGame();  
            }
        }
    }
}
