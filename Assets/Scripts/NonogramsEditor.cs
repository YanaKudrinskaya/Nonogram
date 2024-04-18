using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NonogramsEditor : MonoBehaviour
{
    [SerializeField] private GameObject _cellNonogram;
    [SerializeField][Range(0, 30)] private int _columns = 4;
    [SerializeField][Range(0, 30)] private int _rows = 3;

    private int[] _newPicture;
    private Nonogram _nonogram;
    private LoadNonogram _loadNonogram;
    private NonoBase nonoBase;
    private List<Nono> _nonoList;


    public void CreateEmptyGrid()
    {
        _nonogram = gameObject.GetComponent<Nonogram>();
        float yPos = 0;
        for (int y = 0; y < _rows; y++)
        {
            float xPos = 0;
            for (int x = 0; x < _columns; x++)
            {
                GameObject spriteGrid = Instantiate(_cellNonogram, gameObject.transform);
                spriteGrid.transform.position = new Vector2(spriteGrid.transform.position.x + xPos,
                                                            spriteGrid.transform.position.y - yPos);
                if ((x + 1) % 5 == 0)
                    xPos += _nonogram.BigGridStep();
                else
                    xPos += _nonogram.GridStep();
            }
            if ((y + 1) % 5 == 0)
                yPos += _nonogram.BigGridStep();
            else
                yPos += _nonogram.GridStep();
        }
    }

    private int[] AddPictureInArray()
    {
        _newPicture = new int[_rows * _columns];
        var _cells = GetComponentsInChildren<CellCreator>();
            for (int x = 0; x < _rows * _columns; x++)
            {
                if (_cells[x].CelIsPicture == true)
                {
                    _newPicture[x] = 1;
                }
                else
                    _newPicture[x] = 0;
            }
         return _newPicture;
    }
    public void AddNewNonogram()
    {
        Nono nono = new Nono();
        _loadNonogram = new LoadNonogram();
        var _newNonogram = AddPictureInArray();
        nono.array = _newNonogram;
        nono.row = _rows;
        nono.column = _columns;
        _loadNonogram.LoadInJsonToList();
        _nonoList = _loadNonogram._nonoList;
        _nonoList.Add(nono);
        Debug.Log("добывила новый массив к переменной нонограмс");
        print("Новая длина списка " + _nonoList.Count);
        SaveInJson();
    }

    public void SaveInJson()
    {
        List<String> nonoListString = new List<String>();
        foreach (Nono  nono in _nonoList)
        {
            string nonoJsonString = JsonUtility.ToJson(nono);
            nonoListString.Add(nonoJsonString);
        }
        nonoBase = new NonoBase();
        nonoBase.listString = nonoListString;
        string nonogramJson = JsonUtility.ToJson(nonoBase);
        StreamWriter file = File.CreateText("Assets/Resources/NonogramJson.json");
        file.WriteLine(nonogramJson);
        file.Close();
        print("записала в Джсон");
    }
    private void PrintArray(List<int[,]> list)
    {
        int num = 0;
        foreach (int[,] array in list)
        {
            foreach(int x in array)
            {
                print(x);
            }
            num++;
        }
    }
}




