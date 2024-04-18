using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Nonogram : MonoBehaviour
{
    [SerializeField] private GameObject _cellPicture, _cellCross, _cellHeader;
    
    private float _distanceCell = 0.5f;
    private float _cellSize  = 10f;

    private int[] _nonogram;
    private LoadNonogram _loadNonogram;

    private int _rows, _columns;

    public bool IsSolved { get; private set; } = false;

    private List<List<int>> topHeader, leftHeader;

    private List<GameObject> TopHeaderObjectList, LeftHeaderObjectList;

    public void NonogramStart(int number)
    {
        _loadNonogram = GetComponent<LoadNonogram>();
        _nonogram = _loadNonogram.GetNonogram(number);
        _rows = _loadNonogram.GetRowCount(number);
        _columns = _loadNonogram.GetColumnCount(number);
        NonogramBuild();
    }
   private void NonogramBuild()
    {
        CreateGrid();
        CreateTopHeader();
        CreateLeftHeader();
    }
    public void CreateGrid()
    {
        int i = 0;
        float yPos = 0;
        for (int y = 0; y < _rows; y++)
        {
            float xPos = 0;
            for (int x = 0; x < _columns; x++)
            {
                GameObject spriteGrid;
                if (_nonogram[i] == 1)
                {
                    spriteGrid = Instantiate(_cellPicture.gameObject, gameObject.transform);
                    spriteGrid.transform.position = new Vector2(spriteGrid.transform.position.x + xPos,
                                                            spriteGrid.transform.position.y - yPos);
                }
                else
                {
                    spriteGrid = Instantiate(_cellCross.gameObject, gameObject.transform);
                    spriteGrid.transform.position = new Vector2(spriteGrid.transform.position.x + xPos,
                                                            spriteGrid.transform.position.y - yPos);
                }
                spriteGrid.GetComponent<Cell>().SetCoordinate(x, y);
                if ((x + 1) % 5 == 0)
                    xPos += BigGridStep();
                else
                    xPos += GridStep();
                i++;
            }
            if ((y + 1) % 5 == 0)
                yPos += BigGridStep();
            else
                yPos += GridStep();
        }
    }
    private void CreateTopHeader()
    {
        
        topHeader = new List<List<int>>();
        int maxHedersHeight = 0;
        for (int x = 0; x < _columns; x++)
        {
            List<int> list = new List<int>();
            int count = 0;
            int i = x;
            for (int y = 0; y < _rows; y++)
            {
                switch(_nonogram[i])
                {
                    case 0:
                        if (count != 0)
                        {
                            list.Add(count);
                            count = 0;
                        }
                        break;
                    case 1:
                        if (y == _rows - 1)
                        {
                            count++;
                            list.Add(count);
                        }
                        else
                        {
                            count++;
                        }
                        break;
                }
                i+=_columns;
            }
            if (list.Count == 0)
                list.Add(count);
            list.Reverse();
            topHeader.Add(list);
            if (maxHedersHeight < list.Count)
                maxHedersHeight = list.Count;
        }
        PicturesCellSum(topHeader);
        TopHeaderBuild(maxHedersHeight);
    }
    private void TopHeaderBuild(int height)
    {
        TopHeaderObjectList = new List<GameObject>();
        float xPos = 0;
        for (int x = 0; x < topHeader.Count; x++)
        {
            List<int> listColumn = topHeader[x];
            float yPos = GridStep();
            for (int y = 0; y < height; y++)
            {
                GameObject spriteHeader = Instantiate(_cellHeader, gameObject.transform);
                spriteHeader.transform.position = new Vector2(spriteHeader.transform.position.x + xPos,
                                                            spriteHeader.transform.position.y + yPos);
                if (y < listColumn.Count && listColumn[y] != 0)
                {
                    spriteHeader.GetComponentInChildren<TMP_Text>().text = listColumn[y].ToString();
                    spriteHeader.GetComponent<BoxCollider2D>().enabled = true;
                }
                spriteHeader.GetComponent<Cell>().SetCoordinate(x, y);
                TopHeaderObjectList.Add(spriteHeader);
                yPos += _cellSize + _distanceCell;
            }
            if ((x + 1) % 5 == 0)
                xPos += BigGridStep();
            else
                xPos += GridStep();
        }
    }
    public float GridStep()
    {
        float _gridStep = _cellSize + _distanceCell;
        return _gridStep;
    }
    public float BigGridStep()
    {
        float _gridStep = _cellSize + (2 * _distanceCell);
        return _gridStep;
    }
    private void CreateLeftHeader()
    {
        int i = 0;
        leftHeader = new List<List<int>>();
        int maxHedersHeight = 0;

        for (int x = 0; x < _rows; x++)
        {
            List<int> list = new List<int>();
            int count = 0;
            for (int y = 0; y < _columns; y++)
            {
                if (_nonogram[i] == 1 && y == _columns - 1)
                {
                    count++;
                    list.Add(count);
                }
                else if (_nonogram[i] == 1 && y < _columns - 1)
                {
                    count++;
                }
                else if (_nonogram[i] == 0 && count != 0)
                {
                    list.Add(count);
                    count = 0;
                }
                i++;
            }
            if (list.Count == 0)
                list.Add(count);
            list.Reverse();
            leftHeader.Add(list);
            if (maxHedersHeight < list.Count)
                maxHedersHeight = list.Count;
        }
        LeftHeaderBuild(maxHedersHeight);
    }
    private void LeftHeaderBuild(int height)
    {
        LeftHeaderObjectList = new List<GameObject>();
        float yPos = 0;
        for (int y = 0; y < leftHeader.Count; y++)
        {
            List<int> listColumn = leftHeader[y];
            float xPos = GridStep();
            for (int x = 0; x < height; x++)
            {
                GameObject spriteHeader = Instantiate(_cellHeader, gameObject.transform);
                spriteHeader.transform.position = new Vector2(spriteHeader.transform.position.x - xPos,
                                                            spriteHeader.transform.position.y - yPos);
                if (x < listColumn.Count && listColumn[x] != 0)
                {
                    spriteHeader.GetComponentInChildren<TMP_Text>().text = listColumn[x].ToString();
                    spriteHeader.GetComponent<BoxCollider2D>().enabled = true;
                }
                spriteHeader.GetComponent<Cell>().SetCoordinate(x, y);
                LeftHeaderObjectList.Add(spriteHeader);
                xPos += GridStep();
            }
            if ((y + 1) % 5 == 0)
                yPos += BigGridStep();
            else
                yPos += GridStep();
        }
    }
    private void PrintList(List<int> list)
    {
            foreach (int item in list)
            {
                print(item);
            }
    }
    private void PrintListsList(List<List<int>> Biglist)
    {
        foreach (List<int> subList in Biglist)
        {
            foreach (int item in subList)
            {
                print(item);
            }
        }
    }
    private void PicturesCellSum(List<List<int>> Biglist)
    {
        foreach(List<int> subList in Biglist)
{
            foreach (int item in subList)
            {
                Stats.PicturesCellSum += item;
            }
        }
    }
    public void HighlightRowAndColumn(int row, int column, Color color)
    {
        foreach (GameObject cellHeader in TopHeaderObjectList) 
        {
            if(cellHeader.GetComponent<CellHeader>().columnNumber == column)
            cellHeader.GetComponent<SpriteRenderer>().color = color;
        }
        foreach (GameObject cellHeader in LeftHeaderObjectList)
        {
            if (cellHeader.GetComponent<CellHeader>().rowNumber == row)
                cellHeader.GetComponent<SpriteRenderer>().color = color;
        }
    }
}
