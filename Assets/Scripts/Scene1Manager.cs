using UnityEngine;

public class Scene1Manager : MonoBehaviour
{
    [SerializeField] GameObject overheadLight, startSpawn, newSpawn, firstVictim;

    public bool figureKilled = false;

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
            GameObject.FindGameObjectWithTag("Player").GetComponent<DeathTimer>().startDeathTimer();
            UIManager.instance.ShowText("Hold shift to sprint");
        }
        else
            firstVictim.SetActive(false);
    }

}
