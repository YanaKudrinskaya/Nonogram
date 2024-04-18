using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PictureCreator : MonoBehaviour
{
    private SpriteRenderer _cellColor;
    [SerializeField] private bool cellPicture = false;
    
    public bool CellPicture
    {
        get => cellPicture; 
        set => cellPicture = value;
    }
    /*private void OnMouseOver()
    {
        _cellColor = GetComponent<SpriteRenderer>();

        if (Input.GetMouseButtonDown(0))
        {
            if(_cellPicture == false) 
            {
                _cellColor.color = Color.gray;
                _cellPicture = true;
            }
            else
            {
                _cellColor.color = Color.white;
                _cellPicture = false;
            }
        }
    }*/
}
