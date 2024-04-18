using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadNonogram : MonoBehaviour
{
    public List<Nono> _nonoList { get; private set; }
    private NonoBase nonoBase;

    private void Start()
    {
        LoadInJsonToList();
    }
    public void LoadInJsonToList()
    {
        _nonoList = new List<Nono>();
        var jsonTextFile = Resources.Load<TextAsset>("NonogramJson");
        nonoBase = NonoBase.CreateFromJSON(jsonTextFile.ToString());
        List<String> list = nonoBase.listString;
        Debug.Log("nonograms записаны из json в nono");
        foreach (String str in list)
        {
            Nono nono = Nono.CreateFromJSON(str);
            _nonoList.Add(nono);
        }
    }

    public int GetNonogramsCount()
    {
        return _nonoList.Count;
    }
    public int[] GetNonogram(int number)
    {
        if (number > GetNonogramsCount() - 1)
            return null;
        else
            return _nonoList[number].array;
    }
    public int GetColumnCount(int number)
    {
        return _nonoList[number].column;
    }
    public int GetRowCount(int number)
    {
        return _nonoList[number].row;
    }
}

[Serializable]
public class Nono
{
    public int[] array;
    public int column;
    public int row;
    public static Nono CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Nono>(jsonString);
    }
}

[Serializable]
public class NonoBase
{
    public List<String> listString;
    public static NonoBase CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<NonoBase>(jsonString);
    }
}
