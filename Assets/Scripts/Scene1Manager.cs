using UnityEngine;

public class Scene1Manager : MonoBehaviour
{
    [SerializeField] GameObject overheadLight, startSpawn, newSpawn, firstVictim;

    public bool figureKilled = false;

    //GameObject player;

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

    public void TriggerLevelSwitch()
    {
        RespawnManager.instance.ChangeSpawn(newSpawn.transform.position);
        overheadLight.SetActive(false);

        if (InteractionManager.instance.emmaSpawned)
            GameObject.FindGameObjectWithTag("Emma").GetComponent<FigureApproach>().Despawn();

        if (figureKilled)
        {
            PlayerMovement.instance.StartDeathTimer();
            UIManager.instance.ShowText("Hold shift to sprint");
        }
        else
            firstVictim.SetActive(false);
    }

}
