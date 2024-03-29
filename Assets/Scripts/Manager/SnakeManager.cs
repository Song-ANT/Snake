using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class SnakeData
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

    public List<SnakeData> GetTopSnakes(int count)
    {
        // snakeDatas 리스트를 level이 높은 순으로 정렬
        List<SnakeData> sortedSnakes = new List<SnakeData>(snakeDatas);
        sortedSnakes.Sort((x, y) => y.level.CompareTo(x.level)); // 내림차순 정렬

        // 상위 count개의 SnakeData를 반환
        return sortedSnakes.GetRange(0, Mathf.Min(count, sortedSnakes.Count));
    }


    //public SnakeData GetSnakeData(int index)
    //{
    //    return snakeDatas[index];
    //}

}
