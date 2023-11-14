using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI controlText;
    string moveText = "Use WASD to move";
    public bool flashlightShown, sneakShown = false;

    public static UIManager instance;

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

        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ShowText(moveText);
    }

    public void ShowText(string text)
    {
        if (RespawnManager.instance.deathCount == 0)
        {
            controlText.SetText(text);
            controlText.GetComponent<Animator>().SetBool("ShowText", true);
            StartCoroutine(textFade(controlText));
        }
    }

    public IEnumerator textFade(TextMeshProUGUI _text)
    {
        yield return new WaitForSeconds(2f);
        _text.GetComponent<Animator>().SetBool("ShowText", false);
    }
}
