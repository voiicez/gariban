using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
  public void HandleStartButton()
    {
        SceneManager.LoadScene("KurulumScene");
    }
}
