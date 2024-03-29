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

    private float _currentTime;
    private float _startTime;

    private bool _isGameOver;


    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        _currentTime = Time.time;
        _startTime = Define.startTime;
        BossLv.text = Define.BossLv.ToString();
        _isGameOver = true;

        return true;
    }

    private void Update()
    {
        float timeLeft = _startTime - (Time.time - _currentTime);
        if (timeLeft < 0)
        {
            Timer.text = "00:00";
            if (_isGameOver)
            {
                Main.UI.SetSceneUI<GameOverSceneUI>();
                _isGameOver = false;
            }
            return;
        }

        string minutes = ((int)timeLeft / 60).ToString();
        string seconds = (timeLeft % 60).ToString("f2");
        Timer.text = minutes + ":" + seconds.Substring(0, 2);

        UpdateTopSnakesUI();
    }


    private void UpdateTopSnakesUI()
    {
        // 상위 4개의 뱀 데이터 가져오기
        List<SnakeData> topSnakes = Main.Snake.GetTopSnakes(4);

        // 가져온 데이터를 UI에 표시
        if (topSnakes.Count > 0)
        {
            Rank1Name.text = topSnakes[0].name;
            Rank1Lv.text = topSnakes[0].level.ToString();
        }
        if (topSnakes.Count > 1)
        {
            Rank2Name.text = topSnakes[1].name;
            Rank2Lv.text = topSnakes[1].level.ToString();
        }
        if (topSnakes.Count > 2)
        {
            Rank3Name.text = topSnakes[2].name;
            Rank3Lv.text = topSnakes[2].level.ToString();
        }
        if (topSnakes.Count > 3)
        {
            Rank4Name.text = topSnakes[3].name;
            Rank4Lv.text = topSnakes[3].level.ToString();
        }
    }
}
