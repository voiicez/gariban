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
    private int oyVerenOyuncuIndex = 0; // Oy verme s�ras�n� takip etmek i�in indeks
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

        oyuncuSiraText.text = "Oylamaya Ba�la!";
    }

    public void OyVer(PlayerVotePrefab votedPlayer)
    {
        // Oy verme s�ras�nda oyuncunun kendi ad�n� ekranda g�ster
        oyuncuSiraText.text = "Oy Verildi: " + votedPlayer.GetPlayerName();

        // Oy verilen oyuncuya oy say�s�n� art�r
        votedPlayer.IncreaseVoteCount();

        oyVerenOyuncuIndex++;
        if (oyVerenOyuncuIndex >= playerNames.Count)
        {
            // T�m oyuncular oy verdi, oylamay� bitir ve sonu� panelini g�ster
            sonucPanel.SetActive(true);
            OylamayiBitir();
        }
    }

    private void OylamayiBitir()
    {
        // En fazla oy alan oyuncuyu bul
        string enFazlaOyAlanOyuncu = playerNames.OrderByDescending(p => GetVoteCount(p)).First();

        // Oyuncuyu oyuncu listesinden ��kar
        PlayerManager.Instance.RemovePlayer(enFazlaOyAlanOyuncu);

        // Oylamay� bitir ve sonu� paneline yazd�r
        sonucText.text = enFazlaOyAlanOyuncu + " oyundan ��kar�ld�!";
       
        
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
