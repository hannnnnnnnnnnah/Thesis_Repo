using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [Header("Audios")]
    public static AudioClip lightSound, hitSound;
    public static AudioSource soundManager;
    private static float highestVolume = 1f;
    //private static float highVolume = 0.75f;
    private static float mediumVolume = 0.5f;
    //private static float lowVolume = 0.25f;
    //private static float lowestVolume = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        soundManager = GetComponent<AudioSource>();
        lightSound = Resources.Load<AudioClip>("light") as AudioClip;
        hitSound = Resources.Load<AudioClip>("hit") as AudioClip;
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "light":
                soundManager.PlayOneShot(lightSound, highestVolume);
                break;
            case "hit":
                soundManager.PlayOneShot(hitSound, mediumVolume);
                break;
        }
    }

    public static void RandomSound(bool randomizePitch)
    {
        if (randomizePitch)
            soundManager.pitch = Random.Range(.1f, .5f);
    }
}
