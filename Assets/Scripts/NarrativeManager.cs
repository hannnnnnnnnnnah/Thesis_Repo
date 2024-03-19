using UnityEngine;
using System.Collections.Generic;

public class NarrativeManager : MonoBehaviour
{
    [SerializeField] GameObject overheadLight, startSpawn, newSpawn, firstLights;
    [SerializeField] TrainMove trainMove;

    public List<GameObject> metrocars, lights;
    public bool figureKilled, trackDeathStart, levelSwitched = false;
    public static NarrativeManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Update()
    {
        if (PlayerMovement.instance.inTracks)
        {
            if(!trackDeathStart)
                TriggerTrainDeath();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !levelSwitched)
        {   
            figureKilled = true;

            TriggerLevelSwitch();

            DeathTimer.instance.StartDeathTimer();
            lights = new List<GameObject>();

            trainMove.move = true;
        }
    }

    public void TriggerLevelSwitch()
    {
        //Change spawnpoint 
        RespawnManager.instance.ChangeSpawn(newSpawn.transform.position);
        
        //Turn off lights; show trains; blackout some lights
        overheadLight.SetActive(false);
        firstLights.SetActive(false);
        ShowTrains();

        foreach (GameObject light in lights)
        {
            light.GetComponentInParent<Animator>().SetBool("LightExplode", true);
            light.GetComponentInChildren<LightTrigger>().lightBroken = true;
        }

        levelSwitched = true;
    }

    public void TriggerTrainDeath()
    {
        trackDeathStart = true;

        foreach(GameObject metrocar in metrocars)
            metrocar.gameObject.SetActive(false);
    }

    public void ShowTrains()
    {
        trackDeathStart = false;

        foreach (GameObject metrocar in metrocars)
            metrocar.gameObject.SetActive(true);
    }

}
