
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class NonogramGame : MonoBehaviour
{
    private Nonogram _nonogram;
    private LoadNonogram _nonograms;
    private Cell[] _cells;
    private UI _ui;
    [SerializeField] private GameObject[] _lifes;

    private void Start()
    {
        _nonogram = GetComponent<Nonogram>();
        _nonograms = GetComponent<LoadNonogram>();
        _ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();
    }

    private void Update()
    {
        if (Debug.isDebugBuild)
            DebugKeys();
    }
    public void LoadNewGame()
    {
        Stats.Level = 0;
        LoadLevel();
    }
    public void LoadLevel()
    {
        Stats.NewLevel();
        int level = Stats.Level;
        ClearGrid();
        if (level >= _nonograms.GetNonogramsCount())
        {
            _ui.GameOver();
        }
        else
        {
            _ui.UpdateLevel();
            foreach (GameObject life in _lifes)
            {
                life.GetComponent <UnityEngine.UI.Image> ().enabled = true;
            }
            _nonogram.NonogramStart(level);
            _cells = gameObject.GetComponentsInChildren<Cell>();
            for(int i=0; i< _cells.Length; i++)
            {
                if (_cells[i].GetComponent<CellPicture>()) 
                {
                    CellPicture cell = _cells[i].gameObject.GetComponent<CellPicture>();
                    cell.CellIsSolved += Victory;
                }
                _cells[i].LoseLife += DeleteLife;
            }
        }
    }
    private void DeleteLife()
    {
        Stats.numberOfLifes--;
        _lifes[Stats.numberOfLifes].GetComponent<UnityEngine.UI.Image>().enabled = false;
        if (Stats.numberOfLifes == 0)
        {
            ClearGrid();
            _ui.RestartLevelIfLose();
        }
    }
    public void Victory()
    {
        if (Stats.Progress == Stats.PicturesCellSum)
        {
            Stats.Level++;
            _ui.UpdateWin();
            DeleteCrosses();
        }
    }
    private void DeleteCrosses()
    {
        for (int i = 0; i < _cells.Length; i++)
        {
            if (_cells[i].GetComponent<CellCross>())
            {
                _cells[i].GetComponentInChildren<TextMeshPro>().enabled = false;
            }
        }
    }
    public void ClearGrid()
    {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
    private void DebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Stats.Level++;
            _ui.UpdateWin();
            DeleteCrosses();
        }
            
    }
}
