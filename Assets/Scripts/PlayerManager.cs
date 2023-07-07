using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{

    [SerializeField]
    private int playerCount;
    [SerializeField]
    private List<string> playerNames;
    [SerializeField]
    public Dictionary<string, string> playerRoles = new Dictionary<string, string>();

    public static PlayerManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // PlayerManager nesnesini kalýcý hale getir
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
}
