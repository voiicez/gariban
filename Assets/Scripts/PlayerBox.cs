using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerBox : MonoBehaviour
{
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI playerRoleText;
    public TextMeshProUGUI playerCoinText;
    public TextMeshProUGUI playerDescriptionText;
    public Button actionButton;
    public Button gecButton;
    private Karakterler player;
    public TextMeshProUGUI hedef1;
    public TextMeshProUGUI hedef2;
    public static PlayerBox Instance;
    public Image hedefListImg;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Initialize(Karakterler player)
    {
        this.player = player;
        playerNameText.text = player.GetName();
        playerRoleText.text = player.GetRole();
        playerCoinText.text = player.GetCoin().ToString();
        playerDescriptionText.text = player.GetDescription();
        hedefListImg.gameObject.SetActive(true);
        List<string> garibanIsimleri = PlayerManager.Instance.playerNames.FindAll(name => name != player.GetName());
        hedef1.text = garibanIsimleri[0];
        hedef2.text = garibanIsimleri[1];



        if (player is Gariban)
        {
            hedefListImg.gameObject.SetActive(false);
        }
        
    }





    public void OnActionButtonClick()
    {
        player.PerformAbility();
        OyunEkrani.Instance.PerformCurrentPlayerAction();
    }
    public void OnGecButtonClick()
    {
        if (player is Gariban)
        {
            ((Gariban)player).PerformAbility();
            OyunEkrani.Instance.PerformCurrentPlayerAction();
        }
        else
            OyunEkrani.Instance.PerformCurrentPlayerAction();
    }
}