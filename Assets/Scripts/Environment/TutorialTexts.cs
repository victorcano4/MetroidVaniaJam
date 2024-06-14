using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialTexts : MonoBehaviour
{

    public GameObject upgrade;
    private TextMeshPro text;

    void Start()
    {
        upgrade = GameObject.Find("Upgrade Gliding");
        text = gameObject.GetComponent<TextMeshPro>();
    }

    void Update()
    {
        if (upgrade = null)
        {
            //text.gameObject.SetActive(true);
        }
    }
}
