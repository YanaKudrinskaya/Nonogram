using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class CellPicture : Cell
{
    public event UnityAction CellIsSolved;

    protected override void OnMouseOver()
    {
        base.OnMouseOver();
        if (Input.GetKey(KeyCode.Mouse0))
            ChangeCellsColor();
        if (Input.GetKey(KeyCode.Mouse1))
            Alarm();
    }
    public void ChangeCellsColor()
    {
        _renderer.color = Color.blue;
        Stats.Progress++;
        _collider.enabled = false;
        CellIsSolved?.Invoke();
    }
}
