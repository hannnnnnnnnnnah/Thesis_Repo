using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance;
    public AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();  
    }

    public void PlayBackgroundMusic()
    {
        audioSource.Play();
    }

    public void StopBackgroundMusic() 
    {
        audioSource.Stop();
    }
}
