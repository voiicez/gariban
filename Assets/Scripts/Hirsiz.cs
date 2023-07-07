using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hirsiz : Karakterler
{
    public bool hazir; //default ne ? SORUN �IKARAB�L�R.
    public Hirsiz(string name, int can, int coin,bool hazir,string aciklama) : base(name, can, coin,aciklama)
    {
        this.hazir = hazir;
    }

    public override void PerformAbility()
    {
        if (hazir && coin >= 30)
        {
            int totalCoin = 0;
            foreach (var gariban in Manager.Instance._characters)
            {
                if (gariban is Gariban)
                {
                    totalCoin += gariban.coin;
                    gariban.coin = 0;
                }
            }
            coin += totalCoin;
            hazir = false;
            Debug.Log("H�rs�z butona bast�! Garibanlar�n t�m paralar� H�rs�z'a aktar�ld�.");
        }
        else
        {
            Debug.Log("H�rs�z�n �zelli�i hen�z aktif de�il veya yeterli para yok.");
        }
    }
}
