using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class NarrativeManager : MonoBehaviour
{
    [SerializeField] GameObject overheadLight, startSpawn, newSpawn, firstLights, startProps, endProps, endWife, rotTarget;
    [SerializeField] TrainMove trainMove, trainMove1;
    [SerializeField] Animator wifeAnim;
    [SerializeField] AudioSource crash, lightExplode, flashback, scared, deathBG;

    public List<GameObject> metrocars, lights;
    public bool figureKilled, trackDeathStart, levelSwitched = false;
    bool forceRotate, blendRotation;
    Quaternion storedRotation;
    float rotateSpeed = 5f;
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

    private void FixedUpdate()
    {
        if (forceRotate)
        {
            //it's fine it's fine it's fine look away ahahhaha
            var targetRotation = Quaternion.LookRotation(rotTarget.transform.position - PlayerMovement.instance.mainCamera.transform.position);
            PlayerMovement.instance.mainCamera.transform.rotation = Quaternion.RotateTowards(PlayerMovement.instance.mainCamera.transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }

        if (blendRotation)
        {
            PlayerMovement.instance.mainCamera.transform.rotation = Quaternion.RotateTowards(PlayerMovement.instance.mainCamera.transform.rotation, storedRotation, rotateSpeed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !levelSwitched)
        {
            PlayerMovement.instance.speed = 4f;
            trainMove1.move = true;
            storedRotation = Quaternion.LookRotation(PlayerMovement.instance.mainCamera.transform.position);
            PlayerMovement.instance.rotate = false;
            forceRotate = true;

            StartCoroutine(WifeDeath());
            levelSwitched = true;
        }
    }

    IEnumerator WifeDeath()
    {
        wifeAnim.SetBool("Die", true);
        yield return new WaitForSeconds(3f);
        deathBG.Play();
        crash.Play();
        scared.Play();
        yield return new WaitForSeconds(1f);

        //Play background music
        BackgroundMusic.instance.PlayBackgroundMusic(1);

        lightExplode.Play();
        TriggerLevelSwitch();
        PlayerMovement.instance.speed = 2f;
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
            if(light.GetComponentInParent<Animator>() != null)
                light.GetComponentInParent<Animator>().SetBool("LightExplode", true);
            light.GetComponentInChildren<LightTrigger>().lightBroken = true;
        }

        DeathTimer.instance.StartDeathTimer();
        forceRotate = false;
        blendRotation = true;
        PlayerMovement.instance.rotate = true;

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

    public void EndOfGame()
    {
        //overheadLight.SetActive(true);
        firstLights.SetActive(true);
        startProps.SetActive(false); 
        endProps.SetActive(true);
        endWife.SetActive(true);
    }

}
