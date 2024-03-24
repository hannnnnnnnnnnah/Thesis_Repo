using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class NarrativeManager : MonoBehaviour
{
    [SerializeField] GameObject overheadLight, startSpawn, newSpawn, firstLights;
    [SerializeField] TrainMove trainMove, trainMove1;
    [SerializeField] Animator wifeAnim;
    [SerializeField] AudioSource crash, lightExplode, flashback;

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
            PlayerMovement.instance.speed = 4f;
            trainMove1.move = true;
            StartCoroutine(WifeDeath());

            levelSwitched = true;
        }
    }

    IEnumerator WifeDeath()
    {
        wifeAnim.SetBool("Die", true);
        yield return new WaitForSeconds(3f);
        crash.Play();
        yield return new WaitForSeconds(1f);
        lightExplode.Play();
        //flashback.Play();
        PlayerMovement.instance.speed = 15f;
        TriggerLevelSwitch();
        yield return new WaitForSeconds(6f);
        Destroy(trainMove1.gameObject);
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

        DeathTimer.instance.StartDeathTimer();

        trainMove.move = true;
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
