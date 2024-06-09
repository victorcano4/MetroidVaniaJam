using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options_Button : MonoBehaviour
{
    //UI light slider
    public Slider slider;
    public float increaseSliderFill;
    public float decreaseSliderFill;
    public GameManager gameManager;
    public GameObject option_menu;

    private void Update()
    {
        //Decreae slider value constantly
        slider.value -= decreaseSliderFill * Time.deltaTime;

        if (slider.value >= 0.95f)
        {
            option_menu.SetActive(true);
            PauseGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Adjust light value in the UI slider
        slider.value += increaseSliderFill;
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

}


