using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCutscene : MonoBehaviour
{
    [SerializeField] FinalCutscene finalCutscene;
    public void StartEnd()
    {
        finalCutscene.Exit();
    }
}
