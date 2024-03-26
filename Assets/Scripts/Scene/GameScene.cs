

using System.Collections;
using UnityEngine;

public class GameScene : BaseScene
{
    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        Main.Resource.InstantiatePrefab("Floor");
        GameObject player = Main.Resource.InstantiatePrefab("PlayerSnake");

        StartCoroutine(SpawnFoodRoutine());
        
        return true;
    }


    IEnumerator SpawnFoodRoutine()
    {
        while (true)
        {

            float x = Random.Range(-20, 20);
            float y = Random.Range(-20, 20);

            Vector3 pos = new Vector3(x, 0.5f, y);
            Quaternion rot = Quaternion.identity;

            GameObject food = Main.Resource.InstantiatePrefab("Food", pos, rot, true);

            food.transform.position = pos;
            food.transform.rotation = rot;

            yield return new WaitForSeconds(2f);
        }
    }


}
