using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public Texture2D customCursor;
    void Start()
    {
        Cursor.SetCursor(customCursor,new Vector2(400,400),CursorMode.Auto);
    }

}
