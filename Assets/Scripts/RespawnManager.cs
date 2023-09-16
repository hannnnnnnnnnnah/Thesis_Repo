using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    public Animator animator;
    [SerializeField] AudioSource inhale;

    public int deathCount = 1;

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
    }

    private void OnLevelWasLoaded(int level)
    {
        if(level == 0)
        {
            Debug.Log("Death count:" + deathCount);

            if (deathCount > 0)
            {
                animator.SetBool("Respawn", true);
                inhale.Play();
            }
        }
    }

    public void Die()
    {
        animator.SetBool("Respawn", false);
        SceneManager.LoadScene("Main_Proto1");
        deathCount++;
    }
}
