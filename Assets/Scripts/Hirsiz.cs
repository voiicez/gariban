using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hirsiz : Karakterler
{
    public bool hazir; //default ne ? SORUN ÇIKARABÝLÝR.
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
            Debug.Log("Hýrsýz butona bastý! Garibanlarýn tüm paralarý Hýrsýz'a aktarýldý.");
        }
        else
        {
            Debug.Log("Hýrsýzýn özelliði henüz aktif deðil veya yeterli para yok.");
        }
    }
}
