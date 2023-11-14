using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager instance;

    int sanitySet = 5;
    public int sanity = 5;
    public bool lightExplodes, surroundSound, whisper, steps, emmaSpawned = false;

    GameObject figureSpawner;
    [SerializeField] Volume sanityVolume;

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
                emmaSpawned = false;
                sanityVolume.weight = 0.0f;
                break;

            case 4:
                surroundSound = true;
                steps = true;
                sanityVolume.weight += .2f;
                break;

            case 3:
                whisper = true;
                lightExplodes = true;
                sanityVolume.weight += .2f;
                break;

            case <= 2:
                if (!emmaSpawned)
                {
                    figureSpawner = GameObject.FindGameObjectWithTag("FigureSpawner");
                    figureSpawner.GetComponent<FigureSpawner>().SpawnEmma();
                    emmaSpawned = true;
                }

                if(emmaSpawned)
                    GameObject.FindGameObjectWithTag("Emma").GetComponent<FigureApproach>().speed += .1f;

                sanityVolume.weight += .2f;

                break;
        }
    }
}
