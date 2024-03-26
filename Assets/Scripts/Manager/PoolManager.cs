using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pool
{
    private GameObject _prefab;
    private Queue<GameObject> _pool = new Queue<GameObject>();
    private Transform _root;

    private Transform Root
    {
        get
        {
            if (_root == null)
            {
                var obj = new GameObject($"[Pool_Root] {_prefab.name}");
                _root = obj.transform;
            }
            return _root;
        }
    }

    public Pool(GameObject prefab, int initialSize)
    {
        _prefab = prefab;
        // 초기 풀 사이즈에 맞춰 오브젝트를 생성합니다.
        for (int i = 0; i < initialSize; i++)
        {
            var newObj = CreateNewObject();
            newObj.SetActive(false);
            _pool.Enqueue(newObj);
        }
    }

    public GameObject Pop()
    {
        if (_pool.Count == 0)
        {
            var newObj = CreateNewObject();
            newObj.SetActive(true);
            return newObj;
        }

        var obj = _pool.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public void Push(GameObject obj)
    {
        obj.SetActive(false);
        _pool.Enqueue(obj);
    }

    private GameObject CreateNewObject()
    {
        var obj = GameObject.Instantiate(_prefab, Root);
        obj.name = _prefab.name;
        return obj;
    }
}

public class PoolManager 
{

    private Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();

    public GameObject Pop(GameObject prefab, int initialSize = 10)
    {
        if (!_pools.ContainsKey(prefab.name))
        {
            CreatePool(prefab, initialSize);
        }

        return _pools[prefab.name].Pop();
    }

    public bool Push(GameObject obj)
    {
        if (!_pools.ContainsKey(obj.name)) return false;
        _pools[obj.name].Push(obj);
        return true;
    }

    private void CreatePool(GameObject prefab, int initialSize)
    {
        Pool pool = new Pool(prefab, initialSize);
        _pools.Add(prefab.name, pool);
    }

    public void Clear()
    {
        _pools.Clear();
    }
}
