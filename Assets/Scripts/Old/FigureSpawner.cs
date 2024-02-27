using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureSpawner : MonoBehaviour
{
    [SerializeField] GameObject Emma;
    [SerializeField] GameObject behindPos;

    public void SpawnEmma()
    { 
        Instantiate(Emma, behindPos.transform);
        Debug.Log("emma is spawned");
    }

    public void KillEmma()
    {
        Destroy(GameObject.FindGameObjectWithTag("Emma")); 
    }
}
