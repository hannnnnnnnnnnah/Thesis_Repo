using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    [SerializeField] AudioSource inhale;

    public bool gameStart, spawnChange = false;
    public int deathCount = 0;
    public Vector3 spawnPoint;

    public static RespawnManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnLevelLoad;
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

        //"Awaken" effects
        inhale.Play();
        StopAllCoroutines();
        StartCoroutine(RespawnEffects());

        //Reset player
        PlayerMovement.instance.inTracks = false;

        //Increase death count
        deathCount++;
        Debug.Log("Death count:" + deathCount);
    }

    IEnumerator RespawnEffects()
    {
        PlayerMovement.instance.animator.SetBool("Respawn", true);
        yield return new WaitForSeconds(.5f);
        PlayerMovement.instance.animator.SetBool("Respawn", false);
    }
}
