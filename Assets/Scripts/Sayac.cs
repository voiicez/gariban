using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sayac : MonoBehaviour
{
    public float sure = 180;
    public TextMeshProUGUI sayacText;

    // Update is called once per frame
    void Update()
    {
        if (sure > 0)
        {
            sure -= Time.deltaTime;
            KalanZaman(sure);
        }
        else
        {
            sure = 180;
        }
       
    }

    void KalanZaman(float zaman)
    {
        if(sure < 0)
        {
            sure = 0;
        }
        float dakika=Mathf.FloorToInt(sure / 60);
        float saniye=Mathf.FloorToInt(sure % 60);

        sayacText.text = string.Format("{0:00}:{01:00}",dakika,saniye);
    }

    public void SureEkle()
    {
        sure += 30;
    }
}
