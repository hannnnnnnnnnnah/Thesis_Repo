using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager instance;

    int sanitySet = 10;
    public int sanity = 10;
    public bool lightExplodes, steps = false;

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

        DontDestroyOnLoad(gameObject);
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
            case 10:
                lightExplodes = false;
                steps = false;
                break;

            case 9:
                steps = true;
                break;

            case 8:
                lightExplodes = true;
                break;
        }
    }
}
