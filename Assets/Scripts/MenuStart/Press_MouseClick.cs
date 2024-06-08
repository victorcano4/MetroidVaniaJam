using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Press_MouseClick : MonoBehaviour
{
    public Sprite mouse_unpressed, mouse_pressed;
    public Image image_UI;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            image_UI.sprite = mouse_pressed;
        }

        else
        {
            image_UI.sprite = mouse_unpressed;
        }
    }
}
