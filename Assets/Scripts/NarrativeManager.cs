using UnityEngine;
using System.Collections.Generic;

public class NarrativeManager : MonoBehaviour
{
    [SerializeField] GameObject overheadLight, startSpawn, newSpawn;

    public List<GameObject> metrocars, lights;
    public bool figureKilled, trackDeathStart = false;
    public static NarrativeManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        TriggerLevelSwitch();

        PlayerMovement.instance.StartDeathTimer();
        lights = new List<GameObject>();
    }

    private void Update()
    {
        if (PlayerMovement.instance.inTracks)
        {
            if(!trackDeathStart)
                TriggerTrainDeath();
        }
    }

    public void TriggerLevelSwitch()
    {
        //Change spawnpoint 
        RespawnManager.instance.ChangeSpawn(newSpawn.transform.position);
        
        //Turn off lights; show trains; blackout some lights
        overheadLight.SetActive(false);
        ShowTrains();

        foreach (GameObject light in lights)
        {
            light.GetComponentInParent<Animator>().SetBool("LightExplode", true);
            light.GetComponentInChildren<LightTrigger>().lightBroken = true;
        } 
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
