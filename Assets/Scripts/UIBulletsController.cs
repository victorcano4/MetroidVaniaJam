using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIBulletsController : MonoBehaviour
{
    public int MaxBullets = 3;
    public int NumberOfBullets = 3;
    public TextMeshProUGUI ReloadingText;
    public GameObject UIBulletPrefab;
    private void Start()
    {
        for (int i = 0; i < MaxBullets; i++)
        {
            Instantiate(UIBulletPrefab, transform);
        }
    }
    public void BulletShot()
    {
        NumberOfBullets--;
        Destroy(transform.GetChild(transform.childCount - 1).gameObject);
        if (NumberOfBullets == 0)
            ReloadingText.gameObject.SetActive(true);
    }
    public void Reloaded() 
    {
        ReloadingText.gameObject.SetActive(false);
        NumberOfBullets = MaxBullets;
        for(int i = 0; i < MaxBullets; i++)
        {
            Instantiate(UIBulletPrefab,transform);
        }
    }
}
