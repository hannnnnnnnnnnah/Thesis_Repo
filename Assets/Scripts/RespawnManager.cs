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

    Canvas canvas;

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
        if(scene.buildIndex == 0)
            canvas.enabled = false;

        if(scene.buildIndex == 1 && gameStart)
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

    /*private void OnLevelWasLoaded(int level)
    {
        if(level == 0)
            canvas.enabled = false;

        if(level == 1 && gameStart)
        {
            Debug.Log("Death count:" + deathCount);
            canvas.enabled = true;

            if (deathCount > 0)
            {
                animator.SetBool("Respawn", true);
                inhale.Play();
            }
        }
    }*/

    public void Die()
    {
        if(!exitTriggered) 
        {
            animator.SetBool("Respawn", false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            deathCount++;
            InteractionManager.instance.ResetSanity();
        }
        else if(exitTriggered)
        {
            animator.SetBool("Respawn", false);
            SceneManager.LoadScene("TitleScreen");
        }
    }
}
