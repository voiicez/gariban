using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hirsiz : Karakterler
{
    public bool hazir;
    private Gariban secilmisGariban;
    public Hirsiz(string name, int can, int coin,bool hazir,string aciklama) : base(name, can, coin,aciklama)
    {
        this.hazir = hazir;
        secilmisGariban = null;
    }

    public override void PerformAbility()
    {
        if (secilmisGariban != null && hazir)
        {
            int stolenCoins = secilmisGariban.GetCoin();
            secilmisGariban.SetCoin(0); // Seçili garibanın parasını sıfırla
            coin+=(stolenCoins); // Hırsızın parasına çalınan parayı ekle
        }
        hazir = false;
         
       
        Debug.Log("H�rs�z Yetene�ini Aktifle�tirdi.");
    }


}
