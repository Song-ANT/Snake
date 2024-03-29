using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverSceneUI : UI_Scene
{
    public TextMeshProUGUI resultLv;

    public Button restartBtn;
    public Button mainmenuBtn;

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        Time.timeScale = 0f;

        resultLv.text = Main.Snake.GetSnakeData(0).level.ToString();


        restartBtn.onClick.AddListener(RestartBtnClicked);
        mainmenuBtn.onClick.AddListener(MainmenuBtnClicked);


        return true;
    }

    private void RestartBtnClicked()
    {
        SceneManager.LoadScene(Define.SceneName.game);
    }

    private void MainmenuBtnClicked()
    {
        SceneManager.LoadScene(Define.SceneName.title);
    }

}
