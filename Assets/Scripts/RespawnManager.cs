using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    [SerializeField] AudioSource inhale;
    Canvas canvas;
    GameObject Player;

    public Animator animator;
    public int deathCount = 1;
    public bool gameStart = false;
    public bool spawnChange = false;
    public Vector3 spawnPoint;

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

        SceneManager.sceneLoaded += OnLevelLoad;
    }

    private void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        Player = GameObject.FindGameObjectWithTag("Player");
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
        }
    }

    public void ChangeSpawn(Vector3 newSpawn)
    {
        spawnPoint = newSpawn;
        spawnChange = true;
    }

    public void Die()
    {
        //animator.SetBool("Respawn", false);

        //Set player position to spawn point
        Player.GetComponent<CharacterController>().enabled = false;
        Player.transform.position = spawnPoint;
        Player.GetComponent<CharacterController>().enabled = true;

        //"Awaken" effects
        inhale.Play();
        animator.SetBool("Respawn", true);

        //Increase death count
        deathCount++;
        Debug.Log("Death count:" + deathCount);
    }
}
