using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gariban : Karakterler
{
    
    
    public Gariban(string name,int can, int coin,string aciklama) : base(name, can, coin,aciklama)
    {
      
    }

    public override void PerformAbility()
    {
        Debug.Log("Gariban rol� ger�ekle�tirildi.");
        coin += 1;
    }

    public int GetCoin()
    {
        return coin;
    }
    public void SetCoin(int newCoin)
    {
        coin = newCoin;
    }

}
