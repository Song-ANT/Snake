

using System.Collections;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class GameScene : BaseScene
{
    private int _initEnemyCount = 30;
    private int _initFoodCount = 100;

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        // 뱀 생성 Init
        Main.Snake.Initialize();

        // 바닥 생성
        Main.Resource.InstantiatePrefab(Define.PrefabName.floorPrefab);

        // 플레이어 생성
        GameObject player = Main.Resource.InstantiatePrefab(Define.PrefabName.playerPrefab);
        var head = player.GetComponent<SnakeController>().GetSnakehead();
        Main.Cinemachine.SetPlayerSnakeCamera(head.transform); // 플레이어 카메라

        // 적 및 먹이 생성
        Main.Spawn.InitIntantiateEnemy(_initEnemyCount, Define.PrefabName.enemyPrefab);
        Main.Spawn.InitIntantiateFood(_initFoodCount, Define.ObjectName.food);

        // 게임 씬 UI생성
        Main.UI.SetSceneUI<GameSceneUI>();

        StartCoroutine(SpawnFoodRoutine());
        
        return true;
    }

    

    IEnumerator SpawnFoodRoutine()
    {
        while (true)
        {

            Main.Spawn.InitIntantiateFood(1, Define.ObjectName.food);

            yield return new WaitForSeconds(2f);
        }
    }


}
