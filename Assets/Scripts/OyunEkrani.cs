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

        // Her oyuncuya rol atayal�m
        foreach (string playerName in playerNames)
        {
            string role = PlayerManager.Instance.GetPlayerRole(playerName);
            Karakterler player;

            if (role == "H�rs�z")
            {
                player = new Hirsiz(playerName, 100, 30, true, "H�rs�z her tur para kazanmaz. Sadece bir kez t�m Garibanlar�n paras�n� �alabilir. Dikkatli kullan!");
            }
            else if (role == "Gariban")
            {
                player = new Gariban(playerName, 100, 30, "Gariban her tur 1 alt�n kazan�r.");
            }
            else
            {
                // Bilinmeyen rol, varsay�lan olarak Gariban olarak ata
                player = new Gariban(playerName, 100, 30, "Gariban her tur 1 alt�n kazan�r.");
            }

            players.Add(player);
            CreatePlayerBox(player);
        }

        // �lk oyuncunun bilgilerini g�ster
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

        // �lgili oyuncunun bilgilerini g�ster
        ShowPlayerInfo(players[currentPlayerIndex]);
    }
}
