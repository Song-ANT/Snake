using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class UI_Base : MonoBehaviour
{
    public enum UIEvent
    {
        Click
    }


    private bool initialized = false;

    private void Start()
    {
        Initialize();
    }


    public virtual bool Initialize()
    {
        if (initialized) return false;


        if (GameObject.Find("EventSystem") == null)
        {
            GameObject obj = new() { name = "EventSystem" };
            obj.AddComponent<EventSystem>();
            obj.AddComponent<StandaloneInputModule>();
        }


        initialized = true;
        return true;
    }

    public static void BindEvent(GameObject obj, Action<PointerEventData> action = null, UIEvent type = UIEvent.Click)
    {
        UI_EventHandler evt = Utilities.GetOrAddComponent<UI_EventHandler>(obj);

        switch (type)
        {
            case UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
        }
    }
}
