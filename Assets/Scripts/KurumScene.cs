using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using Unity.VisualScripting;

public class KurumScene : MonoBehaviour
{
    public GameObject playerNameInputPrefab;
    public RectTransform playerNameInputContainer;
    public Button hazirButton;
    public TMP_InputField playerCountInput;
    private int playerCount;
    private List<string> playerNames;
    private List<TMP_InputField> playerNameInputs;

    private void Start()
    {
        hazirButton.interactable = false; // Baþlangýçta hazýr düðmesi etkisiz olsun
    }

    public void HandleContinueButton()
    {
        playerCount = int.Parse(playerCountInput.text);
        playerNames = new List<string>();
        playerNameInputs = new List<TMP_InputField>();

        // Önceki isim giriþ alanlarýný temizle
        foreach (Transform child in playerNameInputContainer)
        {
            Destroy(child.gameObject);
        }

        // Yeni isim giriþ alanlarýný oluþtur
        for (int i = 0; i < playerCount; i++)
        {
            InstantiatePlayerNameInput();
        }
    }

    private void InstantiatePlayerNameInput()
    {
        GameObject playerNameInputObj = Instantiate(playerNameInputPrefab, playerNameInputContainer);
        TMP_InputField playerNameInput = playerNameInputObj.GetComponent<TMP_InputField>();

        playerNameInput.onEndEdit.AddListener(OnPlayerNameInputEndEdit); // Input alanýnýn doldurulduðunda OnPlayerNameInputEndEdit metodu tetiklensin

        playerNameInputs.Add(playerNameInput);
    }

    private void OnPlayerNameInputEndEdit(string playerName)
    {
        UpdatePlayerNames();

        if (playerNames.Count == playerCount)
        {
            hazirButton.interactable = true; // Hazýr düðmesini etkinleþtir
        }
        else
        {
            hazirButton.interactable = false; // Hazýr düðmesini devre dýþý býrak
        }
    }

    private void UpdatePlayerNames()
    {
        playerNames.Clear(); // Tüm isimleri temizle

        // Tüm input alanlarýný kontrol et ve isimleri kaydet
        foreach (TMP_InputField playerNameInput in playerNameInputs)
        {
            if (!string.IsNullOrEmpty(playerNameInput.text))
            {
                playerNames.Add(playerNameInput.text);
            }
        }
    }

    public void HandleHazirButton()
    {
        UpdatePlayerNames();

        // PlayerManager veya GameManager gibi bir scriptte bu bilgileri saklayabilirsiniz
        // Örneðin:
        PlayerManager.Instance.SetPlayerCount(playerCount);
        PlayerManager.Instance.SetPlayerNames(playerNames);

        // Oyunculara rolleri rastgele ata
        List<string> roles = new List<string>();
        roles.Add("Hýrsýz"); // Hýrsýz rolünü ekle
        for (int i = 1; i < playerCount; i++)
        {
            roles.Add("Gariban"); // Gariban rolünü ekle
        }

        ShuffleRoles(roles); // Rollerin sýrasýný rastgele karýþtýr

        // Her oyuncuya rolü atayalým
        for (int i = 0; i < playerCount; i++)
        {
            string playerName = playerNames[i];
            string role = roles[i];

            // Burada oyuncu adý ve rolünü saklamak için istediðiniz þekilde iþlem yapabilirsiniz
            // Örneðin:
            PlayerManager.Instance.SetPlayerRole(playerName, role);

           
        }

        SceneManager.LoadScene("OyunScene"); // Oyun ekraný sahnesine geçiþ yapýlacak sahne adýný buraya yazýn
    }

    private void ShuffleRoles(List<string> roles)
    {
        for (int i = 0; i < roles.Count; i++)
        {
            string temp = roles[i];
            int randomIndex = Random.Range(i, roles.Count);
            roles[i] = roles[randomIndex];
            roles[randomIndex] = temp;
        }
    }

}
