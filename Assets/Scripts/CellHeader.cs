using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class CellHeader : Cell
{
    private GameObject _crossOut;
    protected override void Start()
    {
        base.Start();
        _crossOut = transform.GetChild(1).gameObject;
    }

    protected override void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
            CrossOut();
    }

    private void CrossOut()
    {
        if (!_crossOut.activeSelf)
            _crossOut.SetActive(true);
        else
            _crossOut.SetActive(false);
    }
}
