﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLogic : MonoBehaviour
{
    float ranPos;
    public GameObject[] foes;
    public GameObject selected;
    public float targetTime;
    Foe referenceScript;
    public int counter = 0;

    public Transform Foe_prefab;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(Foe_prefab, new Vector3(Random.Range(-20.0f, 20.0f), 1, Random.Range(-20.0f, 20.0f)), Quaternion.identity);
            // Debug.Log();
        }

        // foes = GameObject.FindGameObjectsWithTag("Foe");


    }

    // Update is called once per frame
    void Update()
    {
        if(counter == 10)
            //exitScene
        foes = GameObject.FindGameObjectsWithTag("Foe");
        targetTime -= Time.deltaTime;

        if(targetTime < 0.1f)
        {
            killall();
            updateTime();
        }

        if((targetTime < 1.0f) && (referenceScript.is_selected == 0))
        {
            undead();
            Debug.Log("ASS");
        }



    }


    void updateTime()
    {
        targetTime = 5.0f;
    }


    void undead()
    {
        while (true)
        {
         selected = foes[Random.Range(0, foes.Length)];
         referenceScript = selected.GetComponent<Foe>();
         if(referenceScript.is_watched == 0)
         {
            referenceScript.is_selected = 1;
            break;
         }

        }

    }

    void killall()
    {
        foreach (GameObject Foee in foes)
        {
            referenceScript = Foee.GetComponent<Foe>();
            referenceScript.is_selected = 0;
        }
    }
}