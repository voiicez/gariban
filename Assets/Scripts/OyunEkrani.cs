using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System;
using System.Linq;

public class OyunEkrani : MonoBehaviour
{
    public Transform playerBoxContainer;
    public PlayerBox playerBoxPrefab;
    public TextMeshProUGUI oyuncuSiraText;
    private List<string> playerNames;
    public List<Karakterler> players;
    private int currentPlayerIndex;
    private PlayerBox currentPlayerBox;
    public GameObject siraGosterici;
    public GameObject oyunScreen;
    public Animator animation;
    public static OyunEkrani Instance;

    public GameObject geceOzetiPanel;
    public TextMeshProUGUI geceOzetiText;

    private List<string> geceOzetiListesi;

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
        
        
        playerNames = PlayerManager.Instance.GetPlayerNames();
        players = new List<Karakterler>();
        currentPlayerIndex = 0;
        ShufflePlayerNames();
        oyunScreen.SetActive(false);
        siraGosterici.SetActive(true);
        animation.SetTrigger("anim");
        
        // Her oyuncuya rol atayal�m
        foreach (string playerName in playerNames)
        {
            string role = PlayerManager.Instance.GetPlayerRole(playerName);
            Karakterler player;

            if (role == "Hirsiz")
            {
                player = new Hirsiz(playerName, 100, 30, true, "Hırsız her tur para kazanmaz. Sadece bir kez tüm Garibanların parasını çalabilir. Dikkatli kullan!");
            }
            else if (role == "Gariban")
            {
                player = new Gariban(playerName, 100, 30, "Gariban her tur 1 altın kazanır.");
            }
            else
            {
                // Bilinmeyen rol, varsay�lan olarak Gariban olarak ata
                player = new Gariban(playerName, 100, 30, "Gariban her tur 1 altın kazanır.");
            }

            players.Add(player);
            CreatePlayerBox(player);
        }

        // �lk oyuncunun bilgilerini g�ster
        ShowPlayerInfo(players[currentPlayerIndex]);
        oyuncuSiraText.text = players[currentPlayerIndex].name;
    }



    private void CreatePlayerBox(Karakterler player)
    {
        PlayerBox playerBox = Instantiate(playerBoxPrefab, playerBoxContainer);
        List<string> garibanIsimleri = players.Where(p => p is Gariban).Select(p => p.GetName()).ToList();
        playerBox.Initialize(player,garibanIsimleri);
        playerBox.actionButton.onClick.AddListener(playerBox.OnActionButtonClick);
        playerBox.gecButton.onClick.AddListener(playerBox.OnGecButtonClick);
        
    }



    public void ShowPlayerInfo(Karakterler player)
    {
        int playerBoxIndex = currentPlayerIndex % playerBoxContainer.childCount;
        currentPlayerBox = playerBoxContainer.GetChild(playerBoxIndex).GetComponent<PlayerBox>();
        List<string> garibanIsimleri = players.Where(p => p is Gariban).Select(p => p.GetName()).ToList();
        currentPlayerBox.Initialize(player, garibanIsimleri);
        currentPlayerBox.actionButton.gameObject.SetActive(false);

        if (player is not Gariban)
        {
            currentPlayerBox.actionButton.gameObject.SetActive(true);
           
        }
        currentPlayerBox.gecButton.gameObject.SetActive(true);
        
        
    }



    public void PerformCurrentPlayerAction()
    {
        currentPlayerBox.actionButton.gameObject.SetActive(false);

        currentPlayerIndex++;
        if (currentPlayerIndex >= players.Count)
        {
            currentPlayerIndex = 0;
            ShowGeceOzetiPanel();

        }

        // �lgili oyuncunun bilgilerini g�ster
        ShowPlayerInfo(players[currentPlayerIndex]);
        oyunScreen.SetActive(false);
        animation.SetTrigger("anim");
        siraGosterici.SetActive(true);
        oyuncuSiraText.text = players[currentPlayerIndex].name;
    }

   
    public void HandleSiraSende()
    {
        oyunScreen.SetActive(true);
        siraGosterici.SetActive(false);
    }

    private void ShufflePlayerNames()
    {
        System.Random random = new System.Random();
        int n = playerNames.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            string value = playerNames[k];
            playerNames[k] = playerNames[n];
            playerNames[n] = value;
        }
    }

    private void ShowGeceOzetiPanel()
    {
        siraGosterici.SetActive(false);
        geceOzetiPanel.SetActive(true);

        // Gece özeti listesini oluştur ve panelde göster
        geceOzetiListesi = new List<string>();

        // Her oyuncunun yaptığı aksiyonları ve sonuçları gece özeti listesine ekleyin
        foreach (Karakterler player in players)
        {
            // Örneğin, Hırsızın aksiyonunu ve sonucunu alalım
            if (player is Hirsiz hirsiz)
            {
                bool skillMevcut = hirsiz.GetHazir();
                if (!skillMevcut)
                {
                    string hirsizOlayi = "Bu gece bir soygun yaşandı.";
                    geceOzetiListesi.Add(hirsizOlayi);
                }
                

            }

            // Diğer karakterlerin aksiyonları ve sonuçları...
        }

        // Gece özeti metin alanına listeleyin
        geceOzetiText.text = "Gece Özeti:\n\n";
        for (int i = 0; i < geceOzetiListesi.Count; i++)
        {
            geceOzetiText.text += geceOzetiListesi[i];
            if (i < geceOzetiListesi.Count - 1)
            {
                geceOzetiText.text += "\n";
            }
        }

        if (geceOzetiListesi.Count==0)
        {
            geceOzetiText.text += "Bu gece hiçbir tatsız olay yaşanmadı.";
        }
    }

    public void TartismayaGecButton()
    {
        SceneManager.LoadScene("OylamaScene");   
    }


}
