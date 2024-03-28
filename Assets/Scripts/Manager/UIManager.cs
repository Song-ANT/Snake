using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager 
{
    private GameObject UIRoot
    {
        get
        {
            GameObject uiRoot = GameObject.Find("@UI_Root");
            if (uiRoot == null)
            {
                uiRoot = new GameObject { name = "@UI_Root" };
            }
            return uiRoot;
        }
    }


    public UI_Base SceneUI { get; private set; }


    #region Scene
    public T SetSceneUI<T>() where T : UI_Scene
    {
        string sceneUIName = typeof(T).Name;
        SceneUI = SetUI<T>(sceneUIName, UIRoot.transform);

        OrderLayerToCanvas(SceneUI.gameObject, false);

        return (T)SceneUI;
    }

    private T SetUI<T>(string uiName, Transform parent = null) where T : Component
    {
        GameObject uiObject = Main.Resource.InstantiatePrefab(uiName, parent);
        T ui = Utilities.GetOrAddComponent<T>(uiObject);
        return ui;
    }

    public void OrderLayerToCanvas(GameObject uiObject, bool sort = true)
    {
        Canvas canvas = Utilities.GetOrAddComponent<Canvas>(uiObject);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;


        CanvasScaler scales = Utilities.GetOrAddComponent<CanvasScaler>(canvas.gameObject);
        scales.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scales.referenceResolution = new Vector2(760, 1280);


        Utilities.GetOrAddComponent<GraphicRaycaster>(uiObject);

        canvas.referencePixelsPerUnit = 100;
    }
    #endregion

    #region SubItem

    public T SetSubItemUI<T>(Transform parent = null) where T : UI_Base
    {
        string subitemUIName = typeof(T).Name;
        return SetUI<T>(subitemUIName, parent);
    }

    public void DestroySubItemUI<T>(GameObject obj) where T : UI_Base
    {
        Object.Destroy(obj);
    }


    #endregion



}
