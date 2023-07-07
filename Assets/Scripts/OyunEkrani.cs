using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System;

public class OyunEkrani : MonoBehaviour
{
    public Transform playerBoxContainer;
    public PlayerBox playerBoxPrefab;

    private List<string> playerNames;
    public List<Karakterler> players;
    private int currentPlayerIndex;
    private PlayerBox currentPlayerBox;

    public static OyunEkrani Instance;

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

        // Her oyuncuya rol atayalým
        foreach (string playerName in playerNames)
        {
            string role = PlayerManager.Instance.GetPlayerRole(playerName);
            Karakterler player;

            if (role == "Hýrsýz")
            {
                player = new Hirsiz(playerName, 100, 30, true, "Hýrsýz her tur para kazanmaz. Sadece bir kez tüm Garibanlarýn parasýný çalabilir. Dikkatli kullan!");
            }
            else if (role == "Gariban")
            {
                player = new Gariban(playerName, 100, 30, "Gariban her tur 1 altýn kazanýr.");
            }
            else
            {
                // Bilinmeyen rol, varsayýlan olarak Gariban olarak ata
                player = new Gariban(playerName, 100, 30, "Gariban her tur 1 altýn kazanýr.");
            }

            players.Add(player);
            CreatePlayerBox(player);
        }

        // Ýlk oyuncunun bilgilerini göster
        ShowPlayerInfo(players[currentPlayerIndex]);
    }



    private void CreatePlayerBox(Karakterler player)
    {
        PlayerBox playerBox = Instantiate(playerBoxPrefab, playerBoxContainer);
        playerBox.Initialize(player);
        playerBox.actionButton.onClick.AddListener(playerBox.OnActionButtonClick);
    }



    public void ShowPlayerInfo(Karakterler player)
    {
        int playerBoxIndex = currentPlayerIndex % playerBoxContainer.childCount;
        currentPlayerBox = playerBoxContainer.GetChild(playerBoxIndex).GetComponent<PlayerBox>();
        currentPlayerBox.Initialize(player);
        currentPlayerBox.actionButton.gameObject.SetActive(true);
    }



    public void PerformCurrentPlayerAction()
    {
        currentPlayerBox.actionButton.gameObject.SetActive(false);

        currentPlayerIndex++;
        if (currentPlayerIndex >= players.Count)
        {
            currentPlayerIndex = 0;
        }

        // Ýlgili oyuncunun bilgilerini göster
        ShowPlayerInfo(players[currentPlayerIndex]);
    }
}
