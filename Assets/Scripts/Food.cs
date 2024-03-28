using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IEated
{
    private int _plusFood;


    public void Eated()
    {
        Main.Pool.Push(gameObject);
        if(_plusFood == 0) Main.Spawn.InitIntantiateFood(1, Define.ObjectName.food);
        else
        {
            _plusFood += 1;
            if (_plusFood == 3) _plusFood =  0;
        }

    }
}
