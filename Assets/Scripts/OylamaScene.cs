using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class OylamaScene : MonoBehaviour
{
    public Transform playerListContainer;
    public PlayerVotePrefab playerVotePrefab;
    public TextMeshProUGUI oyuncuSiraText;
    public GameObject sonucPanel;
    public TextMeshProUGUI sonucText;

    private List<string> playerNames;
    
    private List<PlayerVotePrefab> playerVotePrefabs = new List<PlayerVotePrefab>(); 
    private int oyVerenOyuncuIndex = 0; // Oy verme sýrasýný takip etmek için indeks
    public static OylamaScene Instance { get; private set; }

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
        playerVotePrefabs = new List<PlayerVotePrefab>();

        for (int i = 0; i < playerNames.Count; i++)
        {
            PlayerVotePrefab playerVote = Instantiate(playerVotePrefab, playerListContainer);
            playerVote.Initialize(playerNames[i]);
            playerVotePrefabs.Add(playerVote);
        }

        oyuncuSiraText.text = "Oylamaya Baþla!";
    }

    public void OyVer(PlayerVotePrefab votedPlayer)
    {
        // Oy verme sýrasýnda oyuncunun kendi adýný ekranda göster
        oyuncuSiraText.text = "Oy Verildi: " + votedPlayer.GetPlayerName();

        // Oy verilen oyuncuya oy sayýsýný artýr
        votedPlayer.IncreaseVoteCount();

        oyVerenOyuncuIndex++;
        if (oyVerenOyuncuIndex >= playerNames.Count)
        {
            // Tüm oyuncular oy verdi, oylamayý bitir ve sonuç panelini göster
            sonucPanel.SetActive(true);
            OylamayiBitir();
        }
    }

    private void OylamayiBitir()
    {
        // En fazla oy alan oyuncuyu bul
        string enFazlaOyAlanOyuncu = playerNames.OrderByDescending(p => GetVoteCount(p)).First();

        // Oyuncuyu oyuncu listesinden çýkar
        PlayerManager.Instance.RemovePlayer(enFazlaOyAlanOyuncu);

        // Oylamayý bitir ve sonuç paneline yazdýr
        sonucText.text = enFazlaOyAlanOyuncu + " oyundan çýkarýldý!";
       
        
    }

    public void DevamEtButton()
    {
        SceneManager.LoadScene("OyunScene",LoadSceneMode.Single);
       
    }

    private int GetVoteCount(string playerName)
    {
        PlayerVotePrefab playerVote = playerVotePrefabs.Find(p => p.GetPlayerName() == playerName);
        return playerVote != null ? playerVote.GetVoteCount() : 0;
    }
    private void OnDestroy()
    {
        playerVotePrefabs.Clear();
    }

}
