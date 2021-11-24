using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EntryPoint : MonoBehaviour
{
    public Button StartGameButton;
    public void Start()
    {
        StartGameButton.onClick.AddListener(StartGame);
    }
    private void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}