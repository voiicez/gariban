using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerVotePrefab : MonoBehaviour
{
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI voteCountText;

    private string playerName;
    private int voteCount;

    public void Initialize(string playerName)
    {
        this.playerName = playerName;
        playerNameText.text = playerName;
        voteCount = 0;
        voteCountText.text = "0";
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public int GetVoteCount()
    {
        return voteCount;
    }

    public void IncreaseVoteCount()
    {
        voteCount++;
        voteCountText.text = voteCount.ToString();
    }

    public void SetEliminated()
    {
        playerNameText.text = playerName + " (Elenen)";
        voteCountText.text = "0";
    }

    public void OnVoteButtonClicked()
    {
        OylamaScene.Instance.OyVer(this);
    }
}
