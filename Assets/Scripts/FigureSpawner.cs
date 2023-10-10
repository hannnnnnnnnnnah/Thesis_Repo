using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureSpawner : MonoBehaviour
{
    [SerializeField] GameObject bFigure;

    public void figureSpawn()
    { 
        bFigure.SetActive(true);

    }
}
