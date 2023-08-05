using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerManager : MonoBehaviour
{

    [SerializeField]
    private int playerCount;
    [SerializeField]
    public List<string> playerNames;
    [SerializeField]
    public Dictionary<string, string> playerRoles = new Dictionary<string, string>();
    public Dictionary<string, int> playerVotes = new Dictionary<string, int>();
    public Dictionary<string, int> playerMoney = new Dictionary<string, int>(); // Paraları saklamak için yeni bir sözlük ekledik
    public List<Karakterler> players;


    public static PlayerManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // PlayerManager nesnesini kalici hale getir
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void SetPlayerCount(int count)
    {
        playerCount = count;
    }

    public void SetPlayerNames(List<string> names)
    {
        playerNames = names;
    }

    public void SetPlayerRole(string playerName, string role)
    {
        if (playerRoles.ContainsKey(playerName))
        {
            playerRoles[playerName] = role;
        }
        else
        {
            playerRoles.Add(playerName, role);
        }

        Debug.Log(playerName + " is now assigned as " + role);
    }


    public int GetPlayerCount()
    {
        return playerCount;
    }

    public List<string> GetPlayerNames()
    {
        return playerNames;
    }

    public string GetPlayerRole(string playerName)
    {
        if (playerRoles.ContainsKey(playerName))
        {
            return playerRoles[playerName];
        }
        else
        {
            return string.Empty;
        }
    }

    public void SetPlayerVote(string playerName, int voteCount)
    {
        if (playerVotes.ContainsKey(playerName))
        {
            playerVotes[playerName] = voteCount;
        }
        else
        {
            playerVotes.Add(playerName, voteCount);
        }
    }

    public int GetPlayerVote(string playerName)
    {
        if (playerVotes.ContainsKey(playerName))
        {
            return playerVotes[playerName];
        }
        else
        {
            return 0;
        }
    }

    
    public void RemovePlayer(string playerName)
    {
      
        playerNames.Remove(playerName);
        playerRoles.Remove(playerName);
        var k = players.Where(p => p.name == playerName).FirstOrDefault();
        players.Remove(k);
        playerVotes.Remove(playerName);
    }

    public void SetPlayers()
    {
        players = new List<Karakterler>();
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
            
        }
    }

}
