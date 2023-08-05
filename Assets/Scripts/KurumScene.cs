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
    public int playerCount;
    private List<string> playerNames;
    private List<TMP_InputField> playerNameInputs;
    public TextMeshProUGUI playerCountText;
 
    public static KurumScene Instance;

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
    private void Start()
    {
        
        hazirButton.interactable = false; // Ba�lang��ta haz�r d��mesi etkisiz olsun
        playerCount = 3;
        playerNames = new List<string>();
        playerNameInputs = new List<TMP_InputField>();
        for (int i = 0; i < playerCount; i++)
        {
            InstantiatePlayerNameInput();
        }
    }

    public void OyuncuSayisiArttir()
    {
        if (playerCount < 9)
        {
            playerCount++;
            playerCountText.text = playerCount.ToString();
            playerNames = new List<string>();
            playerNameInputs = new List<TMP_InputField>();

            // �nceki isim giri� alanlar�n� temizle
            foreach (Transform child in playerNameInputContainer)
            {
                Destroy(child.gameObject);
            }

            // Yeni isim giri� alanlar�n� olu�tur
            for (int i = 0; i < playerCount; i++)
            {
                InstantiatePlayerNameInput();
            }
        }
    }
    public void OyuncuSayisiAzalt()
    {
        if (playerCount > 3) { 
        playerCount--;
        playerCountText.text = playerCount.ToString();
        playerNames = new List<string>();
        playerNameInputs = new List<TMP_InputField>();

        // �nceki isim giri� alanlar�n� temizle
        foreach (Transform child in playerNameInputContainer)
        {
            Destroy(child.gameObject);
        }

        // Yeni isim giri� alanlar�n� olu�tur
        for (int i = 0; i < playerCount; i++)
        {
            InstantiatePlayerNameInput();
        }
       }
    }

    public void HandleContinueButton()
    {
        //playerCount = int.Parse(playerCountInput.text);
        playerNames = new List<string>();
        playerNameInputs = new List<TMP_InputField>();

        // �nceki isim giri� alanlar�n� temizle
        foreach (Transform child in playerNameInputContainer)
        {
            Destroy(child.gameObject);
        }

        // Yeni isim giri� alanlar�n� olu�tur
        for (int i = 0; i < playerCount; i++)
        {
            InstantiatePlayerNameInput();
        }
    }

    private void InstantiatePlayerNameInput()
    {
        GameObject playerNameInputObj = Instantiate(playerNameInputPrefab, playerNameInputContainer);
        TMP_InputField playerNameInput = playerNameInputObj.GetComponent<TMP_InputField>();

        playerNameInput.onEndEdit.AddListener(OnPlayerNameInputEndEdit); // Input alan�n�n dolduruldu�unda OnPlayerNameInputEndEdit metodu tetiklensin

        playerNameInputs.Add(playerNameInput);
    }

    private void OnPlayerNameInputEndEdit(string playerName)
    {
        UpdatePlayerNames();

        if (playerNames.Count == playerCount)
        {
            hazirButton.interactable = true; // Haz�r d��mesini etkinle�tir
        }
        else
        {
            hazirButton.interactable = false; // Haz�r d��mesini devre d��� b�rak
        }
    }

    private void UpdatePlayerNames()
    {
        playerNames.Clear(); // T�m isimleri temizle

        // T�m input alanlar�n� kontrol et ve isimleri kaydet
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
        // �rne�in:
        PlayerManager.Instance.SetPlayerCount(playerCount);
        PlayerManager.Instance.SetPlayerNames(playerNames);

        // Oyunculara rolleri rastgele ata
        List<string> roles = new List<string>();
        roles.Add("Hirsiz"); // H�rs�z rol�n� ekle
        for (int i = 1; i < playerCount; i++)
        {
            roles.Add("Gariban"); // Gariban rol�n� ekle
        }

        ShuffleRoles(roles); // Rollerin s�ras�n� rastgele kar��t�r

        // Her oyuncuya rol� atayal�m
        for (int i = 0; i < playerCount; i++)
        {
            string playerName = playerNames[i];
            string role = roles[i];

            // Burada oyuncu ad� ve rol�n� saklamak i�in istedi�iniz �ekilde i�lem yapabilirsiniz
            // �rne�in:
            PlayerManager.Instance.SetPlayerRole(playerName, role);

           
        }
        PlayerManager.Instance.SetPlayers();
        
        SceneManager.LoadScene("OyunScene");
       
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
