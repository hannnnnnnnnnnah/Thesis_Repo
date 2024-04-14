using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsTransition : MonoBehaviour
{
    public void TitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    void LightBreakSound()
    {
        SoundManager.PlaySound("light");
    }
}
