using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    public Animator animator;
    [SerializeField] AudioSource inhale;

    public int deathCount = 1;

    public bool exitTriggered = false;
    public bool gameStart = false; 

    public static RespawnManager instance;

    [SerializeField] Canvas canvas;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            animator.SetBool("Respawn", false);
            SceneManager.LoadScene("TitleScreen");
        }
    }

    private void OnDisable()
    {
         SceneManager.sceneLoaded -= OnLevelLoad;
    }

    private void Start()
    {
        canvas = GetComponentInChildren<Canvas>();   
    }

    void OnLevelLoad(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "TitleScreen")
        {
            if(canvas != null)
                canvas.enabled = false;
        }

        if(scene.name == "FigureTest" && gameStart)
        {
            Debug.Log("Death count:" + deathCount);
            canvas.enabled = true;

            if (deathCount > 0)
            {
                animator.SetBool("Respawn", true);
                inhale.Play();
            }
        }
    }

    public void Die()
    {
        if(!exitTriggered) 
        {
            animator.SetBool("Respawn", false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            deathCount++;
            InteractionManager.instance.ResetSanity();
        }
        //else if(exitTriggered)
        //{
        //    animator.SetBool("Respawn", false);
        //    SceneManager.LoadScene("TitleScreen");
        //}
    }
}
