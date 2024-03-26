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
    private readonly PlayerManager _player = new();

    public static PoolManager Pool => Instance?._pool;
    public static ResourceManager Resource => Instance?._resource;
    public static PlayerManager Player => Instance?._player;
}
