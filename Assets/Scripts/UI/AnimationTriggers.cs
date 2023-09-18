using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggers : MonoBehaviour
{
    void ButtonHoverSound()
    {
        SoundManager.PlaySound("hit");
    }
    void LightBreakSound()
    {
        SoundManager.PlaySound("light");
    }
}
