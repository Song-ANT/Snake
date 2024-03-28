using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSceneUI : UI_Scene
{
    public TextMeshProUGUI Timer;
    
    public TextMeshProUGUI BossLv;
    
    public TextMeshProUGUI Rank1Name;
    public TextMeshProUGUI Rank1Lv;

    public TextMeshProUGUI Rank2Name;
    public TextMeshProUGUI Rank2Lv;

    public TextMeshProUGUI Rank3Name;
    public TextMeshProUGUI Rank3Lv;

    public TextMeshProUGUI Rank4Name;
    public TextMeshProUGUI Rank4Lv;




    public override bool Initialize()
    {
        if (!base.Initialize()) return false;



        return true;
    }
}
