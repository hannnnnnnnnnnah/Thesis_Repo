using UnityEngine;
using TMPro;

public class SettingsScreen : MonoBehaviour
{
    [SerializeField] GameObject settingsScreen;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] TextMeshProUGUI SubtitleText;
    public void BackToPause()
    {
        pauseScreen.SetActive(true);
        settingsScreen.SetActive(false);
    }

    public void ChangeSubtitles()
    {
        if(SubtitleText.alpha > 0f)
            SubtitleText.alpha = 0f;
        else
            SubtitleText.alpha = 255f;
    }
}
