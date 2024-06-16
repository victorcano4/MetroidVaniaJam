using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueBoxController : MonoBehaviour
{
    public List<string> DialogueLines;
    public TextMeshProUGUI DialogueText;

    int index = 0;
    void Start()
    {
        StartCoroutine(ShowLines());
    }

    IEnumerator ShowLines()
    {
        for(int i = 0; i < DialogueLines.Count; i++)
        {
            DialogueText.text = DialogueLines[index];
            index++;
            yield return new WaitForSeconds(4);

            if (index == DialogueLines.Count)
            {
                SceneManager.LoadScene("Credits");
            }
        }        
    }
}
