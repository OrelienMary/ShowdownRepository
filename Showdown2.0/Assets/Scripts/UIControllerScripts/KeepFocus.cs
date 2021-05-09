using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepFocus : MonoBehaviour
{
    public MyEventSystem myEventSystem;

    GameObject lastGameObjectSelected;

    // Update is called once per frame
    void Update()
    {
        if(myEventSystem.currentSelectedGameObject != null)
            lastGameObjectSelected = myEventSystem.currentSelectedGameObject;

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            Invoke("Refocus", 0.01f);
        }
    }

    void Refocus()
    {
        if(lastGameObjectSelected != null)
            myEventSystem.SetSelectedGameObject(lastGameObjectSelected);
    }
}
