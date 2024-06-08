using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate_UI_Icon : MonoBehaviour
{
    public GameObject mouse;
    public GameObject start_button;
    public GameObject icon_aim;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mouse.SetActive(true);
            start_button.SetActive(true);
            icon_aim.SetActive(true);
        }
    }
}
