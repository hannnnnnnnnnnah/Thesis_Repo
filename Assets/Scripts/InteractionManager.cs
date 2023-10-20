using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager instance;

    int sanitySet = 10;
    public int sanity = 5;
    public bool lightExplodes, surroundSound, whisper, steps = false;

    GameObject figureSpawner;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ResetSanity()
    {
        sanity = sanitySet;
        UpdateSanity();
    }

    public void UpdateSanity()
    {
        Debug.Log("sanity is at" + " " + sanity);

        switch (sanity)
        {
            case 5:
                lightExplodes = false;
                surroundSound = false;
                break;

            case 4:
                surroundSound = true;
                steps = true;
                break;

            case 3:
                whisper = true;
                lightExplodes = true;
                break;

            case 2:
                figureSpawner = GameObject.FindGameObjectWithTag("FigureSpawner");
                figureSpawner.GetComponent<FigureSpawner>().SpawnEmma();
                break;
            
            case 1:
                break;

            case -5:
                break;
        }
    }
}
