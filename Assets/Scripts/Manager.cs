using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Manager : MonoBehaviour
{
    public static Manager Instance;
   public TextMeshProUGUI karakterIsimText;
    public TextMeshProUGUI karakterAciklamaText;
    public List<Karakterler> _characters;
    private int currentPlayerIndex;
   

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

    public void StartGame(int garibanCount)
    {
        currentPlayerIndex = 0;

        // Oyuncu rollerini ve karakterlerini olu�turma
        _characters = new List<Karakterler>();

        for (int i = 1; i < garibanCount; i++)
        {
            _characters.Add(new Gariban("Gariban " + i, 100, 30,"Garibans�n. Hayatta kald���n her tur 2 para kazanacaks�n."));
        }

        _characters.Add(new Hirsiz("H�rs�z", 100, 30, true,"H�rs�zs�n. Do�ru an� yakalayabilirsen herkesin paras�n� �alabilirsin."));

        ShufflePlayers();
        Karakterler currentPlayer =_characters[currentPlayerIndex];
        karakterIsimText.text = _characters[currentPlayerIndex].name;
        karakterAciklamaText.text = _characters[currentPlayerIndex].aciklama;

        foreach (var character in _characters)
        {
            Debug.Log(character.name);
        }

        
    }

    private void ShufflePlayers()
    {
        // Oyuncu s�ralamas�n� rastgele kar��t�rma
        for (int i = 0; i < _characters.Count; i++)
        {
            Karakterler temp = _characters[i];
            int randomIndex = Random.Range(i, _characters.Count);
            _characters[i] = _characters[randomIndex];
            _characters[randomIndex] = temp;
        }
    }

    public void PerformCurrentPlayerAction()
    {
        if (currentPlayerIndex < _characters.Count)
        {
            Karakterler currentPlayer = _characters[currentPlayerIndex];
            currentPlayer.PerformAbility();
            karakterIsimText.text = _characters[currentPlayerIndex].name;
            karakterAciklamaText.text = _characters[currentPlayerIndex].aciklama;
            
            
        }
        else
        {
            Debug.Log("Oyun bitti.");
            return;
        }
        currentPlayerIndex++;
    }

   
}
