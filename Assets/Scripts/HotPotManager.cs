using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotPotManager : MonoBehaviour
{
    List<Transform> hidingSpots;
    AudioSource audioSource;

    FindItem hidingObject;
    ExitFromMinigame exit;

    public bool isGameStarted = false;
    public float lastDistance;

    [SerializeField]
    GameObject player, hidingSpotsParent;
    [SerializeField]
    float distanceDifference = 2f;
    [SerializeField]
    AudioClip instructionAudio, closeAudio, farAudio, foundClip;

    void Start()
    {
        hidingObject = FindObjectOfType<FindItem>();
        exit = FindObjectOfType<ExitFromMinigame>();

        audioSource = GetComponent<AudioSource>();
        hidingSpots = new List<Transform>(hidingSpotsParent.GetComponentsInChildren<Transform>());

        audioSource.clip = instructionAudio;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStarted) {

            GiveInstruction();

        }

    }

    public void HideObject() {
        int random = Random.Range(0, hidingSpots.Count - 1);
        Transform choosenPlace = hidingSpots[random];

        hidingObject.transform.parent = null;
        hidingObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        hidingObject.transform.position = choosenPlace.position;

    }

    public void StartGame() {
        isGameStarted = true;
        lastDistance = Vector3.Distance(hidingObject.transform.position, player.transform.position);
        
        exit.ChangeExitAvailibity(true);
    }

    public void GiveInstruction() {
        
        float newDistance = Vector3.Distance(hidingObject.transform.position, player.transform.position);
        if (Mathf.Abs(newDistance - lastDistance) < distanceDifference)
            return;

        if(newDistance > lastDistance) {

            audioSource.clip = farAudio;

        } else {

            audioSource.clip = closeAudio;

        }

        audioSource.Play();
        lastDistance = newDistance;
    }

    public void ObjectInteraction() {
        if (!isGameStarted) {

            HideObject();
            StartGame();

        } else {

            isGameStarted = false;

            audioSource.clip = foundClip;
            audioSource.Play();

            exit.ChangeExitAvailibity(false);

        }
    }

}
