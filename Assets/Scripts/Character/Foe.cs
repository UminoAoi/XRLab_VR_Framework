using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class Foe : MonoBehaviour
{
 
     public Transform Player;
     int MoveSpeed = 4;
     int MinDist = 1;
     public int is_selected = 0;
 
 
 
 
     void Start()
     {
        Scene scene = gameObject.scene;
        Debug.Log(gameObject.name + " is from the Scene: " + scene.name);
        Debug.Log(scene.GetRootGameObjects()[1].name);
        // Player = GameObject.FindWithTag("Player").transform;
     }
 
     void Update()
     {
         transform.LookAt(Player);
 
         if ((Vector3.Distance(transform.position, Player.position) >= MinDist) && (is_selected != 0))
         {
 
             transform.position += transform.forward * MoveSpeed * Time.deltaTime;
 
 
 
             if (Vector3.Distance(transform.position, Player.position) <= MinDist)
             {
                 //EndGame
             }
 
         }
     }
 }
