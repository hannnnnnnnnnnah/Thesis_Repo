using UnityEngine;

public class Scene1Manager : MonoBehaviour
{
    [SerializeField] GameObject overheadLight, startSpawn, newSpawn, firstVictim;
    [SerializeField] GameObject[] metrocars, lights;

    public bool figureKilled, trackDeathStart = false;
    public static Scene1Manager instance;

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

    private void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");

        if (!RespawnManager.instance.spawnChange)
            RespawnManager.instance.spawnPoint = startSpawn.transform.position;
        else
            TriggerLevelSwitch();
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
            light.GetComponent<Animator>().SetBool("LightExplode", true);
            light.GetComponentInChildren<LightTrigger>().lightBroken = true;
        } 

        if (InteractionManager.instance.emmaSpawned)
            GameObject.FindGameObjectWithTag("Emma").GetComponent<FigureApproach>().Despawn();

        if (figureKilled)
        {
            PlayerMovement.instance.StartDeathTimer();
            //UIManager.instance.ShowText("Hold shift to sprint");
        }
        else
            firstVictim.SetActive(false);
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
