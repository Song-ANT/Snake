using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define 
{
    public struct SceneName
    {
        public const string title = "TitleScene";
        public const string game = "GameScene";
    }

    public struct ObjectName
    {
        public const string food = "Food";
        public const string enemy = "Enemy";
        public const string player = "Player";
        public const string head = "Head";
        public const string body = "Body";
        public const string movement = "Movement";
        public const string playerMaterial = "PlayerMaterial";
        public const string enemyMaterial = "EnemyMaterial";
    }

    public struct PrefabName
    {
        public const string floorPrefab = "Floor";
        public const string playerPrefab = "PlayerSnake";
        public const string enemyPrefab = "EnemySnake";
        public const string snakeHeadPrefab = "SnakeHead";
        public const string snakeBodyPrefab = "SnakeBody";
        public const string playerSnakeCamera = "PlayerSnakeCamera";
    }

    public const int BossLv = 100;
    public const int startTime = 30;
}
