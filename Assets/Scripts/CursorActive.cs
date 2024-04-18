using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorActive : MonoBehaviour
{
    [SerializeField] private Texture2D ActiveTexture;
    private CursorMode cursorMode = CursorMode.Auto;
    [SerializeField] private Vector2 hotSpot = Vector2.zero;

    public void ActiveCursor()
    {
        Cursor.SetCursor(ActiveTexture, hotSpot, cursorMode);
    }

    public void NotActiveCursor()
    {
        Cursor.SetCursor(null, hotSpot, cursorMode);
    }
}
