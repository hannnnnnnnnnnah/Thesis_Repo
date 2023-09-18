using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggers : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    void ButtonHoverSound()
    {
        SoundManager.PlaySound("hit");
    }
    void LightBreakSound()
    {
        SoundManager.PlaySound("light");
    }
}
