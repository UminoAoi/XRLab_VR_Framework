﻿using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Foe : MonoBehaviour
{
 
     public Transform Player;
     int MoveSpeed = 4;
     int MinDist = 1;
     public int is_selected = 0;
     public int is_watched = 0;
     public int was_seen = 0;
     public AudioSource audioSource;
 
 
 
 
     void Start()
     {
        Scene scene = gameObject.scene;
        // Player = GameObject.FindWithTag("Player").transform;
     }
 
     void Update()
     {
         transform.LookAt(Player);
 
         if ((Vector3.Distance(transform.position, Player.position) >= 1) && (is_selected != 0))
         {
 
             transform.position += transform.forward * MoveSpeed * Time.deltaTime;
 
 
 
             if (Vector3.Distance(transform.position, Player.position) <= MinDist)
             {
                 audioSource.Play();
                 SceneManager.LoadScene("3MiniGame");
             }
 
         }
     }
         
     

     void OnTriggerEnter(Collider other)
     {
        Debug.Log("POOP");
        if(other.gameObject.tag=="Ebalo")
            is_watched = 1;
            if(is_selected != 0)
                was_seen = 1;
     }

     void OnTriggerExit(Collider other) 
     {
        if(other.gameObject.tag=="Ebalo")
            is_watched = 0;
     }
 }
