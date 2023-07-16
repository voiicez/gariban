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
    public TMP_Dropdown dropdown;

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
    public void Initialize(Karakterler player, List<string> garibanIsimleri)
    {
        this.player = player;
        playerNameText.text = player.GetName();
        playerRoleText.text = player.GetRole();
        playerCoinText.text = player.GetCoin().ToString();
        playerDescriptionText.text = player.GetDescription();

        if (player is Hirsiz hirsiz)
        {
            dropdown.gameObject.SetActive(true);
            dropdown.ClearOptions();
            dropdown.AddOptions(garibanIsimleri);
            dropdown.gameObject.SetActive(true);
        }
        else
        {
            dropdown.gameObject.SetActive(false);
        }
    }





    public void OnActionButtonClick()
    {
        #region Hirsiz Durumu
        if (player is Hirsiz hirsiz)
        {
            string selectedGaribanName = dropdown.options[dropdown.value].text;
            Gariban selectedGariban = OyunEkrani.Instance.players.Find(p => p is Gariban && p.GetName() == selectedGaribanName) as Gariban;
            hirsiz.SetSelectedGariban(selectedGariban);
        }
        #endregion
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