using UnityEngine;

public class Main : MonoBehaviour
{
    private static bool initialized;
    private static Main instance;
    public static Main Instance
    {
        get
        {
            if (!initialized)
            {
                initialized = true;

                GameObject obj = GameObject.Find("@Main");
                if (obj == null)
                {
                    obj = new() { name = @"Main" };
                    obj.AddComponent<Main>();
                    DontDestroyOnLoad(obj);
                    instance = obj.GetComponent<Main>();
                }
            }
            return instance;
        }
    }

    private readonly PoolManager _pool = new();
    private readonly ResourceManager _resource = new();
    private readonly SnakeManager _snake = new();
    private readonly CinemachineManager _cinemachine = new();
    private readonly SpawnManager _spawn = new();
    private readonly GameManager _game = new();
    private readonly UIManager _ui = new();

    public static PoolManager Pool => Instance?._pool;
    public static ResourceManager Resource => Instance?._resource;
    public static SnakeManager Snake => Instance?._snake;
    public static CinemachineManager Cinemachine => Instance?._cinemachine;
    public static SpawnManager Spawn => Instance?._spawn;
    public static GameManager Game => Instance?._game;
    public static UIManager UI => Instance?._ui;
}
