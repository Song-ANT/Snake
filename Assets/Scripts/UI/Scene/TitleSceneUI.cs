using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneUI : UI_Scene
{

    public Button startBtn;

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        OnStartBtnClicked();

        return true;
    }

    private void OnStartBtnClicked()
    {
        startBtn.onClick.AddListener(GameStart);
    }

    private void GameStart()
    {
        SceneManager.LoadScene(Define.SceneName.game);
    }
}
