using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CellCreator : MonoBehaviour
{
    public bool CelIsPicture = false;

    [SerializeField] private Color _color;

    private SpriteRenderer _colorRenderer;

    void Awake()
    {
        _colorRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (CelIsPicture)
            ChangeCellsColor(_color);
        else ChangeCellsColor(Color.white);
    }

    private void ChangeCellsColor(Color color)
    {
        _colorRenderer.color = color;
    }
}
