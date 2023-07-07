using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    private int playerCount;
    private List<string> playerNames;
    private Dictionary<string, string> playerRoles;

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
}
