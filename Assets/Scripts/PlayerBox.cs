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

    public static PlayerBox Instance;

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
    }

    public void OnPlayerBoxClick()
    {
        OyunEkrani.Instance.ShowPlayerInfo(player);
    }


    public void OnActionButtonClick()
    {
        player.PerformAbility();
        OyunEkrani.Instance.PerformCurrentPlayerAction();
    }
    public void OnGecButtonClick()
    {
        //Boş geçildi.
        OyunEkrani.Instance.PerformCurrentPlayerAction();
    }
}
