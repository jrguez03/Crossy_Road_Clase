using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Creator : MonoBehaviour
{

    public delegate void PresionaEnter();
    public event PresionaEnter OnPresionarEnter;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Return))
        {
            if(OnPresionarEnter != null)
            {
                OnPresionarEnter();
            }
        }
    }
}
