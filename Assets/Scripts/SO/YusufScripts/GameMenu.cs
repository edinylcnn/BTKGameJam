using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{

    public Button startButton;
    public Button endButton;

    public void Start()
    {
        startButton.onClick.AddListener(NewGame);
        endButton.onClick.AddListener(ExitGame);
    }
    public void NewGame()
    {
        Debug.Log("buraya girdi mi");
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame()
    {
        Debug.Log("Oyun kapanıyor...");
        Application.Quit();
    }
}
