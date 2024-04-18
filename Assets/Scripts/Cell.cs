using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public abstract class Cell : MonoBehaviour
{
    protected BoxCollider2D _collider;
    protected SpriteRenderer _renderer;
    protected CursorActive cursor;
    protected Nonogram nonogram;
    protected Color _highlightHeaderColor = new(0.6f, 0.6f, 0.6f, 1);
    protected Color _headerColor = new(0.4f, 0.4f, 0.4f, 1);

    [HideInInspector] public int columnNumber, rowNumber;

    public event UnityAction LoseLife;
    private bool _loseLife = false;

    public void SetCoordinate(int column, int row)
    {
        columnNumber = column;
        rowNumber = row;
    }
    protected virtual void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        cursor = FindObjectOfType<CursorActive>();
        nonogram = FindObjectOfType<Nonogram>();
    }
    protected void Alarm()
    {
        _renderer.color = Color.red;
        Invoke("CellWhite", 0.3f);
    }
    private void CellWhite()
    {
        _renderer.color = Color.white;
        if (!_loseLife) 
        {
            _loseLife = true;
            LoseLife?.Invoke();
        }
    }
    protected virtual void OnMouseOver()
    {
        nonogram.HighlightRowAndColumn(rowNumber, columnNumber, _highlightHeaderColor);
    }
    protected void OnMouseEnter()
    {
        if (_renderer.enabled)
            cursor.ActiveCursor();
    }
    protected void OnMouseExit()
    {
        nonogram.HighlightRowAndColumn(rowNumber, columnNumber, _headerColor);
        cursor.NotActiveCursor();
    }
}
