using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance;
    [SerializeField] AudioSource bgMusic1, bgMusic2, bgMusic3;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void PlayBackgroundMusic(int Track)
    {
        switch (Track)
        {
            case 1:
                bgMusic1.Play();
                break;
            case 2:
                bgMusic2.Play();
                break;
            case 3: 
                bgMusic3.Play();
                break;
        }
    }

    public void StopBackgroundMusic() 
    {
        bgMusic1.Stop();
        bgMusic2.Stop();
        bgMusic3.Stop();
    }
}
