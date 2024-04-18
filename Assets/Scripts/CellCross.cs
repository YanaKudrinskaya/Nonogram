using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class CellCross : Cell
{
    private TextMeshPro _textMeshPro;

    protected override void Start()
    {
        base.Start();
        _textMeshPro = GetComponentInChildren<TextMeshPro>();
        _textMeshPro.enabled = false;
    }
    protected override void OnMouseOver()
    {
        base.OnMouseOver();
        if (Input.GetKey(KeyCode.Mouse0))
            Alarm();
        if (Input.GetKey(KeyCode.Mouse1))
            PutCross();
    }
    protected void PutCross()
    {
        _textMeshPro.enabled = true;
        _collider.enabled = false;
    }
}
