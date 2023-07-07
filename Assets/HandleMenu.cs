using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HandleMenu : MonoBehaviour
{
    public TMP_InputField playerCountInput;
    public GameObject playerNameInputPrefab;
    public Transform playerNameInputContainer;
    public Button startButton;
    public Transform isimkayitButonBolge;
    public GameObject girilenAdButton;
    public List<string> oyuncular;

    private void Start()
    {
        startButton.interactable = false;
        List<string> oyuncular=new List<string>();
    }

    public void AddPlayerNameInputs()
    {
        int playerCount = int.Parse(playerCountInput.text);

        if (playerCount < 2)
        {
            Debug.Log("Oyuncu sayýsý en az 2 olmalýdýr.");
            return;
        }

        
        // Yeni isim giriþ alanlarýný oluþtur
        for (int i = 0; i < playerCount; i++)
        {
            GameObject playerNameInputObj = Instantiate(playerNameInputPrefab, playerNameInputContainer);
            GameObject butonPrefab = Instantiate(girilenAdButton,isimkayitButonBolge);
            
        }

        startButton.interactable = true;
    }

    public void StartGame()
    {
        // Ýþlemler ve kontroller yapýlabilir
        SceneManager.LoadScene("PlayerListScene");
    }

    public void HandleIsimKayit()
    {
        Debug.Log(playerNameInputPrefab.gameObject.GetComponent<TMP_InputField>().text.ToString());

    }
}
