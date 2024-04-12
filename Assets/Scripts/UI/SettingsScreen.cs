using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingsScreen : MonoBehaviour
{
    [SerializeField] GameObject settingsScreen;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] TextMeshProUGUI SubtitleText;
    [SerializeField] Image SubtitlesButtonImage;
    public static Sprite onButton, offButton;
    public bool subtitles;

    public void Start()
    {
        onButton = Resources.Load<Sprite>("on_button");
        offButton = Resources.Load<Sprite>("off_button");
        subtitles = true;
    }
    public void BackToPause()
    {
        pauseScreen.SetActive(true);
        settingsScreen.SetActive(false);
    }

    public void ChangeSubtitles()
    {
        if (SubtitleText.alpha > 0f && subtitles == true)
        {
            SubtitleText.alpha = 0f;
            SubtitlesButtonImage.sprite = offButton;
            subtitles = false;
            EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            SubtitleText.alpha = 255f;
            SubtitlesButtonImage.sprite = onButton;
            subtitles = true;
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
