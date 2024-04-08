using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNote : MonoBehaviour
{
    [SerializeField] NoteStart noteStart;

    public void Note()
    {
        noteStart.StandUp();
    }
}
