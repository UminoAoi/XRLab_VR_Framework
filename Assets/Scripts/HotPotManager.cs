using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotPotManager : MonoBehaviour
{
    List<Transform> hidingSpots;
    AudioSource audioSource;

    public static bool isGameStarted = false;
    public static float lastDistance;

    [SerializeField]
    GameObject player, hidingSpotsParent, hidingObject;
    [SerializeField]
    float distanceDifference = 2f;
    [SerializeField]
    AudioClip instructionAudio, closeAudio, farAudio;

    void Start()
    {
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

        hidingObject.transform.position = choosenPlace.position;

    }

    public void StartGame() {
        isGameStarted = true;
        lastDistance = Vector3.Distance(hidingObject.transform.position, player.transform.position);

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

}
