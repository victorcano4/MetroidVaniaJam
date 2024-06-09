using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exit_Button : MonoBehaviour
{
    //UI light slider
    public Slider slider;
    public float increaseSliderFill;
    public float decreaseSliderFill;
    public GameManager gameManager;

    private void Update()
    {
        //Decreae slider value constantly
        slider.value -= decreaseSliderFill * Time.deltaTime;

        if (slider.value >= 0.95f)
        {
            gameManager.ExitGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Adjust light value in the UI slider
        slider.value += increaseSliderFill;
    }

}
