using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPrompt : MonoBehaviour
{
    [SerializeField] string prompt;

    bool promptShown = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            ShowPrompt(prompt);
        }
    }

    public void ShowPrompt(string _prompt)
    {
        if (!promptShown)
        {
            UIManager.instance.ShowText(_prompt);
            promptShown = true;
        }
        else
            UIManager.instance.ShowText("");
    }
}
