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
    public bool lightExplodes, steps = false;

    [SerializeField] GameObject figureSpawner;

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

        //DontDestroyOnLoad(gameObject);
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
                steps = false;
                break;

            case 4:
                steps = true;
                break;

            case 3:
                lightExplodes = true;
                break;

            case 2:
                figureSpawner.GetComponent<FigureSpawner>().figureSpawn();
                break;
            
            case 1:
                break;
        }
    }
}
