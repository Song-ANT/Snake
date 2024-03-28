using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public struct SnakeData
{
    public int index;
    public string name;
    public int level;
}

public class SnakeManager 
{
    private int index = 0;
    private List<SnakeData> snakeDatas = new List<SnakeData>();


    public void Initialize()
    {
        index = 0;
        snakeDatas.Clear();
    }

    public SnakeData AddSnakeData(int level, string name = null)
    {
        var data = new SnakeData();
        data.index = index++;
        if(name != null) { data.name = name; }
        else data.name = index.ToString();
        data.level = level;


        snakeDatas.Add(data);
        return data;
    }

    //public SnakeData GetSnakeData(int index)
    //{
    //    return snakeDatas[index];
    //}

}
