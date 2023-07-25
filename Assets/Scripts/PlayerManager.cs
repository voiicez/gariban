using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{

    [SerializeField]
    private int playerCount;
    [SerializeField]
    public List<string> playerNames;
    [SerializeField]
    public Dictionary<string, string> playerRoles = new Dictionary<string, string>();
    public Dictionary<string, int> playerVotes = new Dictionary<string, int>();
    public Dictionary<string, int> playerMoney = new Dictionary<string, int>(); // Paralar� saklamak i�in yeni bir s�zl�k ekledik



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
        playerVotes.Remove(playerName);
    }


}
