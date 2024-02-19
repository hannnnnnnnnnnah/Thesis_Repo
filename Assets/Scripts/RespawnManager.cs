using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    [SerializeField] AudioSource inhale;

    public bool gameStart, spawnChange = false;
    public int deathCount = 0;
    public Animator animator;
    public Vector3 spawnPoint;

    Canvas canvas;

    public static RespawnManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnLevelLoad;

        canvas = GetComponentInChildren<Canvas>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
            Die();
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
        //Set player position to spawn point
        PlayerMovement.instance.GetComponent<CharacterController>().enabled = false;
        PlayerMovement.instance.transform.position = spawnPoint;
        PlayerMovement.instance.GetComponent<CharacterController>().enabled = true;

        //UI reset
        UIManager.instance.ResetEye("EyeVisible", false);

        //"Awaken" effects
        inhale.Play();
        animator.SetBool("Respawn", true);

        //Reset player
        PlayerMovement.instance.inTracks = false;
        PlayerMovement.instance.StopSurroundSound();

        //Sanity is reset
        InteractionManager.instance.sanity = 5;
        InteractionManager.instance.UpdateSanity();

        //Increase death count
        deathCount++;
        Debug.Log("Death count:" + deathCount);
    }
}
