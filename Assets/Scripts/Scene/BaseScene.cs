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

        // ���� �ٲ𶧸��� �ʱ�ȭ�ϱ⶧���� main �ν��Ͻ����� �ȿ� bool���� ���� �ѹ��� ����ǰ� �ؾ��մϴ�.


        initialized = true;
        return true;
    }
}
