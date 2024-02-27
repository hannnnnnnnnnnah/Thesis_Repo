using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] Volume sanityVolume;

    public bool lightExplodes, surroundSound = false;
    public int sanity, sanitySet = 5;

    public int surroundMin, surroundMax;

    public static InteractionManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void ResetSanity()
    {
        sanity = sanitySet;
        UpdateSanity();
    }

    public void UpdateSanity()
    {
        Debug.Log("sanity is at" + " " + sanity);

        /*if(sanity < sanitySet)
        {
            PlayerMovement.instance.flashback.Play();
            PlayerMovement.instance.animator.SetBool("Flashback", true);
            StartCoroutine(StopFlashback());
        }*/

        switch (sanity)
        {
            case 5:
                sanityVolume.weight = 0.0f;
                break;

            case 4:
                surroundSound = true;
                surroundMin = 0;
                surroundMax = 3;
                sanityVolume.weight += .2f;
                break;

            case 3:
                lightExplodes = true;
                sanityVolume.weight += .2f;
                break;

            case <= 2:
                sanityVolume.weight += .2f;
                break;
        }
    }

    IEnumerator StopFlashback()
    {
        yield return new WaitForSeconds(8f);
        PlayerMovement.instance.animator.SetBool("Flashback", false);
    }

    public void StartSurround()
    {
        DeathTimer.instance.StartCoroutine(DeathTimer.instance.StartSurroundSound(surroundMin, surroundMax));
    }

    public void StopSurround()
    {
        DeathTimer.instance.StopCoroutine(DeathTimer.instance.StartSurroundSound(surroundMin, surroundMax));
    }
}
