using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureSpawner : MonoBehaviour
{
    [SerializeField] GameObject behindLoc;
    [SerializeField] GameObject bFigure;

    void figureSpawn()
    {
        if(InteractionManager.instance.bFigureSpawn)
            bFigure.SetActive(true);

    }
}
