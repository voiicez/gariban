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
            foreach (var player in OyunEkrani.Instance.players)
            {
                if (player is Gariban)
                {
                    totalCoin += player.coin;
                    player.coin = 0;
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
        Debug.Log("H�rs�z Yetene�ini Aktifle�tirdi.");
    }

}
