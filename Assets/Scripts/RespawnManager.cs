using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    [SerializeField] AudioSource inhale;

    public bool gameStart, spawnChange = false;
    public int deathCount = 1;
    public Animator animator;
    public Vector3 spawnPoint;

    Canvas canvas;
    GameObject Player;

    public static RespawnManager instance;

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

        DontDestroyOnLoad(gameObject);

        Player = GameObject.FindGameObjectWithTag("Player");
        SceneManager.sceneLoaded += OnLevelLoad;
    }

    private void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        //Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            animator.SetBool("Respawn", false);
            SceneManager.LoadScene("TitleScreen");
        }

        if(Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log(spawnPoint);
        }
    }

    private void OnDisable()
    {
         SceneManager.sceneLoaded -= OnLevelLoad;
    }

    void OnLevelLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "TitleScreen")
        {
            if (!spawnChange)
                spawnPoint = GameObject.FindGameObjectWithTag("SpawnStart").transform.position;

            if (canvas != null)
                canvas.enabled = true;
        }
        else
        {
            if (canvas != null)
                canvas.enabled = false;

            spawnChange = false;
        }
    }

    public void ChangeSpawn(Vector3 newSpawn)
    {
        spawnPoint = newSpawn;
        spawnChange = true;
    }

    public void Die()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        //Set player position to spawn point
        Player.GetComponent<CharacterController>().enabled = false;
        Player.transform.position = spawnPoint;
        Player.GetComponent<CharacterController>().enabled = true;

        //UI reset
        UIManager.instance.ResetEye("EyeVisible", false);

        //"Awaken" effects
        inhale.Play();
        animator.SetBool("Respawn", true);

        //Reset player
        Player.GetComponent<PlayerMovement>().inTracks = false;
        Player.GetComponent<PlayerMovement>().StopSurroundSound();

        //Sanity is reset
        InteractionManager.instance.sanity = 5;
        InteractionManager.instance.UpdateSanity();

        //Increase death count
        deathCount++;
        Debug.Log("Death count:" + deathCount);
    }
}
