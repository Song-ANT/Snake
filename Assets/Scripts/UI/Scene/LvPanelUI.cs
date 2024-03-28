using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class LvPanelUI : UI_Base
{
    public TextMeshProUGUI LvText;

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        

        return true;
    }


    public void SetLvText(string level)
    {
        LvText.text = level;
    }
}
