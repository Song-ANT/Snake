

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

        // �� ���� Init
        Main.Snake.Initialize();

        // �ٴ� ����
        Main.Resource.InstantiatePrefab(Define.PrefabName.floorPrefab);

        // �÷��̾� ����
        GameObject player = Main.Resource.InstantiatePrefab(Define.PrefabName.playerPrefab);
        var head = player.GetComponent<SnakeController>().GetSnakehead();
        Main.Cinemachine.SetPlayerSnakeCamera(head.transform); // �÷��̾� ī�޶�

        // �� �� ���� ����
        Main.Spawn.InitIntantiateEnemy(_initEnemyCount, Define.PrefabName.enemyPrefab);
        Main.Spawn.InitIntantiateFood(_initFoodCount, Define.ObjectName.food);

        // ���� �� UI����
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
