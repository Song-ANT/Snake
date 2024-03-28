

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

        Main.Resource.InstantiatePrefab("Floor");
        GameObject player = Main.Resource.InstantiatePrefab("PlayerSnake");
        var head = player.GetComponent<SnakeController>().GetSnakehead();
        Main.Cinemachine.SetPlayerSnakeCamera(head.transform);

        InitIntantiateEnemy(_initEnemyCount, "EnemySnake");
        InitIntantiateFood(_initFoodCount, "Food");

        StartCoroutine(SpawnFoodRoutine());
        
        return true;
    }


    private void InitIntantiateEnemy(int initCount, string initObject)
    {
        for (int i = 0; i < initCount; i++)
        {
            Debug.Log($"{i} : {initCount}");
            float x = Random.Range(-50f, 50f);
            float y = Random.Range(-50f, 50f);
            GameObject enemy = Main.Resource.InstantiatePrefab(initObject, new Vector3(x, 0.5f, y), Quaternion.identity);
        }
    }

    private void InitIntantiateFood(int initCount, string initObject)
    {
        for (int i = 0; i < initCount; i++)
        {
            float x = Random.Range(-50f, 50f);
            float y = Random.Range(-50f, 50f);

            Vector3 pos = new Vector3(x, 0.5f, y);
            Quaternion rot = Quaternion.identity;

            GameObject food = Main.Resource.InstantiatePrefab(initObject, pos, rot, true);

            food.transform.position = pos;
            food.transform.rotation = rot;
        }
    }

    IEnumerator SpawnFoodRoutine()
    {
        while (true)
        {

            InitIntantiateFood(1, "Food");

            yield return new WaitForSeconds(2f);
        }
    }


}
