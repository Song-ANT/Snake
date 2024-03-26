using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScene : MonoBehaviour
{
    private bool initialized = false;

    private void Start()
    {
        if (!Main.Resource.Loaded)
        {
            Main.Resource.ResourcesAssign();
        }

        Initialize();

    }

    public virtual bool Initialize()
    {
        if (initialized) return false;

        // 씬이 바뀔때마다 초기화하기때문에 main 인스턴스들은 안에 bool값을 만들어서 한번만 실행되게 해야합니다.


        initialized = true;
        return true;
    }
}
