using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Karakterler  //Herhangi bir karakteri tanýmlar
{
    public string name;
    public int can;
    public int coin;
    public string aciklama;

    public Karakterler(string name, int can, int coin, string aciklama)
    {
        this.can = can;
        this.name = name;
        this.coin = coin;
        this.aciklama = aciklama;
    }

    public virtual void PerformAbility()
    {
        //
    }

    public string GetName()
    {
        return name;
    }

    public int GetCoin()
    {
        return coin;
    }

    public string GetRole()
    {
        return PlayerManager.Instance.GetPlayerRole(name);
        
    }

    public string GetDescription()
    {
        return aciklama;
    }

}
