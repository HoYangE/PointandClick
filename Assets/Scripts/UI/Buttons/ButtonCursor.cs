using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ButtonCursor : MonoBehaviour
{
    public Texture2D normalCursorTexture;
    public Texture2D eventCursorTexture;

    public void OnButton()
    {
        Cursor.SetCursor(eventCursorTexture, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void OutButton()
    {
        Cursor.SetCursor(normalCursorTexture, Vector2.zero, CursorMode.ForceSoftware);
    }
}
