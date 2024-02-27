using UnityEngine;

public class TriggerPrompt : MonoBehaviour
{
    [SerializeField] string prompt;

    bool promptShown = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
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
