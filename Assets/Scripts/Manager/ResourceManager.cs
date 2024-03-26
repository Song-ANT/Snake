using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResourceManager
{
    public bool Loaded = false;
    public Dictionary<string, Object> _resources = new();


    #region ReadResources ------------------------------------------------------------------------------------------
    public void ResourcesAssign()
    {
        if (Loaded) return;

        ResourcesToDictionary<GameObject>();
        ResourcesToDictionary<InputActionAsset>();

        // TODO ���ҽ��������� ���� �ڷ��� ���� ��������. ���ҽ������ȿ� ���� ���ϸ��־ 1���� ����Ǵ�(����) ����

        Loaded = true;

    }

    public void ResourcesToDictionary<T>() where T : Object
    {
        foreach (var obj in Resources.LoadAll<T>(""))
        {
            if (!_resources.ContainsKey(obj.name))
                _resources.Add(obj.name, obj);
        }
    }
    #endregion



    #region Load & UnLoad--------------------------------------------------------------------------------------------
    public T Load<T>(string key) where T : Object
    {
        if (_resources.TryGetValue(key, out var resource)) return resource as T;

        return null;

    }
    public void Unload(string key)
    {
        if (_resources.TryGetValue(key, out var resource))
        {
            _resources.Remove(key);
            Resources.UnloadAsset(resource); // ���� �̰� �³�?
        }
    }
    #endregion



    #region Instatiate Prefab ---------------------------------------------------------------------------------------
    public GameObject InstantiatePrefab(string key, Transform parent = null, bool pooling = false)
    {
        GameObject prefab = Load<GameObject>(key);

        if (prefab == null)
        {
            Debug.LogError($"{key}������ �ҷ����� ����");
            return null;
        }

        if (pooling)
        {
            GameObject poo = Main.Pool.Pop(prefab);
            poo.transform.parent = parent;
            return poo;
        }

        GameObject obj = GameObject.Instantiate(prefab, parent);
        obj.name = prefab.name;
        return obj;
    }

    public GameObject InstantiatePrefab(string key, Vector3 _position, Quaternion _rotate, bool pooling = false) // �÷��̾�Ŵ�����
    {
        GameObject prefab = Load<GameObject>(key);

        if (prefab == null)
        {
            // Debug.LogError($"{key}������ �ҷ����� ����"); 
            return null;
        }

        if (pooling) return Main.Pool.Pop(prefab);

        GameObject obj = GameObject.Instantiate(prefab, _position, _rotate);
        obj.name = prefab.name;
        return obj;
    }

    public void Destroy(GameObject obj) //������ Ǯ�ε����� Ǯ������ ����
    {
        if (obj == null) return;
        if (Main.Pool.Push(obj)) return;

        UnityEngine.Object.Destroy(obj);
    }
    #endregion
}