using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSetter : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;

    private void Start()
    {
        Cursor.SetCursor(cursorTexture, new Vector2(256, 256), CursorMode.Auto);
        Cursor.visible = true;
    }
}
